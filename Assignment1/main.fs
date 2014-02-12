module Main

let rec factorial = function
  | x when x < 0 -> None
  | 0 -> Some 1
  | x ->
    match factorial (x - 1) with
      | Some y -> Some (x * y)
      | _ -> None

// Here compiler said, that it couldn't resolve type
let calculateNextFibonacci (numbers: int list) =
  numbers.Head + numbers.Tail.Head

let rec getListFibonacci n =
  match n with
    | n when n < 0 -> None
    | 0 -> Some([])
    | 1 -> Some([1])
    | 2 -> Some([1; 1])
    | _ ->
        let prev = getListFibonacci (n - 1)
        Some((prev.Value |> calculateNextFibonacci) :: prev.Value)

let rec getNth n list =
  match list with
    | [] -> None
    | head :: tail ->
      match n with
        | x when x <= 0 -> None
        | 1  -> Some head
        | _ -> getNth (n - 1) list.Tail

let rec getNthFibonacci n =
  match (getListFibonacci n) with
    | None -> None
    | Some number -> Some number.Head
