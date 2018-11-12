module TypeProvisor.FSharp

open TypeProvisor

let getTypeName =
    function
    | BaseType.Bool -> "bool"
    | BaseType.Int -> "int"
    | BaseType.Decimal -> "decimal"
    | BaseType.String _ -> "string"

let generateRecordProperty useOptions {Name=pn;BaseType=bt;IsOptional=isOpt;Comments=c}:Indentables=
    // works as long as there is no other column-type specific data (like lengthLimit,precision,scale)
    let canUseNullable = 
        function
        | BaseType.Bool
        | BaseType.Int
        | BaseType.Decimal -> true
        | _ -> false
    let t =
        if isOpt && not <| useOptions && canUseNullable bt then sprintf "%s Nullable"
        elif isOpt && useOptions then sprintf "%s option"
        // if the above isn't true we assume anything that makes it here accepts `null`
        else id
        |> fun f -> getTypeName bt |> f

    [
        yield! c
        yield 0,sprintf "%s:%s" pn t
    ]

let generateRecord useOptions {Name=rn;Properties=props;Comments=c} : Indentables =
    [
        yield! c |> List.map(IndentationImpl.prepend "// ")
        yield 0,sprintf "type %s = {" rn
        yield!
            props
            |> List.collect (generateRecordProperty useOptions>> List.map (IndentationImpl.indent 1))
        yield 0, "}"
    ]







