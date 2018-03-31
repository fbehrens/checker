module Checker.File
open System
open System.IO
open Checker

let scripts2 path =
  (Environment.GetEnvironmentVariable("scripts2"),path)
  |> IO.Path.Combine

let libwba = scripts2 "libwba"
let checker = scripts2 "checker"
let fixtures = scripts2 "checker/src/fixtures"

let cd dir = Environment.CurrentDirectory <- dir

let inlineMap f name =
  let lines =
      File.ReadAllLines(name)
      |> Array.map f
  File.WriteAllLines(name,lines)

let rec getFileInfo (dir:DirectoryInfo) : FileInfo list =
  let excluded= ["packages";".paket";"bin";"obj"]
  let files = [ for f in dir.EnumerateFiles() -> f  ]
              |> List.filter (fun f -> f.Name.StartsWith(".") |> not )
  let subFiles = [ for d in dir.EnumerateDirectories() -> d ]
                 |> List.filter (fun d -> List.contains d.Name excluded |> not )
                 |> List.filter (fun d -> d.Name.StartsWith(".") |> not )
                 |> List.map getFileInfo
  List.concat (files::subFiles)

let dir path =
    new DirectoryInfo (path)
    |> getFileInfo
    |> List.map (fun f -> f.FullName)
    |> Regex.projectFiles

let hasUtf8Bom file =
    let b = file |> File.ReadAllBytes
    b.Length > 2 && b.[0] = 239uy && b.[1] = 187uy && b.[2] = 191uy

let removeUtf8Bom file =
    printf "%s Remove utf8BOM: " file
    let bl = file |> File.ReadAllBytes |> List.ofArray
    match bl with
    | 239uy :: 187uy :: 191uy :: tail ->
        let r = tail |> Array.ofList
        File.WriteAllBytes(file, r)
        printfn "done"
    | _ ->
        printfn "no"

let checkCrlf file =
  let t = File.ReadAllText file
  sprintf "%s %s" (Regex.lookCrlf t) file

let normalizeCrlf file =
  let l = File.ReadAllLines file
  File.WriteAllLines(file,l)

let removeTrailingWhitespace = inlineMap Regex.removeTrailingWhitespace
