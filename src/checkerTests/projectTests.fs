module CheckerTests.Project
open Swensen.Unquote
open Expecto
open Checker.Project

[<Tests>]
let tests =
  testList "project" [
      test "foo" { foo =! "bar" }
  ]
