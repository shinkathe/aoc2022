let split (separator: string) (s: string) = s.Split separator

let loss = 0
let draw = 3
let win = 6
let rock = 1
let paper = 2
let scissor = 3

let gameOutcomes =
    [| ("A X", rock + draw, scissor) // Rock vs Rock, lose
       ("A Y", paper + win, rock + draw) // Rock vs Paper, draw
       ("A Z", scissor, paper + win) // Rock vs Scissor, win
       ("B X", rock, rock) // Paper vs Rock, lose
       ("B Y", paper + draw, paper + draw) // Paper vs Paper, draw
       ("B Z", scissor + win, scissor + win) // Paper vs Scissor, win
       ("C X", rock + win, paper) // Scissor vs Rock, lose
       ("C Y", paper, scissor + draw) // Scissor vs Paper, draw
       ("C Z", scissor + draw, rock + win) |] // Scissor vs Scissor, win
    |> Array.groupBy (fun (a, _, _) -> a)
    |> dict

let game =
    System.IO.File.ReadAllText "inputs/day2.txt"
    |> split "\n"
    |> Array.map (fun ln -> gameOutcomes.Item(ln) |> Array.head)

let answer1 = game |> Array.sumBy (fun (_, strategy1, _) -> strategy1)
let answer2 = game |> Array.sumBy (fun (_, _, strategy2) -> strategy2)

printfn "Answer 1: %i" answer1
printfn "Answer 2: %i" answer2
