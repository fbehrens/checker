open Argu
open Checker

type SrtArgs =
    | [<Mandatory; MainCommand>] File of file:string
    | [<Mandatory; AltCommandLine("-s")>] Sec of sec:float
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | File _ -> "specify a File"
            | Sec _ -> "Specify Seconds to shift"
 
type BlaArgs =
    | Foo of foo:string
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Foo _ -> "specify Foo"

type CheckArgs =
    | Version of version:string
    | [<CliPrefix(CliPrefix.None)>] Srt of ParseResults<SrtArgs>
    | [<CliPrefix(CliPrefix.None)>] Bla of ParseResults<BlaArgs>

with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Version _ -> "current Version"
            | Srt _ -> "Shift subtitle"
            | Bla _ -> "Bla Command"

let parser = ArgumentParser.Create<CheckArgs>(programName = "check.exe",
                                              helpTextMessage = "help was requested ")
 
[<EntryPoint>]
let main argv =
    let results = parser.Parse(argv, raiseOnUsage = false)
    if results.IsUsageRequested then printfn "%s" (parser.PrintUsage())
    else 
        match results.GetSubCommand() with 
        | Srt r -> 
            if r.IsUsageRequested then printfn "%s" (r.Parser.PrintUsage())
            else 
                let file = r.GetResult (<@ File @>)
                let sec = r.GetResult (<@ Sec @>)
                printfn "file=%s sec=%f" file sec
                Subtitle.fileShift sec file

        | Bla r -> () 
        | Version _ -> ()

    0 
