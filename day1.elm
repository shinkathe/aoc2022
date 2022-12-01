import Html exposing (text)
import String exposing (split, toInt, join, fromInt, concat)
import List exposing (map, sum, sort, reverse, head, take, foldl)
import Debug exposing (log, toString)
import Maybe exposing (withDefault)

input =
  """6529
8085
4534
1503
2983

15219
7137
2691
2898
1798

15219
7137
2691
2898
1798

15219
7137
2691
2898
1798"""

elvesByCalories = split "\r\n\r\n" >> map (split "\r\n" >>  map (toInt >> withDefault 0))

elvesByCaloriesSorted = elvesByCalories >> map sum >> sort >> reverse

answer1 = elvesByCaloriesSorted >> head >> withDefault 0 >> toString
answer2 = elvesByCaloriesSorted >> take 3 >> sum >> toString

main =
  text (concat ["Answer 1: ", (answer1 input), "\n Answer2: ", (answer2 input)])
