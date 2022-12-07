open System

let startsWith (s: string) (w: string) = s.StartsWith(w)
let split (separator: string) (s: string) = s.Split separator

let commands =
    System.IO.File.ReadAllLines "inputs/day7.txt"
    |> (Array.map (split " "))
    |> Seq.toArray

let runcmd curr =
    function
    | [| "$"; "cd"; ".." |] -> Array.take (Array.length curr - 1) curr
    | [| "$"; "cd"; "/" |] -> [| "base" |]
    | [| "$"; "cd"; directory |] -> Array.append curr [| directory |]
    | [| _; _ |] -> curr

commands
|> Seq.fold
    (fun (cwd, folders) command ->
        let newFolders =
            match Int32.TryParse(Array.head command) with
            | true, num -> Array.append folders [| cwd, (num) |]
            | _ -> folders

        (runcmd cwd command, newFolders))
    ([| "base" |], [||])
|> snd
|> Array.groupBy (fst)
|> Array.map (fun (a, b) -> (String.Join("/", a), Array.sumBy (snd) b))
|> Array.fold
    (fun acc (dir, size) ->
        let recomputedAcc =
            acc
            |> Array.map (fun (subDirectory: string, subDirSize) ->
                subDirectory,
                if startsWith dir subDirectory then
                    subDirSize + size
                else
                    subDirSize)

        Array.concat [| [| (dir, size) |]; recomputedAcc |])
    [||]
|> Array.sumBy (fun (_, size) -> if size <= 100000 then size else 0)
|> printfn "%A"
