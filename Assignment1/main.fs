module Main

let factorial x = 
  let rec positiveFactorialTR acc x =
    match x with
      | 0 -> acc
      | x when x > 0 -> positiveFactorialTR (acc * x) (x - 1)
      | _ -> raise (System.Exception())
  match x with
    | x when x < 0 -> None
    | 0 -> Some 1
    | x -> Some (positiveFactorialTR 1 x)

let getNthFibonacci n =
  // p stands for previous and pp stands for previous-previous
  let rec getFibHelper p pp currentN =
    if currentN > n then p else getFibHelper (pp + p) p (currentN + 1)
  match n with
    | 1 -> Some 1
    | 2 -> Some 1
    | x when x > 2 -> Some (getFibHelper 1 1 3)
    | _ -> None
