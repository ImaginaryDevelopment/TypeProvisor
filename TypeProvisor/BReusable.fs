module BReusable
let mutable debug = true
open System
    module StringPatterns =
        let isValueString = String.IsNullOrWhiteSpace >> not
    module Unions =
        open System.Reflection
        open Microsoft.FSharp.Reflection
        open System.Collections.Generic

        let getCases<'t>() =
            FSharpType.GetUnionCases(typeof<'t>)
        let acceptsDefaultCaseField typeName caseName (x:PropertyInfo) =
            let r =
                x.PropertyType.IsPrimitive
                || x.PropertyType.IsByRef
                || (x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() = typedefof<Option<_>>)
            if debug then
                printfn "%s.%s.%s:%s acceptsDefaultCaseField?%b" typeName caseName x.Name x.PropertyType.Name r
            r
        let getDefaultValues<'t>() =
            let (|AllDefaultable|_|) caseName =
                function
                | [| |] -> None
                | x when Array.forall(fun y -> acceptsDefaultCaseField typeof<'t>.Name caseName y) x -> x |> Array.map(fun _ -> null) |> Some
                | _ -> None

            getCases<'t>()
            |> Seq.map(fun c ->
                match c.GetFields() with
                | [| |] -> FSharpValue.MakeUnion(c, Array.empty)
                | AllDefaultable c.Name x -> FSharpValue.MakeUnion(c, x)
                | _ -> failwithf "Unsupported fields exist for %s.%s" typeof<'t>.Name c.Name
                :?> 't
            )
            |> List.ofSeq
            :> IReadOnlyList<_>


