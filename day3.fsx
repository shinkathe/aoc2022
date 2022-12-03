let characterToPriority c =
    if int c >= 97 then int c - 96 else int c - 38

let findIntersections = Array.map Set.ofArray >> Set.intersectMany >> Set.toArray

let intersectOverRucksacks (rucksackCount: int) (e: int[][]) =
    e
    |> Array.chunkBySize rucksackCount
    |> Array.map (findIntersections >> Array.head)

let intersectInsideRucksack =
    Array.map (Array.splitInto 2) >> Array.map (findIntersections >> Array.head)

let rucksack =
    System.IO.File.ReadAllLines "inputs/day3.txt"
    |> Array.map (id >> Seq.toArray >> Array.map characterToPriority)

rucksack |> intersectInsideRucksack |> Array.sum |> printfn "Answer 1: %A"
rucksack |> intersectOverRucksacks 3 |> Array.sum |> printfn "Answer 2: %i"
