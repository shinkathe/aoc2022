type Pos = {x: int; y: int}
type Acc = { head: Pos; tail: Pos; visited: array<Pos>}
let currCoords: Acc = {head={x=0; y=0}; tail={x=0; y=0}; visited =[||]}
let move = function
    | "D" -> (0, -1)
    | "L" -> (-1, 0)
    | "R" -> (1, 0)
    | "U" -> (0, 1)

let calculateGridOffset x1 y1 x2 y2  = x2 - x1 , y2 - y1

let computeMovement (posFrom: Pos) (posTo: Pos) = calculateGridOffset posFrom.x posFrom.y posTo.x posTo.y |> (fun (a, b) -> if (sqrt((double a)**2 + (double b)**2)) >= 2 then {x = int(ceil((double a) / 2.0)); y =  int(ceil((double b)/2.0))} else {x = 0; y= 0}  )
let operations =
    System.IO.File.ReadAllLines "inputs/day9.txt"
    |> Seq.map ((fun ln -> ln.Split " ") >> fun ([|dir; amount|]) -> (dir, int amount))
    |> Seq.toArray
    |> Array.fold (fun acc (dir, amount) ->
        // printfn "dir %A + amount %A" dir amount
        printfn "%A %A" dir amount
        [| 1..amount |] |> Array.fold (fun innerAcc _ ->
            let newPos = move dir |> fun (moveX, moveY) -> {x = innerAcc.head.x + moveX; y = innerAcc.head.y + moveY}
            let newTailPos = computeMovement innerAcc.tail newPos |> fun movement -> {x = innerAcc.tail.x + movement.x; y = innerAcc.tail.y + movement.y}
            // printfn "newtail %A %A" newPos newTailPos
            {head = newPos; tail = newTailPos; visited = Array.concat [|innerAcc.visited; [|newTailPos|]|]}
        ) acc
    ) currCoords
    |> fun (f) -> f.visited |> Array.groupBy (fun f ->  $"{f.x}{f.y}") |> Array.length |> printfn "%A"
