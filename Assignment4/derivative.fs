module Derivative

exception InnerException

type Expr =
  | Var of char
  | Constant of int 
  | Prod of Expr * Expr
  | Sum of Expr * Expr
  | Dif of Expr * Expr
  | Quot of Expr * Expr

let simplifySum e1 e2 operator =
  let selectOperator e1 e2 op =
    if op = '+' then Sum(e1, e2) elif op = '-' then Dif(e1, e2) else raise InnerException
  let realOp = if operator = '+' then (+) else (-)
  match e1 with
    | Constant 0 -> e2
    | Constant x ->
      match e2 with
        | Constant y -> Constant (realOp x y)
        | _ -> selectOperator e1 e2 operator
    | _ ->
      match e2 with
        | Constant 0 -> e1
        | _ -> selectOperator e1 e2 operator

let simplifyProd e1 e2 operator =
  let selectOperator e1 e2 op =
    if op = '*' then Prod(e1, e2) elif op = '/' then Quot(e1, e2) else raise InnerException
  let realOp = if operator = '*' then (*) else (/)
  match e1 with
    | Constant 0 -> Constant 0
    | Constant 1 -> e2
    | Constant x ->
      match e2 with
        | Constant y -> Constant (realOp x y)
        | _ -> selectOperator e1 e2 operator
    | _ ->
      match e2 with
        | Constant 0 -> Constant 0
        | Constant 1 -> e1
        | _ -> selectOperator e1 e2 operator

let rec derive = function
  | Var c -> Constant 1
  | Constant num -> Constant 0
  | Prod (e1, e2) -> simplifySum (simplifyProd <| e1 <| (derive e2) <| '*') (simplifyProd <| (derive e1) <| e2 <| '*') '+'
  | Sum (e1, e2) -> simplifySum (derive e1) (derive e2) '+'
  | Dif (e1, e2) -> simplifySum (derive e1) (derive e2)  '-'
  | Quot(e1, e2) -> Quot (simplifySum (simplifyProd (derive e1) e2 '*') (simplifyProd e1 (derive e2) '*') '-', simplifyProd e2 e2 '*')
