module PQ

type PQueue<'a when 'a: equality>() =
  let mutable elems = []
  let rec removeFirst lst elem built =
    match lst with
      | [] -> []
      | x :: xs when x = elem -> (List.rev built) @ xs
      | x :: xs -> removeFirst xs elem (x :: built)
  member p.enqueue(x: 'a, pr: int) = elems <- (x, pr) :: elems
  member p.dequeue =
    if elems.IsEmpty
    then None
    else
      let elem = (List.rev (List.sortBy(fun (a, b) -> b) elems)).Head
      elems <- removeFirst elems elem []
      Some(fst elem)
    
    
