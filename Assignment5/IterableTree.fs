module IterableTree

open System.Collections.Generic
open System.Collections

type Tree<'a> =
  | Tree of 'a * Tree<'a> * Tree<'a>
  | Leaf of 'a option

// raise when no element in current
exception IteratorException

exception DeleteException of string

// Enumerator for tree
type TreeEnum<'a>(tr: Tree<'a>) =
  // buld stack from left most branch of the tree and join it to the given stack(st)
  // stack consists of left most branch
  let rec buildStack tree st =
    match tree with
      | Leaf None -> []
      | Leaf (Some x) as leaf -> leaf :: st
      | Tree(x, l, r) as branch -> buildStack l (branch :: st)

  // get element from stack and modify the stack
  // makes possible traversing stack in order
  let rec getNext stack =
    match stack with
      // empty stack
      | [] -> raise IteratorException
      | x :: xs ->
        match x with
          | Leaf None -> raise IteratorException
          | Leaf (Some u) -> (xs, u)
          | Tree(v, l, r) ->
            match r with
              | Leaf None -> (xs, v)
              | _ -> getNext(buildStack r (x :: xs))

  // get element or raise an exception
  let getElem tree =
    match tree with
      | Tree(el, _, _) -> el
      | Leaf (Some el) -> el
      | Leaf None -> raise IteratorException
      
  let mutable stack = []
  do
    // Build stack of tree
    stack <- buildStack tr stack
  // current element
  let mutable isAccessible = false
  interface IEnumerator with
    member e.Current with get () = if isAccessible && not stack.IsEmpty then box <| getElem(stack.Head) else raise IteratorException
    member e.MoveNext() =
      try
        let (st, elem) = getNext stack
        stack <- st
        isAccessible <- true
        true
      with
        | IteratorException -> isAccessible <- false; false
    member e.Reset() = stack <- buildStack tr []
  interface IEnumerator<'a> with
    member e.Current with get () = if isAccessible && not stack.IsEmpty then getElem(stack.Head) else raise IteratorException
    member e.Dispose() = () // Have no idea, what should I dispose here


type ItTree<'a when 'a: comparison>() =
  // I believe, there is no need to make this function tail-recursive, while it can't cause stack overflow
  let rec addIntoTree t x =
    match t with
      | Leaf None -> Leaf <| Some x
      | Leaf (Some y) ->
        if x > y then
          Tree(y, Leaf None, Leaf <| Some x) else
            Tree(y, Leaf <| Some x, Leaf None)
      | Tree(z, l, r) ->
        if x > z then
          Tree(z, l, addIntoTree r x) else
            Tree(z, addIntoTree l x, r)

  let rec findInTree t x =
    match t with
      | Leaf None -> false
      | Leaf (Some y) -> x = y
      | Tree(z, l, r) -> if x > z then findInTree r x else findInTree l x

  // returns just Leaf or exception is raised
  let rec rightmost t =
    match t with
      | Leaf None as empty -> raise <| DeleteException "rightmost of nothing"
      | Leaf (Some x) as leaf -> leaf
      | Tree(y, l, r) as tree ->
        match r with
          | Leaf None as empty -> tree
          | _ -> rightmost r

  let rec deleteFromTree t x =
    match t with
      | Leaf None -> Leaf None
      | Leaf(Some y) -> if x = y then Leaf None else Leaf (Some y)
      | Tree(z, l, r) ->
        if x > z then Tree(z, l, deleteFromTree r x)
        elif x < z then Tree(z, deleteFromTree l x, r)
        else
          match r with
            | Leaf None ->
              // then l in not none - invariant
              // has just left
              l
            | _ ->
              match l with
                | Leaf None ->
                  // has just right
                  r
                | _ ->
                  // has both
                  match rightmost l with
                    // asserted l is not null
                    | Leaf None | Tree(_, _, _) -> raise <|  DeleteException "rightmost can't be nothing or Tree"
                    | Leaf(Some u) -> Tree(u, deleteFromTree l u, r)

  let mutable tree: Tree<'a> = Leaf None
  member t.find elem = findInTree tree elem
  member t.insert elem = tree <- addIntoTree tree elem
  member t.remove elem = tree <- deleteFromTree tree elem
  interface IEnumerable with
    member t.GetEnumerator() = (new TreeEnum<'a>(tree) :> IEnumerator)
  interface IEnumerable<'a> with
    member t.GetEnumerator() = (new TreeEnum<'a>(tree) :> IEnumerator<'a>)
