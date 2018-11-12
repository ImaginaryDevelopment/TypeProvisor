module TypeProvisor.CSharp
open TypeProvisor
open TypeProvisor.DotNet
open System

// not INPC capable, and do not attempt to promote it
let generateProperty writable {Name=pn;BaseType=bt;IsOptional=isOpt;Comments=c}:Indentables=
    let t = 
        if isOpt && canUseNullable bt then sprintf "%s?"
        else id
        |> fun f -> getTypeName bt |> f
    [
        yield! c
        if writable then
            yield 0, sprintf "public %s %s {get;set;}" t pn
        else yield 0, sprintf "public %s %s {get;private set;}" t pn
    ]
// not INPC capable, and do not attempt to promote it
let generateClass writable {Name=rn;Properties=props;Comments=c} : Indentables =
    [
        yield! c |> List.map(IndentationImpl.prepend "//")
        yield 0, sprintf "public class %s" rn
        yield 0, "{"
        yield!
            props
            |> List.collect(generateProperty writable >> List.map (IndentationImpl.indent 1))
        yield 0, "}"
    ]
// whole file
let generateClassFile targetNamespace writable c =
    [
        yield 0,"using System;"
        yield 0, Environment.NewLine
        yield 0, sprintf "namespace %s" targetNamespace
        yield 0, "{"
        yield! generateClass writable c |> List.map(IndentationImpl.indent 1)
        yield 0, "}"
    ]
