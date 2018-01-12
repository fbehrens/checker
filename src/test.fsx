open System
open System.Globalization
open System.IO
open System.Text.RegularExpressions

// #load "../.paket/load/netcoreapp2.0/main.group.fsx"
// open Expecto
// #load "checkerTests/Sample.fs"
// runTests defaultConfig  Tests.tests



let ex = "00:01:10,020 --> 00:01:12,219"

// replaces line containing time information shifted
let subtitle sec line =
    let timeShift time =
        let format = "HH:mm:ss,fff"
        let _, d = DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None) 
        d.AddSeconds(sec).ToString(format)
    let pattern = @"^(\d{2}:\d{2}:\d{2}.\d{3}) --> (\d{2}:\d{2}:\d{2}.\d{3})"
    let m = Regex.Match(line, pattern)
    if m.Success then
      timeShift m.Groups.[1].Value + " --> " + timeShift m.Groups.[2].Value
    else
      line

subtitle 5.0 ex


let fileShift sec name = 
    let lines = 
      File.ReadAllLines(name)
      |> Array.map (subtitle sec)
    File.WriteAllLines(name,lines)


fileShift 5.0 "src/fixtures/bm.srt"
