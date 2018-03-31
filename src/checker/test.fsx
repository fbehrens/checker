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




#load"project.fs"
Checker.Project.projects






#load"../checkerTests/regexTests.fs"
open CheckerTests.Regex
runTests defaultConfig tests


