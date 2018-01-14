module Tests.Checker

open Swensen.Unquote
open Expecto
open Checker.Say

[<Tests>]
let tests =
  testList "samples" [
      test "bla" { 
          foo =! "bar"
      } 
  ]
