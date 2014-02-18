module Lists

let rec reverse = function
  | [] -> []
  | head :: tail -> (reverse tail) @ [head]

let rec powersOfTwo = function
    | n when n < 0 -> []
    | n -> (pown 2 n) :: powersOfTwo (n - 1)

let firstFivePowerOfTwo = reverse (powersOfTwo 5)

let countProduct number =
  let letters = string number
  
