open Expecto
open Mechanic

let tests =
  test "A simple test" {
    Expect.equal Say.foo "bar" "The strings should equal"
  }

[<EntryPoint>]
let main args =
  runTestsWithArgs defaultConfig args tests
