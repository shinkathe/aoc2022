let fst3 (a, _, _) = a
let snd3 (_, b, _) = b
let thrd3 (_, _, c) = c

let draw = 3
let win = 6
let rock = 1
let paper = 2
let scissor = 3

let RPSMoveOutcomes =
    [| ("A X", rock + draw, scissor) // Rock vs Rock, lose
       ("A Y", paper + win, rock + draw) // Rock vs Paper, draw
       ("A Z", scissor, paper + win) // Rock vs Scissor, win
       ("B X", rock, rock) // Paper vs Rock, lose
       ("B Y", paper + draw, paper + draw) // Paper vs Paper, draw
       ("B Z", scissor + win, scissor + win) // Paper vs Scissor, win
       ("C X", rock + win, paper) // Scissor vs Rock, lose
       ("C Y", paper, scissor + draw) // Scissor vs Paper, draw
       ("C Z", scissor + draw, rock + win) |] // Scissor vs Scissor, win
    |> Array.groupBy fst3
    |> dict

let gamePlayedByStrategies =
    System.IO.File.ReadAllLines "inputs/day2.txt"
    |> Array.map (fun move -> RPSMoveOutcomes.[move] |> Array.head)

let answer1 = gamePlayedByStrategies |> Array.sumBy snd3
let answer2 = gamePlayedByStrategies |> Array.sumBy thrd3

printfn "Answer 1: %i" answer1
printfn "Answer 2: %i" answer2
