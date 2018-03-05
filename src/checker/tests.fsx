#I "../../.paket/load/net47"
#load "main.group.fsx"

open Expecto
#load "../checker/subtitle.fs"
#load "../checkerTests/subtitleTests.fs"
runTests defaultConfig  CheckerTests.Subtitle.tests
