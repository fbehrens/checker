#r "paket: groupref build //"
#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators


let dnOptions =
  let localPath = Path.getFullName "."
  DotNet.Options.withWorkingDirectory localPath >> ( Some DotNet.Verbosity.Minimal |> DotNet.Options.withVerbosity )

let runDotNet cmd args =
    let result = DotNet.exec dnOptions cmd args
    if result.ExitCode <> 0 then failwithf "'dotnet %s %s failed(%i)" cmd args result.ExitCode


Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    |> Shell.cleanDirs
)

Target.create "Build" (fun _ ->
    !! "src/**/*.*proj"
    |> Seq.iter (runDotNet "build")
)
Target.runOrDefault "Build"


(*
#r "paket: groupref build //"
#load "./.fake/build.fsx/intellisense.fsx"

// #r @"C:\Users\imynv\.nuget\packages\fake\4.64.14\tools\FakeLib.dll"

open Fake.Core
open System
open System.Diagnostics


let mutable dotnetCliPath = "dotnet"
let dotnetCliVersion = "2.1.103"

let runDotNet args = ()
//   let proc (info : ProcessStartInfo) =
//     info.FileName <- dotnetCliPath
//     info.WorkingDirectory <- "."
//     info.Arguments <- args
//   let result = ProcessHelper.ExecProcess proc TimeSpan.MaxValue
//   if result <> 0 then failwithf "dotnet %s failed" args


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

*)
