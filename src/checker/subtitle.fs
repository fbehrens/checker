// namespace Checker
module Checker.Subtitle 
open System
open System.Globalization
open System.IO
open System.Text.RegularExpressions

let timeShift sec time =
    let format = "HH:mm:ss,fff"
    let _, d = DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None) 
    d.AddSeconds(sec).ToString(format)

let subtitle sec line =
    let pattern = @"^(\d{2}:\d{2}:\d{2}.\d{3}) --> (\d{2}:\d{2}:\d{2}.\d{3})"
    let m = Regex.Match(line, pattern)
    if m.Success then
      timeShift sec m.Groups.[1].Value + " --> " + timeShift sec m.Groups.[2].Value
    else
      line

let fileShift sec name = 
    let lines = 
      File.ReadAllLines(name)
      |> Array.map (subtitle sec)
    File.WriteAllLines(name,lines)
// fileShift 5.0 "src/fixtures/bm.srt"
