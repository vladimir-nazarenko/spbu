module Tests

if (Main.factorial -3).IsNone
then printfn "%s" "factorial(-3) is None passed"
else printfn "%s" "(factorial(-3) failed"

if Main.factorial 0 = Some 1
then printfn "%s" "factorial(0) = 1 passed"
else printfn "%s" "factorial(0) failed"

if Main.factorial 5 = Some 120
then printfn "%s" "factorial(5) = 120 passed"
else printfn "%s" "factorial(5) failed"

if Main.getNthFibonacci 6 = Some 8
then printfn "%s" "getNthFibonacci(5) = 8 passed"
else printfn "%s" "getNthFibonacci(5) failed"

if Main.getNthFibonacci 1 = Some 1
then printfn "%s" "getNthFibonacci(1) = 1 passed"
else printfn "%s" "getNthFibonacci(1) failed"

if (Main.getNthFibonacci -1).IsNone
then printfn "%s" "getNthFibonacci(-1) is None passed"
else printfn "%s" "getNthFibonacci(-1) failed"
