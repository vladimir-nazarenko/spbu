module IterableTree

open System.Collections.Generic
open System.Collections

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

// raise when no element in current
exception IteratorException

// buld stack from left most branch of the tree and join it to the given stack(st)
let rec buildStack tree st =
  match tree with
    | None -> []
    | Some(Leaf x) -> (Leaf x) :: st
    | Some(Tree(x, l, r)) -> buildStack l ((Tree(x, l, r)) :: st)

// get element from stack and modify the stack
let rec getNext stack =
  match stack with
    | [] -> raise IteratorException
    | x :: xs ->
      match x with
        | Leaf u -> (xs, u)
        | Tree(v, l, r) ->
          if r.IsSome then getNext(buildStack r (x :: xs)) else (xs , v)

// get element or raise an exception
let tryGetElem opt =
  match opt with
    | None -> raise IteratorException
    | Some y -> y

// Enumerator for tree
type TreeEnum<'a>(tr: Tree<'a> option) =
  let mutable stack = []
  do
    // Build stack of tree
    stack <- buildStack tr stack
  // current element
  let mutable cur = None
  interface IEnumerator<'a> with
    member e.Current with get () = tryGetElem cur
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

type ItTree<'a when 'a: comparison>() =
  let mutable tree: Tree<'a> option = None
  member t.find elem = findInTree tree elem
  member t.insert elem = tree <- addIntoTree tree elem
  member t.remove elem = tree <- deleteFromTree tree elem
  interface IEnumerable<'a> with
    member t.GetEnumerator = fun () -> new TreeEnum<'a>(tree)

    
