module Lists

let rec reverse = function
  | [] -> []
  | head :: tail -> (reverse tail) @ [head]

let rec powersOfTwo = function
    | n when n < 0 -> []
    | n -> (pown 2 n) :: powersOfTwo (n - 1)

let firstFivePowerOfTwo = reverse (powersOfTwo 5)

let splitString (str: string) =
  List.init str.Length (fun index -> string str.[index])

// type needed to avoid cases, when user calls function with non-integer, but conversible to string type, because this can cause an exception
let countProduct (number: int) =
  let strNum = string number
  strNum |> splitString |> (List.fold (fun init x -> init * (System.Int32.Parse x)) 1)
  
let getFirstPosition elem lst =
  let rec helper lst_helper n =
    match lst_helper with
      | [] -> None
      | x :: tail when (x = elem) -> Some n
      | x :: xs -> helper xs (n + 1)
  helper lst 0

let isPalindrom str = str = (str |> splitString |> reverse |> (List.reduce (fun a b -> a + b)))
