module CheckerTests.Regex
open Swensen.Unquote
open Expecto
open Checker.Regex

[<Tests>]
let tests =
  testList "regex" [
    test "matches" {
      matches "b" "abc" =! true
      matches "d" "abc" =! false
    }
  ]
