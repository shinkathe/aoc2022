open System
let input = System.IO.File.ReadAllLines "inputs/day5.txt"

let startingBuckets = input |> Array.take 9

let instructions =
    input
    |> Array.skip 10 |> Array.map (fun s ->
        s.Split([| "move "; " from "; " to " |], StringSplitOptions.RemoveEmptyEntries)
        |> Array.map (int >> fun s -> s - 1))

let buckets =
    [| 1..4 .. 4 * 9 |]
    |> Array.map (
        (fun column -> [| 0 .. 8 |] |> Array.map (fun i -> startingBuckets[i][column])) >> Array.where Char.IsUpper
    )

let runCrateMover900 =
    instructions
    |> Array.fold (fun (acc: char[][]) ([| amount; from; target |]) ->
        let targetNewLine = Array.concat [| acc[from][0..amount] |> Array.rev; acc[target] |] // Remove rev for answer 2
        let sourceNewLine = acc[from][amount + 1 .. acc[from].Length]
        let midState = Array.updateAt from sourceNewLine acc
        Array.updateAt target targetNewLine midState) buckets
    |>  Array.map (Array.head) |> String |> printfn "Answer 1: %s"
