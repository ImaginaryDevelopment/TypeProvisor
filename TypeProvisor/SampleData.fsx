// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
#r "bin/Debug/TypeProvisor.dll"
open TypeProvisor

let sampleMeta = 
    [
        {   Name="Patient"
            Properties= [
                {Name="PatientId"; BaseType=BaseType.Int;IsOptional=false;Comments=List.empty;Cardinality=Some IAmTheParent}
                {Name="PayerProfileId"; BaseType=BaseType.Int;IsOptional=true;Comments=List.empty;Cardinality=Some <| IAmAChild("PayerProfile","PayerProfileId")}
            ]
            Comments=[
                0, "Hello comments"
            ]
        }
        {   Name="PatientInfo"
            Comments=List.empty
            Properties= [
                {Name="PatientInfoId"; BaseType=BaseType.Int;IsOptional=false; Comments=List.empty; Cardinality = Some IAmTheParent}
                {Name="PatientId"; BaseType=BaseType.Int;IsOptional=false;Comments=List.empty;Cardinality=Some <| IAmAChild ("Patient","PatientId")}
                {Name="FirstName"; BaseType=BaseType.String (Some 50);IsOptional=false;Comments=List.empty;Cardinality=None}

            ]
        }

    ]


#r "System.Windows.Forms"
type Clipboard = System.Windows.Forms.Clipboard
#r @"../packages/Newtonsoft.Json.11.0.2/lib/net45/Newtonsoft.Json.dll"
open Newtonsoft.Json
let potential = JsonConvert.SerializeObject sampleMeta

potential
|> Clipboard.SetText
// Define your library scripting code here

