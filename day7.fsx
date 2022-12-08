open System
let commands = System.IO.File.ReadAllLines "inputs/day7.txt" |> (Array.map (fun s -> s.Split " "))

let cmd cwd = function
    | [| "$"; "cd"; ".." |] -> Array.take (Array.length cwd - 1) cwd
    | [| "$"; "cd"; "/" |] -> [| "base" |]
    | [| "$"; "cd"; directory |] -> Array.append cwd [| directory |]
    | [| _; _ |] -> cwd

let foldersBySize =
    commands
    |> Seq.fold
        (fun (cwd, folders) command ->
            cmd cwd command, 
            match Int32.TryParse(Array.head command) with
                | true, num -> Array.append folders [| cwd, (num) |]
                | _ -> Array.append folders [| cwd, (0) |])
       ([| "base" |], [||])
    |> snd
    |> Array.groupBy (fst)
    |> Array.map (fun (path, sizes) -> (String.Join("/", path), Array.sumBy (snd) sizes))
    |> Array.fold (fun acc (dir, size) ->
            let sizesCombined = acc |> Array.map (fun (subDir: string, subDirSize) -> subDir, if dir.StartsWith(subDir) then subDirSize + size else subDirSize)
            Array.concat [| [| (dir, size) |]; sizesCombined |]) [||]

printfn "Answer 1: %i" (foldersBySize |> Array.sumBy (fun (_, size) -> if size <= 100000 then size else 0))
printfn "Answer 2: %A" (foldersBySize |> Array.where (snd >> (<=) (foldersBySize |> 
    Array.find (fun (a, _) -> a = "base") |> (snd >> (+) 30000000 >> (+) -70000000))) |> Array.sortBy (snd) |> Array.head)