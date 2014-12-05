module Rhombus

exception InternalError

let makeChars ch len =
  let rec makeCharsTR s c l =
    match l with
      | x when x < 0 -> raise InternalError
      | 0 -> s
      | x -> makeCharsTR (s + c) c (l - 1)
  makeCharsTR "" (string(ch)) len
    

let makeSpaces count = makeChars ' ' count

let makeStars count = makeChars '*' count

let makeStringWithStars starCount strLen =
  if (strLen - starCount) % 2 > 0 then raise InternalError
  let ident = int(float(strLen - starCount) / 2.0)
  makeSpaces(ident) + makeStars(starCount) + makeSpaces(ident)

let printRhomb n =
  if n <= 0 then raise InternalError
  let rec helper m stars len result =
    match m with
      | n when n > 0 ->
        helper (m - 1) (stars + 2) len ((makeStringWithStars stars len) :: result)
      | _ -> result
  let parts = helper n 1 (n * 2 - 1) []
  let rhomb = ((List.rev (parts.Tail))) @ parts
  List.iter (fun x -> printf "%s\n" x) rhomb

    
