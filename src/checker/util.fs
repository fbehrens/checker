module Checker.Util
    let memoize f =
        let cache = System.Collections.Generic.Dictionary<_, _>()
        fun x ->
              if cache.ContainsKey(x) then cache.[x]
              else let res = f x
                   cache.[x] <- res
                   res


