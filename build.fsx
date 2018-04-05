(* -- Fake Dependencies paket-inline
source https://api.nuget.org/v3/index.json

nuget Fake.Core.Target prerelease
nuget FSharp.Core prerelease
-- Fake Dependencies -- *)

#r @"packages/build/FAKE/tools/FakeLib.dll"
open System
open System.Diagnostics

open Fake

let mutable dotnetCliPath = "dotnet"
let dotnetCliVersion = "2.1.103"

let runDotNet args =
  let proc (info : ProcessStartInfo) =
    info.FileName <- dotnetCliPath
    info.WorkingDirectory <- "."
    info.Arguments <- args
  let result = ProcessHelper.ExecProcess proc TimeSpan.MaxValue
  if result <> 0 then failwithf "dotnet %s failed" args

Target "Clean" <| fun _ ->
  !!"**/bin"++"**/obj" |> CleanDirs

Target  "InstallDotNet" <| fun _ ->
  dotnetCliPath <- DotNetCli.InstallDotNetSDK dotnetCliVersion

Target "Build" <| fun _ ->
  runDotNet "build"

Target "Test" <| fun _ ->
  !!"**/*Tests.fsproj"
  |> Seq.iter ( sprintf "run --project %s --no-build" >> runDotNet )

"Clean"
  ==> "InstallDotNet"
  ==> "Build"
  ==> "Test"

RunTargetOrDefault "Test"
