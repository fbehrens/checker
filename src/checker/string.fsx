#load "../../.paket/load/netcoreapp2.0/Unquote.fsx"
open Swensen.Unquote

"1\
2" =! "12" // qoting newline

"1
2" =! "1\n2"

test <@ "\n" <> @"\n" @> // @"verbatim"
@"say \""hi"" " =! """say \"hi" """ // """triple"""


let s = "abcd"
s.[0]    =! 'a' // index
s.[1..2] =! "bc" // substring
[ for c in s -> c ] =! ['a';'b';'c';'d'] // enumerate

" a ".Trim() =! "a"
 
s.IndexOf("bc") =! 1


"a".PadLeft(3,'-') =! "--a"
