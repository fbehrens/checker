#load "../../.paket/load/netcoreapp2.0/main.group.fsx"
open Expecto

#load "../checker/subtitle.fs"
#load "../checkerTests/subtitleTests.fs"
runTests defaultConfig  CheckerTests.Subtitle.tests
