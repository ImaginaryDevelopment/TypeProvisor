// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
#r "bin/Debug/TypeProvisor.dll"
open TypeProvisor

let sampleMeta = {  Name="Patient"
                    Properties= [
                        {Name="PatientId"; BaseType=BaseType.Int;IsOptional=false;Comments=List.empty;Cardinality=Some IAmTheParent}
                        {Name="FirstName"; BaseType=BaseType.String (Some 50);IsOptional=false;Comments=List.empty;Cardinality=None}
                    ]
                    Comments=[
                        0, "Hello comments"
                    ]
                }


#r "System.Windows.Forms"
type Clipboard = System.Windows.Forms.Clipboard
#r @"../packages/Newtonsoft.Json.11.0.2/lib/net45/Newtonsoft.Json.dll"
open Newtonsoft.Json
JsonConvert.SerializeObject [sampleMeta]
|> Clipboard.SetText
// Define your library scripting code here

