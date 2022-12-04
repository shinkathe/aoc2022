let split (separator: string) (s: string) = s.Split separator

let toPair arr = Array.head arr, Array.last arr

let isFullyContained (start1, end1) (start2, end2) =
    (set [ start1..end1 ], set [ start2..end2 ])
    |> fun (a, b) -> Set.isSubset a b || Set.isSuperset a b

let setsAnyOverlap (start1, end1) (start2, end2) =
    (set [ start1..end1 ], set [ start2..end2 ])
    ||> Set.intersect
    |> Seq.isEmpty
    |> not

let input =
    System.IO.File.ReadAllLines "inputs/day4.txt"
    |> Array.map (split "," >> Array.map (split "-" >> Array.map int >> toPair))

input
|> Array.sumBy (fun pair -> isFullyContained pair[0] pair[1] |> System.Convert.ToInt32)
|> printfn "Answer 1 %i"

input
|> Array.sumBy (fun pair -> setsAnyOverlap pair[0] pair[1] |> System.Convert.ToInt32)
|> printfn "Answer 2 %i"
