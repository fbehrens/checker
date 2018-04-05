// #I "../../.paket/load/net47"
#I "../../.paket/load/netcoreapp2.0"
#load "unquote.fsx"
open Swensen.Unquote

open System
Environment.GetCommandLineArgs()  // =! [|"/Library/Frameworks/Mono.framework/Versions/5.8.0/lib/mono/fsharp/fsi.exe"; "--exename:fsharpi"|]
Environment.UserName =! "fb"
Environment.Is64BitProcess =! true
Environment.MachineName =! "mbp.fritz.box"
Environment.OSVersion
Environment.StackTrace
Environment.UserDomainName
Environment.CurrentDirectory

open System.IO
let p = "ab/c/def.txt"
Path.GetExtension(p) =! ".txt"
Path.ChangeExtension(p,"csv") =! "ab/c/def.csv"
Path.GetDirectoryName(p) =! "ab/c"
Path.GetFullPath(p)
Path.DirectorySeparatorChar =! '/'






