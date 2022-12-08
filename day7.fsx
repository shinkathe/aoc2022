open System

let startsWith (s: string) (w: string) = s.StartsWith(w)
let split (separator: string) (s: string) = s.Split separator
let commands = System.IO.File.ReadAllLines "inputs/day7.txt" |> (Array.map (split " "))

let cmd cwd = function
    | [| "$"; "cd"; ".." |] -> Array.take (Array.length cwd - 1) cwd
    | [| "$"; "cd"; "/" |] -> [| "base" |]
    | [| "$"; "cd"; directory |] -> Array.append cwd [| directory |]
    | [| _; _ |] -> cwd

let foldersBySize =
    commands
    |> Seq.fold
        (fun (cwd, folders) command ->
            let currentDir = cmd cwd command
            let newFolders =
                match Int32.TryParse(Array.head command) with
                | true, num -> Array.append folders [| currentDir, (num) |]
                | _ -> Array.append folders [| currentDir, (0) |]
            (currentDir, newFolders))
        ([| "base" |], [||])
    |> snd
    |> Array.groupBy (fst)
    |> Array.map (fun (path, sizes) -> (String.Join("/", path), Array.sumBy (snd) sizes))
    |> Array.fold
        (fun acc (dir, size) ->
            let recomputedAcc =
                acc
                |> Array.map (fun (subDirectory: string, subDirSize) ->
                    subDirectory,
                    if startsWith dir subDirectory then subDirSize + size else subDirSize)
            Array.concat [| [| (dir, size) |]; recomputedAcc |])
        [||]

printfn "Answer 1: %i" (foldersBySize |> Array.sumBy (fun (_, size) -> if size <= 100000 then size else 0))

let limit = foldersBySize |> Array.find (fun (a, _) -> a = "base") |> (snd >> (+) 30000000 >> (+) -70000000)
printfn "Answer 2: %A" (foldersBySize |> Array.where (snd >> (<=) limit) |> Array.sortBy (snd) |> Array.head)