// #I "../../.paket/load/net47"
#I "../../.paket/load/netcoreapp2.0"
#load "Unquote.fsx"
open Swensen.Unquote

fsi.PrintSize <- 1000
fsi.PrintDepth <-1000
fsi.PrintLength <-10000
fsi.PrintWidth <- 50

open System
open System.IO
open System.Text.RegularExpressions

#load "util.fs" "regex.fs" 
#load "file.fs"
open Checker.File
//cd libwba
cd checker

dir "."
|> List.filter hasUtf8Bom
|> List.iter removeUtf8Bom


dir "."
|> List.map checkCrlf
|> List.sort

dir "."
|> List.iter normalizeCrlf


dir "."
|> List.iter removeTrailingWhitespace

removeTrailingWhitespace @"c:\scripts2\libwba\build.fsx"


checkCrlf @"c:\scripts2\libwba\build.fsx"


File.ReadAllBytes(@"c:\scripts2\libwba\src\tools\main.fs")


open System.Text.RegularExpressions

let ps1 = Regex "\.ps1$"

ps1.Match "ss.ps1"


let n=Environment.NewLine
n = "\n"
let lf = "1\n2\n"
let crlf = "1\r\n2\r\n"



let l = @"C:\scripts2\libwba\modules\airwatch\airwatch.ps1"
let t = File.ReadAllText(l)

t.[0]


File.ReadAllText("lf") =! lf
File.ReadAllText("crlf") =! crlf
File.ReadAllLines("lf").[0] =! "1"




let fix = new DirectoryInfo "src/fixtures"


// getFiles fix |> List.map (fun f -> f.Name)



