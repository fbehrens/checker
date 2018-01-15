#load "../../.paket/load/netcoreapp2.0/Unquote.fsx"
open System
open Swensen.Unquote
open System
open System.IO
open System.Text.RegularExpressions

let n=Environment.NewLine
n =! "\n"
let lf = "1\n2\n"
let crlf = "1\r\n2\r\n"

File.ReadAllText("src/fixtures/lf") =! lf
File.ReadAllText("src/fixtures/crlf") =! crlf
File.ReadAllLines("src/fixtures/lf").[0] =! "1"

let fix = new DirectoryInfo "src/fixtures"

let rec getFiles (dir:DirectoryInfo) : FileInfo list =
  let excluded= ["packages";".paket";"bin";"obj"] 
  let files = [ for f in dir.EnumerateFiles() -> f  ]
              |> List.filter (fun f -> f.Name.StartsWith(".") |> not ) 
  let subFiles = [ for d in dir.EnumerateDirectories() -> d ]
                 |> List.filter (fun d -> List.contains d.Name excluded |> not ) 
                 |> List.filter (fun d -> d.Name.StartsWith(".") |> not ) 
                 |> List.map getFiles 
  List.concat (files::subFiles)

// getFiles fix |> List.map (fun f -> f.Name)



Regex.Replace("aS01E02vvf.en.srt",".*(S\d\dE\d\d).*?(\.en)?\.(.*?)","$1$2.$3")




let remove pattern = 
  new DirectoryInfo "."
  |> getFiles 

remove "ripped"
|> List.map (fun f -> f.FullName)



Regex.Escape ".1"

Environment.CurrentDirectory <- "/Users/fb/Movies/tv/Black.Mirror/s01"








