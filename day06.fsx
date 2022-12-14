let input = System.IO.File.ReadAllText "inputs/day6.txt"
let firstDistinct len =  Seq.windowed len >> Seq.findIndex (Array.distinct >> Array.length >> (=) len) >> (+) len
input |> firstDistinct 4 |> printfn "Answer 1: %i"
input |> firstDistinct 14 |> printfn "Answer 2: %i"
