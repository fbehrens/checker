// #I "../../.paket/load/net47"
#I "../../.paket/load/netcoreapp2.0"
#load "Unquote.fsx"
#load "expecto.fsx"
open Swensen.Unquote
open Expecto

#load"util.fs"
#load"regex.fs"
#load"subtitle.fs"

#load"file.fs"
#load"../checkerTests/fileTests.fs"
runTests defaultConfig CheckerTests.File.tests

