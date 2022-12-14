open System;
let concat (s: string[]) = String.Join("", s)
let parseOrDefault (defaultTo: int) (str: string) = match System.Int32.TryParse str with | true, int -> int | _ -> defaultTo

let instructions = System.IO.File.ReadAllLines "inputs/day10_r.txt" |> (Array.map (fun s -> s.Split " " |> (Array.last >> parseOrDefault 0)))

let cycles =
    instructions
     |> Array.fold (fun acc instruction ->
        let index, register = Array.last acc
        Array.concat (match instruction with
            | 0 -> [|acc; [|(index + 1, register)|]|]
            | x -> [|acc; [|(index + 1, register)|]; [|(index + 2), register + x|]|])
        ) [|(1, 1)|]

[|20;60;100;140;180;220|] |> Array.sumBy (fun i -> (cycles[i-1] |> snd) * i) |> printfn "Answer 1: %A"
cycles |> Array.chunkBySize 40 |> Array.map (Array.mapi (fun col (_, x) -> if (x=col-1 || x=col || x=col+1) then "#" else ".")) |> Array.map (concat >> printfn "%A")
