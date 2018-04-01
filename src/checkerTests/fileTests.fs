module CheckerTests.File
open Swensen.Unquote
open Expecto
open Checker.File

let config = { FsCheckConfig.defaultConfig with maxTest = 10000 }

[<Tests>]
let tests =
  testList "file" [
      test "foo" { foo =! "bar" }
      testProperty "commutative" <| fun a b ->
        a + b = b + a
      testPropertyWithConfig config  "commutative10000" <| fun a b ->
        a + b = b + a
  ]
