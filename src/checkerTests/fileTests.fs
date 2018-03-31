module CheckerTests.File
open Swensen.Unquote
open Expecto
open Checker.File

[<Tests>]
let tests =
  testList "file" [
      test "foo" { foo =! "bar" }
      test "foo1" { foo =! "bar" }
  ]
