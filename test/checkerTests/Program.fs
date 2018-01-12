open Swensen.Unquote
open Expecto

open Checker

let tests =
  test "A simple test" {
    Say.foo =! "bar" 
  }  

[<EntryPoint>]
let main args =
  runTestsWithArgs defaultConfig args tests
