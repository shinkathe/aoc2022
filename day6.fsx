let input = System.IO.File.ReadAllText "inputs/day6.txt"

input |> Seq.windowed 4 |> Seq.findIndex (Array.distinct >> Array.length >> (=) 4) |> (+) 4 |> printfn "Answer 1: %i"
input |> Seq.windowed 14 |> Seq.findIndex (Array.distinct >> Array.length >> (=) 14) |> (+) 14 |> printfn "Answer 2: %i"
