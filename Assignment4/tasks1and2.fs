module Main

// Check if xs contains correct parenthesis sequence
let checkCorrectness xs =
  let isOpenedBrace = function
    | '[' | '(' | '{' -> true
    | _ -> false
  let isClosedBrace = function
    | ']' | ')' | '}' -> true
    | _ -> false
  let matches a b =
    match a with
      | '(' -> b = ')'
      | '[' -> b = ']'
      | '{' -> b = '}'
      | _ -> false      
  let helper acc x =
    if isOpenedBrace x then x :: acc
    elif isClosedBrace x then
      match acc with
        | [] -> [x]
        | z :: zs -> if matches z x then zs else x :: acc
    else acc
  (List.fold (helper) [] xs).IsEmpty

// Converting function in point-free
let func x l = List.map (fun y -> y * x) l

let multiplyBy x y = x * y

let func1 x : 'a list -> 'b list = List.map (multiplyBy x)

let func2 : int -> 'a list -> 'b list = List.map << multiplyBy

let func3 : int -> 'a list -> 'b list = List.map << (*)

// Thought, that it's impossible to skip type annotation in f#
let func4 = List.map << (*)


