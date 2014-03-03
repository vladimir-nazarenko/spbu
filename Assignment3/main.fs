module Main

let maxPair xs =
  let rec joinLists ys zs acc =
    match ys with
      | [] -> acc
      | y :: ys' ->
        match zs with
          | [] -> acc
          | z :: zs' -> joinLists ys' zs' ((y, z) :: acc)
        // TODO: find a mistake in comment  
        //  (xs, xs.Tail, []) |||> joinLists |> (List.map (fun (a, b) -> a + b))
  let sums = List.map (fun (a, b) -> a + b) (List.rev (joinLists xs xs.Tail []))
  let maxSum = List.max sums
  List.findIndex (fun x -> x = maxSum) sums
