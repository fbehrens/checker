#load "../../.paket/load/netcoreapp2.0/Unquote.fsx"
open Swensen.Unquote
open System.Text.RegularExpressions
// https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

let isMatch s p = test <@ Regex.IsMatch(s,p) @>

let w = Regex "\w"
w.IsMatch("a") =! true

let isExactMatch s p =
    let m = Regex.Match( s, p )
    test <@ m.Success && m.Groups.[0].Value = s @>


let isNotMatch s p = test <@ Regex.IsMatch(s,p) |> not @>

let paMatchNot p ml nml =
  ml |> List.iter (fun s -> isMatch s p)
  nml |> List.iter (fun s -> isNotMatch s p)

isMatch "ab" "a"
isExactMatch "a" "a"

// escapes
isMatch "\n" "\n" // newline
isNotMatch "ab" "c"


paMatchNot "[ab]" ["a";"b"] ["c"] //    word
paMatchNot "[a-c]" ["a";"b";"c"] ["d"] //    word
paMatchNot "[^bc]" ["a";"d"] ["b";"c"] //    word


paMatchNot "\s" [" ";"\t";"\r";"\n"] ["."] //    whitespace
paMatchNot "\S" ["."] [" ";"\t"] // no 

paMatchNot "\w" ["a";"1"] ["."] //    word
paMatchNot "\W" ["."] ["a";"1"] // no word

paMatchNot "\d" ["0";"1"] ["a"] //    digit
paMatchNot "\D" ["a"] ["0";"1"] // no digit

// anchors
isMatch "ab" "^a" // beginning of line 
isMatch "ab" "b$" // end of line 
isMatch "ab" @"\ba" // word boundary 
isMatch "ab" @"\Bb" // no word boundary 


// one match
let Match s p = 
    let m = Regex.Match( s, p )
    [ for g in m.Groups -> g.Value ]
        
let Matches s p = 
  [ for m in Regex.Matches(s, p ) ->
      [ for g in m.Groups -> g.Value ] ]


// Grouping
let m1 = Regex.Match("abc","a(?<middle>.)c") // named captures (?<name>___)
m1.Groups.["middle"].Value =! "b"

Match "ab" "a(.)"  =! ["ab";"b"] 
Match "ab" "a(?:.)"  =! ["ab"] // none capturing group (?:___)

Match "acbd" ".(?=d)" = ["b"] // Lookahead (?=___)
Match "acbd" "[ab](?!c)" = ["b"] // negative Lookahead (?!___)

Match "acbd" "(?<=c)." = ["b"] // Lookbehind (?<=___)
Match "acbd" "(?<!a)[cd]" = ["b"] // negative Lookbehind (?<!___)


// Quantifiers
paMatchNot "ab*" ["a";"ab";"abb"] [] // * 0-n  
paMatchNot "ab+" ["ab";"abb"] ["a"]  // + 1-n
paMatchNot "ab?" ["a";"ab"] ["abb"]  // ? 0-1

// non greedy with ?
Match "ab" "a(b*)" =! ["ab";"b"]
Match "ab" "a(b*?)" =! ["a";""]

paMatchNot "_a{2}_" ["_aa_"] ["_a_";"_aaa_"]            // {n  } exactly n times
paMatchNot "_a{2,3}_" ["_aa_";"_aaa_"] ["_a_";"_aaaa_"] // {n,m} between n and m 
paMatchNot "_a{2,}_" ["_aa_";"_aaa_";"_aaaa_"] ["_a_"]  // {n, } min n
paMatchNot "_a{,2}_" ["_a_";"_aa_"] []                  // { ,m} maximal m 


// backreferences
Match "abccd" "(.)\1"  =! ["cc";"c"] // numbered
Match "abccd" "(?<name>.)\k<name>"  =! ["cc";"c"]  // named 

// Alternation
isExactMatch "a" "a|b" 
 

// Options https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-options
// n Do not capture unnamed groups
// s single-line mode: . matches also \r and \n

// i case-insensitive
isMatch "a" "(?i)A" // can be switched on in the midlle of a regex

// x Ignore unescaped white space and allow comments
isMatch "a" "(?x)    a   #coment" 

// m  multiline mode: ^ and $ match the beginning and end of a line (instead of string).  Attetion: use \r?$ to capture also CR
Matches "1\nb\n3\n" @"(?m)^\d$"  //=! [["1"];["3"]]  
Matches "1\r\nb\r\n3\r\n"  @"(?m)^\d\r?$" =! [["1\r"];["3\r"]]

// comment
isMatch "ab" "a(?# this is an inline comment)b" // can be switched on in the midlle of a regex

//collection
Matches "eabddacff" "a(.)" = [ ["ab";"b"]
                               ["ac";"c"]]

// Substitution
Regex.Replace("a","a","b") =! "b"
Regex.Replace("aa","a","b") =! "bb"
Regex.Replace("abc","a(.)c","_$1_") =! "_b_" // $1 replacement
Regex.Replace("abc","a(?<foo>.)c","_${foo}_") =! "_b_" // $1 replacement

//  MatchEvaluator: Match -> string
let bracket (m: Match) = sprintf "<%s>" m.Groups.[0].Value
Regex.Replace("_a_","a", bracket) =! "_<a>_"  

Regex.Replace("_a_","a", fun (m:Match) -> sprintf "<%s>" m.Groups.[0].Value ) =! "_<a>_"  // inline

// split
Regex.Split("a|b|c", @"\|") =! [| "a";"b";"c"|]

