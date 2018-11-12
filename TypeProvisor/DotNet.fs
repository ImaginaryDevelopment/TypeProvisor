module TypeProvisor.DotNet

// thus far C# and F# use the same names for all
let getTypeName =
    function
    | BaseType.Bool -> "bool"
    | BaseType.Int -> "int"
    | BaseType.Decimal -> "decimal"
    | BaseType.String _ -> "string"

// works as long as there is no other column-type specific data (like lengthLimit,precision,scale)
let canUseNullable = 
    function
    | BaseType.Bool
    | BaseType.Int
    | BaseType.Decimal -> true
    | _ -> false
