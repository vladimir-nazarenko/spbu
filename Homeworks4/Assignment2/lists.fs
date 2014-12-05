module Lists

let reverse l =
  let rec reverseTR lst constructed =
    match lst with
      | [] -> constructed
      | x :: xs -> reverseTR xs (x :: constructed)
  reverseTR l []

let powersOfTwo n =
  let rec helper m cur =
    match m with
      | m when m < 0 -> cur
      | m -> helper m ((pown 2 m) :: cur)
  helper n []
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
