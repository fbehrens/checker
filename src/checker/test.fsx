// #I "../../.paket/load/net47"
#I "../../.paket/load/netcoreapp2.0"
#load "main.group.fsx"
open Expecto



#load"util.fs"
#load"regex.fs"
#load"subtitle.fs"
#load"file.fs"
#load"project.fs"

#load"../checkerTests/fileTests.fs"
#load"../checkerTests/projectTests.fs"
#load"../checkerTests/regexTests.fs"

#load"regex.fs"
#load"file.fs"
#load"project.fs"
open Checker.Project
projects
|> List.iter load

#load"../checkerTests/fileTests.fs"
open CheckerTests.File
runTests defaultConfig tests


