module Workflow

type rounding(ratio: int) =
  member this.Bind(x: float, rest: float -> float) =
    rest(x)
  member this.Return(x: float) = System.Math.Round(x, ratio)

let tryCompute =
  rounding 3 {
    let! a = 2.0 / 12.0
    let! b = 3.5
    return a / b
  }
