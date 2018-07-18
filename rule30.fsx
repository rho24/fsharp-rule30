let halfWidth = 50
open System.Text
let firstRow = Array.concat [Array.zeroCreate<int> halfWidth; [|1|]; Array.zeroCreate halfWidth]

let rule30 = function
    | [|1;0;0|]
    | [|0;1;1|]
    | [|0;1;0|]
    | [|0;0;1|] -> 1
    | _ -> 0

let printRow row = 
    row
    |> Array.map (fun e -> if e = 1 then '█' else ' ')
    |> System.String

printRow firstRow

let generateRow (prevRow: int[]) = 
    Array.concat [
        [|0|];
        prevRow
            |> Seq.windowed 3
            |> Seq.map rule30
            |> Seq.toArray
        [|0|];
    ]

firstRow |> generateRow |> printRow

let rows = 
    [|0..halfWidth|]
    |> Array.scan (fun row _ -> generateRow row) firstRow

let output = new StringBuilder("\n")

rows
    |> Array.map (printRow)
    |> Array.iter (Printf.bprintf output "%s\n")
