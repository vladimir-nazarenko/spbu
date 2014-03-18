module IterableTree

open System.Collections.Generic

// Binary tree representation
type Tree<'a> =
  | Tree of 'a * Tree<'a> option* Tree<'a> option
  | Leaf of 'a

// I believe, there is no need to make this function tail-recursive, while it can't cause stack overflow
let rec addIntoTree t x =
  match t with
    | None -> Some(Leaf x)
    | Some(Leaf y) -> if x > y then Some(Tree(y, None, Some(Leaf x))) else Some(Tree(y, Some(Leaf x), None))
    | Some(Tree(z, l, r)) -> if x > z then Some(Tree(z, l, addIntoTree r x)) else Some(Tree(z, addIntoTree l x, r))

let rec findInTree t x =
  match t with
    | None -> false
    | Some(Leaf y) -> x = y
    | Some(Tree(z, l, r)) -> if x > z then findInTree r x else findInTree l x

let rec deleteFromTree t x =
  let rec rightmost t =
    match t with
      | None -> None
      | Some(Leaf x) -> Some(Leaf x)
      | Some(Tree(y, l, r)) -> if r.IsSome then rightmost r else Some(Tree(y, l, r))
  match t with
    | None -> None
    | Some(Leaf y) -> if x = y then None else Some (Leaf y)
    | Some(Tree(z, l, r)) ->
      if x > z then deleteFromTree r x
      elif x < z then deleteFromTree l x
      else
        if l.IsSome && r.IsSome then
          match rightmost l with
            | None -> None
            | Some(Leaf u) -> Some(Tree(u, l, r))
            | Some(Tree(z', l', r')) -> Some(Tree(z', deleteFromTree l z', r))
        elif l.IsSome then l
        elif r.IsSome then r
        else None

exception IteratorException

type ItTree<'a when 'a: comparison>() =
  let mutable tree: Tree<'a> option = None
  member t.find elem = findInTree tree elem
  member t.insert elem = tree <- addIntoTree tree elem
  member t.remove elem = tree <- deleteFromTree tree elem
  type TreeEnum<'a>(tr: Tree<'a> option) =
    let mutable stack = []
    let rec buildStack tree st =
      match tree with
        | None -> []
        | Some(Leaf x) -> (Leaf x) :: st
        | Some(Tree(x, l, r)) -> buildStack l ((Tree(x, l, r)) :: st)
    let rec getNext stack =
      match stack with
        | [] -> raise IteratorException
        | x :: xs ->
          match x with
            | Leaf u -> (xs, u)
            | Tree(v, l, r) ->
              if r.IsSome then getNext(buildStack r (x :: xs)) else (xs , v)
    do
      stack <- buildStack tr stack
    let mutable cur = None
    let tryGetElem =
      match cur with
        | None -> raise IteratorException
        | Some y -> y
    interface IEnumerator<'a> with
      member e.Current with get() = tryGetElem
      member e.MoveNext() =
        try
          let (st, elem) = getNext stack
          stack <- st
          cur <- Some elem
          true
        with
          | IteratorException -> cur <- None; false
      member e.Dispose() = () // Have no idea, what should I dispose here
      member e.Reset() = stack <- buildStack tr []


    
