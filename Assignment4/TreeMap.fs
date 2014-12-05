module TreeMap

// Binary tree representation
type Tree<'a> =
  | Tree of 'a * Tree<'a> * Tree<'a>
  | Leaf of 'a

let rec map f tree =
  match tree with
    | Leaf x -> Leaf (f x)
    | Tree(x, l, r) -> Tree((f x), (map f l), (map f r))
