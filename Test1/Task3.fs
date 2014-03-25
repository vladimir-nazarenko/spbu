module PQ

type PQueue<'a when 'a: equality>() =
  let mutable elems = []
  member p.enqueue(x: 'a, pr: int) = elems <- (x, pr) :: elems
  member p.dequeue =
    if elems.IsEmpty
    then None else
      let elem = (List.rev (List.sortBy(fun (a, b) -> b) elems)).Head
      List.filter ((<>) elem) elems
      Some(fst elem)
  
