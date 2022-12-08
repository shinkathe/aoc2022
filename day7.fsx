open System

let startsWith (s: string) (w: string) = s.StartsWith(w)
let split (separator: string) (s: string) = s.Split separator

let commands =
    System.IO.File.ReadAllLines "inputs/day7.txt" |> (Array.map (split " "))

let runcmd curr =
    function
    | [| "$"; "cd"; ".." |] -> Array.take (Array.length curr - 1) curr
    | [| "$"; "cd"; "/" |] -> [| "base" |]
    | [| "$"; "cd"; directory |] -> Array.append curr [| directory |]
    | [| a; b |] -> curr

let intermediate =
    commands
    |> Seq.fold
        (fun (cwd, folders) command ->
            let currentDir = runcmd cwd command
            let newFolders =
                match Int32.TryParse(Array.head command) with
                | true, num -> Array.append folders [| currentDir, (num) |]
                | _ -> Array.append folders [| currentDir, (0) |]
            (currentDir, newFolders))
        ([| "base" |], [||])
    |> snd

let intermediate2 =
    intermediate
    |> Array.groupBy (fst)
    |> Array.map (fun (a, b) -> (String.Join("/", a), Array.sumBy (snd) b))
    |> Array.fold
        (fun acc (dir, size) ->
            let recomputedAcc =
                acc
                |> Array.map (fun (subDirectory: string, subDirSize) ->
                    subDirectory,
                    if startsWith dir subDirectory then subDirSize + size else subDirSize)

            Array.concat [| [| (dir, size) |]; recomputedAcc |])
        [||]

printfn "Answer 1: %i" (intermediate2 |> Array.sumBy (fun (_, size) -> if size <= 100000 then size else 0))

let limit =
    intermediate2 |> Array.find (fun (a, _) -> a = "base") |> (snd >> (+) 30000000 >> (+) -70000000)

intermediate2
|> Array.where (snd >> (<=) limit) |> Array.sortBy (snd) |> Array.head |> printfn "Answer 2: %A"