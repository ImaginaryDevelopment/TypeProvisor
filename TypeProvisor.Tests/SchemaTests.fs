module SchemaTests

open Expecto
open TypeProvisor

type TrippingType ={Hello:string}
type MustFailType =
    | Trip of TrippingType
[<Tests>]
let x:Test =
    testList "SchemaTests" [
        testCase "Will not produce null for a record"
            <| fun () ->
                Expect.throws(fun () ->
                    BReusable.Unions.getDefaultValues<MustFailType>() |> ignore
                ) "should throw for record type"
        testCase "CanGenerateBaseTypes" (fun () ->
            printfn "I'm running"
            let things = BaseType.getDefaults()
            printfn "Found things! %A" things
            ()
        )
    ]

