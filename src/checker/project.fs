module Checker.Project
open Checker.File
open Checker.Regex
open System.Xml
let foo="bar"

type Project = {
  Name : string
  File : string
  Files : string list
  References : string list
}

let load p =
   printfn "\n// %s" p.Name
   p.Files
   |> List.iter( printfn "#load \"%s\"")

let getProject f =
  let doc = new XmlDocument()
  let name = firstCapture @"([\w]+)\.fsproj$" f
  let content = text f
  let getAttribute path (attribute:string) =
    doc.SelectNodes path
    |> Seq.cast<XmlNode>
    |> Seq.map ( fun n -> n.Attributes.[attribute].InnerText)
    |> List.ofSeq
  doc.LoadXml content
  {
    Name = name
    File =  f
    Files = getAttribute "/Project/ItemGroup/Compile" "Include"
    References = getAttribute "/Project/ItemGroup/ProjectReference" "Include"
  }

let projects =
  dir checker
  |> List.filter (matches @"\.fsproj$")
  |> List.map getProject

let project name =
  projects
  |> List.find (fun p -> p.Name = name)
