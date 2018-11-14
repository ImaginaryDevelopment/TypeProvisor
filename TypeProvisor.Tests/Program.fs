open Expecto

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    let config = defaultConfig
    let r = Tests.runTestsInAssembly config argv
    //let r = Tests.runTests config (testList "SchemaTests" SchemaTests.x)
    #if !INTERACTIVE
    System.Console.ReadLine() |> ignore
    #endif
    r
