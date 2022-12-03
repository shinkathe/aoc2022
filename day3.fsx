let characterToPriority c =
    if int c >= 97 then int c - 96 else int c - 38

let intersectInsideRucksack rucksack =
    rucksack
    |> Seq.map (Seq.splitInto 2 >> Seq.map set >> Set.intersectMany >> Seq.head)

let intersectOverRucksacks rucksacks =
    rucksacks
    |> Seq.chunkBySize 3
    |> Seq.map (Seq.map set >> Set.intersectMany >> Seq.head)

let rucksack =
    System.IO.File.ReadAllLines "inputs/day3.txt"
    |> Seq.map (Seq.map characterToPriority)

rucksack |> intersectInsideRucksack |> Seq.sum |> printfn "Answer 1: %A"
rucksack |> intersectOverRucksacks |> Seq.sum |> printfn "Answer 2: %A"
