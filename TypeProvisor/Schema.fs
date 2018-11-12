namespace TypeProvisor
open System
type Indentable = int * string
type Indentables = Indentable list
type Comments = Indentables

[<RequireQualifiedAccess>]
type BaseType =
    | Bool
    | Int
    | String of lengthLimit:int option
    | Decimal


type Cardinality =
    // this property is 
    | IAmAChild of parentTypeName:string * parentPropertyName:string
    | IAmTheParent

// represent a base .net type, and enable transformations to other type systems (sql, js, ts)
// require magic string or typeof<'t> ?
type Property = {Name:string; BaseType:BaseType;IsOptional:bool; Comments:Comments;Cardinality: Cardinality option}

// for class/interface/abstract (doesn't currently support enum or DU)
type TypeMeta = {Name:string; Properties:Property list;Comments:Comments}

module IndentationImpl =
    let indent i (j,x) = i+j,x
    let toString indentation (i,s) =
        let x = String.replicate i indentation
        sprintf "%s%s" x s
    let prepend toPrepend (i,s) =
        i,sprintf "%s%s" toPrepend s

