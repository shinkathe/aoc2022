let input =
    System.IO.File.ReadAllText "inputs/day1.txt"
    |> fun file -> file.Split "\n\n"
    |> Array.map ((fun calorieList -> calorieList.Split "\n") >> Array.map int)

let elvesSortedByCalories = input |> Array.map (Array.sum) |> Array.sortDescending

let elfWithMostCalories = elvesSortedByCalories |> Array.head

printfn "Answer 1: %i" elfWithMostCalories

let topThreeElfCalories = elvesSortedByCalories |> Array.take 3 |> Array.sum

printfn "Answer 2: %i" topThreeElfCalories
