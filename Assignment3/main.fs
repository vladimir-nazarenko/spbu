module Main

// Function to find maximum pair of neighbour elements in a list
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
  List.findIndex ((=) maxSum) sums


// Check if all elements in the list are unique
let isUnique xs =
  let rec isUniqueTR ys duplicates =
    match ys with
      | [] -> true
      | z :: zs -> if (List.exists ((=) z) duplicates) then false else isUniqueTR zs (z :: duplicates)
  isUniqueTR xs []

// Count the naumber of even elements in the list
let countEven1 xs = xs |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum

let countEven2 xs = xs |> List.filter (fun x -> x % 2 = 0) |> List.length

let countEven3 = List.fold (fun acc elem -> if elem % 2 = 0 then acc + 1 else acc) 0

// Binary tree representation
type Tree<'a> =
  | Tree of 'a * Tree<'a> * Tree<'a>
  | Leaf of 'a

// Count the height of a tree

let countHeight tr =
  let rec countHeightHelper tr' cnt results =
    match tr' with
    | Leaf _ -> (cnt + 1) :: results
    | Tree(_, l, r) -> (countHeightHelper l (cnt + 1) results) @ (countHeightHelper r (cnt + 1) results)
  List.max (countHeightHelper tr 0 [])

// Arithmetical tree representation

type Expr =
  | Sum of Expr * Expr
  | Mult of Expr * Expr
  | Neg of Expr
  | Num of int

// Counts arithmetical expression by its tree

let rec countExpr expr =
  match expr with
    | Mult (x, y) -> countExpr(x) * countExpr(y)
    | Sum (x, y) -> countExpr(x) + countExpr(y)
    | Num x -> x
    | Neg x -> -countExpr(x)

let intSqrt n = n |> float |> sqrt |> int

let isPrime n =
  let rec isPrimeTR n c sqrtN =
    match n % c with
      | 0 -> false
      | _ -> if c < sqrtN then isPrimeTR n (c + 1) sqrtN else true
  isPrimeTR n 2 (intSqrt n)

let primeNumbers =
  Seq.initInfinite(fun i -> i + 1) |> Seq.filter (isPrime)
