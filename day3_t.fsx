// puzzle: https://adventofcode.com/2022/day/3
#time





let priority = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
let rucksacks = "inputs/day3.txt" |> System.IO.File.ReadAllLines

let findIntersections = Seq.map set >> Set.intersectMany >> Set.maxElement

rucksacks
|> Seq.sumBy (Seq.splitInto 2 >> findIntersections >> priority.IndexOf)
|> printfn "Part 1: %i"

rucksacks
|> Seq.chunkBySize 3
|> Seq.sumBy (findIntersections >> priority.IndexOf)
|> printfn "Part 2: %i"
