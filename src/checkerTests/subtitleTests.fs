module CheckerTests.Subtitle 
open Swensen.Unquote
open Expecto
open Checker.Subtitle

[<Tests>]
let tests =
  testList "subtitle" [
      test "timeShift" {
          timeShift 5.0 "00:01:10,020"  =! "00:01:15,020"
      } 
      test "subtitle" {
          subtitle 5.0 "00:01:10,020 --> 00:01:12,219" =! "00:01:15,020 --> 00:01:17,219"
          subtitle 5.0 "bla" =! "bla"
      }
  ]
