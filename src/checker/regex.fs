module Checker.Regex
open System.Text.RegularExpressions
open System
open Checker.Util
// #load "util.fs"
let regex = memoize (fun s -> new Regex (s) )

let matches p s =    
    let m = (regex p).Match s
    m.Success
// matches "b" "abc"

let notMatches p = 
    matches p >> not

let getMatches p s = 
  [ for m in (regex p).Matches(s) ->
      m.Groups.[0].Value ]
// getMatches "." "ab" 

let firstCapture p s =  
    let m = (regex p).Match( s )
    m.Groups.[1].Value 
// firstCapture "a(.)c" "abc"

let noneFirstCapture p s =  
    let m = (regex p).Match( s )
    if m.Success then m.Groups.[1].Value 
    else "None"

let notmatchesGitEtc = 
    notMatches @"\\(.git|bin|obj|packages|.fake|vendor|paket-files)\\"

let matchesSource = 
    matches @"\.(ps1|psm1|fs|fsx)$"

let projectFiles = 
    List.filter notmatchesGitEtc >> List.filter matchesSource

let extension = 
    noneFirstCapture @"\.([^\.]+)$" 

let lookCrlf s =
    let d = dict [("\r\n","CRLF")
                  ("\n","LF")]    
    let toText s = d.[s] 
    let m = 
        getMatches "\r?\n" s 
        |> List.map toText 
        |> List.distinct 
        |> List.sort
        |> Array.ofList
    String.Join("+",m)

let removeTrailingWhitespace line =
  (regex @"\s*$").Replace(line,"")
