let toPair arr = Array.head arr, Array.last arr
let toSet = fun [| start1; end1; start2; end2 |] -> (set [ start1..end1 ], set [ start2..end2 ])
let isFullyContained = fun (a, b) -> Set.isSubset a b || Set.isSuperset a b
let setsAnyOverlap = fun sets -> (sets ||> Set.intersect) |> Seq.isEmpty |> not
let input =
    System.IO.File.ReadAllLines "inputs/day4.txt"
    |> Array.map ((fun s -> s.Split(',', '-')) >> Array.map int >> toSet)

input |> Array.countBy isFullyContained |> toPair |> snd |> snd |> printfn "Answer 1: %i"
input |> Array.countBy setsAnyOverlap |> toPair |> fst |> snd |> printfn "Answer 2: %i"
