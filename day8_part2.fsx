open System

let input =
    System.IO.File.ReadAllLines "inputs/day8.txt"
    |> Array.map (Seq.toArray >> Array.map (fun c -> Int32.Parse(c.ToString())))

let arr = array2D input
let skipLast a = a |> Array.take (a.Length - 1)

let findClosest elements curr = match elements |> Array.tryFindIndex (fun f -> f >= curr) with | Some x -> x + 1 | None -> elements.Length

let distanceToClosestBlockingLookLeft x y = findClosest (arr[y, 0..x] |> skipLast |> Array.rev) arr[y, x]
let distanceToClosestBlockingLookUp x y = findClosest (arr[0..y, x] |> skipLast |> Array.rev) arr[y, x]
let distanceToClosestBlockingLookRight x y = findClosest (arr[y, x..(arr[y, *].Length)] |> Array.tail) arr[y, x]
let distanceToClosestBlockingLookDown x y = findClosest (arr[y..(arr[*, x].Length), x] |> Array.tail) arr[y, x]

arr
|> Array2D.mapi (fun i j v -> distanceToClosestBlockingLookLeft i j * distanceToClosestBlockingLookRight i j * distanceToClosestBlockingLookDown i j * distanceToClosestBlockingLookUp i j)
    |> Seq.cast<int> |> Seq.sortDescending |> Seq.head |> printfn "Answer2 : %A"
