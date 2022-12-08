open System

let trees = System.IO.File.ReadAllLines "inputs/day8.txt" |> Array.map (Seq.toArray >> Array.map (fun c -> Int32.Parse(c.ToString()))) |> array2D
let skipLast a = a |> Array.take (a.Length - 1)

let findClosest elements curr = match elements |> Array.tryFindIndex (fun f -> f >= curr) with | Some x -> x + 1 | None -> elements.Length
let canSeeOutside elements curr = match elements |> Array.tryFindIndex (fun f -> f >= curr) with | Some x -> false | None -> true

let distanceToClosestBlockingLookLeft x y fn = fn (trees[y, 0..x] |> skipLast |> Array.rev) trees[y, x]
let distanceToClosestBlockingLookUp x y  fn = fn (trees[0..y, x] |> skipLast |> Array.rev) trees[y, x]
let distanceToClosestBlockingLookRight x y  fn = fn (trees[y, x..(trees[y, *].Length)] |> Array.tail) trees[y, x]
let distanceToClosestBlockingLookDown x y  fn = fn (trees[y..(trees[*, x].Length), x] |> Array.tail) trees[y, x]

trees |> Array2D.mapi (fun i j v -> distanceToClosestBlockingLookLeft i j canSeeOutside || distanceToClosestBlockingLookRight i j canSeeOutside || distanceToClosestBlockingLookDown i j canSeeOutside || distanceToClosestBlockingLookUp i j canSeeOutside)
    |> Seq.cast<bool> |> Seq.where((=) true) |> Seq.length |> printfn "Answer1 : %A"

trees |> Array2D.mapi (fun i j v -> distanceToClosestBlockingLookLeft i j findClosest * distanceToClosestBlockingLookRight i j findClosest * distanceToClosestBlockingLookDown i j findClosest * distanceToClosestBlockingLookUp i j findClosest)
    |> Seq.cast<int> |> Seq.sortDescending |> Seq.head |> printfn "Answer2 : %A"
