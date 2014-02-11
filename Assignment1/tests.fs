module Tests

if Main.factorial -3 = 1
then printfn "%s" "f(-3) = 1 passed"
else printfn "%s" "(f(-3) failed"

if Main.factorial 0 = 1
then printfn "%s" "f(0) = 1 passed"
else printfn "%s" "f(0) failed"

if Main.factorial 5 = 120
then printfn "%s" "f(5) = 120 passed"
else printfn "%s" "f(5) failed"
