let getPriority c =
    if int c >= 97 then int c - 96 else int c - 38

let findIntersections = Array.map Set.ofArray >> Set.intersectMany >> Set.toArray

let intersectByChunk (chunkSize: int) (e: int[][]) =
    e |> Array.chunkBySize chunkSize |> Array.map (findIntersections >> Array.head)

let existsTwice =
    Array.map (Array.splitInto 2) >> Array.map (findIntersections >> Array.head)

let rucksack =
    System.IO.File.ReadAllLines "inputs/day3.txt"
    |> Array.map (id >> Seq.toArray >> Array.map getPriority)

rucksack |> existsTwice |> Array.sum |> printfn "Answer 1: %A"
rucksack |> intersectByChunk 3 |> Array.sum |> printfn "Answer 2: %i"
