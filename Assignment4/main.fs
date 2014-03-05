module Main


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
    
    
