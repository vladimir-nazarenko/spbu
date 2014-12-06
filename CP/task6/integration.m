format long
puts "Task 6. Formulas of the highest degree of precision\n";
f(NaN);
a = 0; #input("Enter the left margin: ");
b = 1; #input("Enter the right margin: ");
N = 3; #input("Enter the number of notes for formula: ");
assert(N > 0);
assert(a < b);
# Comoute coefficients for ortagonal polynomial
A = zeros(N);
for i = 1:N
    for j = 1:N
	A(i, j) = getMoment(N + i - j - 1, a, b);
    end
end
B = -getMoment(N:(N * 2 - 1), a, b)';
A
B
coeff = A \ B
X = roots([1; coeff]')
rts = zeros(N)
for i = 1:N
  for j = 1:N
    rts(i, j) = X(j) ^ (i - 1);
  endfor
endfor
rts
Ak = rts \ getMoment(0:N-1, a, b)'
calc = Ak' * f(X)
format short
