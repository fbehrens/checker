module CheckerTests.Regex
open Swensen.Unquote
open Expecto
open Checker.Regex

[<Tests>]
let tests =
  test "regex" {

      matches "b" "abc" =! true
      matches "d" "abc" =! false
      notMatches "b" "abc" =! false
      notMatches "d" "abc" =! true

      getMatches "." "ab" =! ["a";"b"]

      firstCapture "a(.)c" "abc" =! "b"
      noneFirstCapture "a(r)c" "abc" =! "None"
    }
