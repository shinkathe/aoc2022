open System

let trees = System.IO.File.ReadAllLines "inputs/day8.txt" |> Array.map (Seq.toArray >> Array.map (fun c -> Int32.Parse(c.ToString()))) |> array2D
let skipLast a = a |> Array.take (a.Length - 1)

let findClosest elements curr = match elements |> Array.tryFindIndex (fun f -> f >= curr) with | Some x -> x + 1 | None -> elements.Length

let distanceToClosestBlockingLookLeft x y = findClosest (trees[y, 0..x] |> skipLast |> Array.rev) trees[y, x]
let distanceToClosestBlockingLookUp x y = findClosest (trees[0..y, x] |> skipLast |> Array.rev) trees[y, x]
let distanceToClosestBlockingLookRight x y = findClosest (trees[y, x..(trees[y, *].Length)] |> Array.tail) trees[y, x]
let distanceToClosestBlockingLookDown x y = findClosest (trees[y..(trees[*, x].Length), x] |> Array.tail) trees[y, x]

trees |> Array2D.mapi (fun i j v -> distanceToClosestBlockingLookLeft i j * distanceToClosestBlockingLookRight i j * distanceToClosestBlockingLookDown i j * distanceToClosestBlockingLookUp i j)
    |> Seq.cast<int> |> Seq.sortDescending |> Seq.head |> printfn "Answer2 : %A"
