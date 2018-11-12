module BReusable
open System
    module StringPatterns =
        let isValueString = String.IsNullOrWhiteSpace >> not

