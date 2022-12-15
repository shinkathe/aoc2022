open System.Text.RegularExpressions
#time

type Monkey = {
  MonkeyNumber: int
  StartingItems: int64 list
  Operation: string list
  Test: int64
  IfTrue: int
  IfFalse: int
  Inspections: int64
}

let regex = new Regex(@"
Monkey\s(?<id>\d+):\s*
  Starting\sitems:\s(?<starting_items>.*)\s*
  Operation:\snew\s=\s(?<opType>.*)\s*
  Test:\sdivisible\sby\s(?<test_value>\d+)\s*
    If\strue:\sthrow\sto\smonkey\s(?<true_id>\d+)\s*
    If\sfalse:\sthrow\sto\smonkey\s(?<false_id>\d+)
", RegexOptions.IgnorePatternWhitespace)


let success (mts: Match) =
  {
      MonkeyNumber = mts.Groups.["id"].Value |> int
      StartingItems = mts.Groups.["starting_items"].Value |> fun s -> s.Split(", ") |> Array.toList |> List.map (int64)
      Operation = (mts.Groups.["opType"].Value.Trim().Split(" ") |> Array.toList)
      Test = mts.Groups.["test_value"].Value |> int64
      IfTrue = mts.Groups.["true_id"].Value |> int
      IfFalse = mts.Groups.["false_id"].Value |> int
      Inspections = 0
  }

let runOp (old: int64) = function
  |  ["old"; "+"; "old"] -> old + old
  |  ["old"; "*"; "old"] -> old * old
  |  ["old"; "+"; h] -> old + int64 h
  |  ["old"; "*"; h] -> old * int64 h

let monkeyInit = regex.Matches(System.IO.File.ReadAllText "inputs/day11t.txt") |> Seq.toArray |> Seq.map (success) |> Seq.toArray

let gcf = monkeyInit |> Array.map (fun f -> f.Test) |> Array.reduce (*)

let round monkeys =
  monkeys |> Array.fold (fun (acc: Monkey array) monkey ->
    monkey.StartingItems |> List.fold (fun (startingItems: list<int64>) inspect ->
      let newWorryLevel = (runOp inspect monkey.Operation) % gcf
      let targetMonkey = if (newWorryLevel % monkey.Test) = 0 then monkey.IfTrue else monkey.IfFalse
      let sourceMonkeyList = List.tail (startingItems)
      let targetMonkeyList = (acc[targetMonkey].StartingItems) @ [newWorryLevel]
      acc[targetMonkey] <- {acc[targetMonkey] with StartingItems = targetMonkeyList}
      acc[monkey.MonkeyNumber] <- {acc[monkey.MonkeyNumber] with StartingItems = sourceMonkeyList; Inspections = acc[monkey.MonkeyNumber].Inspections + int64 1}
      sourceMonkeyList
    ) monkey.StartingItems |> ignore
    acc
  ) monkeys

let runRounds till init = [|1..till|] |> Array.fold (fun acc _ -> (round acc)) init
let calculateMonkeyBusiness = Array.map (fun f -> f.Inspections) >> Array.sortDescending >> Array.take(2) >> Array.reduce ((*))

runRounds 20 monkeyInit |> calculateMonkeyBusiness |> printfn "Answer1: %A"
runRounds 10000 monkeyInit |> calculateMonkeyBusiness |> printfn "Answer2: %A"
