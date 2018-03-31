#I "../../.paket/load/netcoreapp2.0"
#load "main.group.fsx"


#load "project.fs"
Checker.Project.projects

open System.Xml
open System.IO
open System

//let xml = File.ReadAllText("src/checker/checker.fsproj")

let xml = """
<EmailList>
  <Email>test@email.com</Email>
  <Email>test2@email.com</Email>
</EmailList>
"""
let doc = new XmlDocument() in
    doc.LoadXml xml;
    doc.SelectNodes "/EmailList/Email/text()"
        |> Seq.cast<XmlNode>
        |> Seq.map (fun node -> node.Value)
        |> String.concat Environment.NewLine

let d = new XmlDocument()
d.LoadXml """
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="util.fs" />
    <Compile Include="regex.fs" />
    <Compile Include="file.fs" />
    <Compile Include="subtitle.fs" />
    <Compile Include="project.fs" />
  </ItemGroup>
  <Import Project="..\..\.paket\Paket.Restore.targets" />
</Project>
"""

let n =
  d.SelectNodes "/Project/ItemGroup/Compile"
  |> Seq.cast<XmlNode>
  |> Seq.map ( fun n -> n.Attributes.["Include"].InnerText)
  |> Seq.iter( printfn "#load \"%s\"")

