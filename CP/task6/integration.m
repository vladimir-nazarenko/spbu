format long
puts "Task 6. Formulas of the highest degree of precision\n";
f(NaN);
a = input("Enter the left margin: ");
b = input("Enter the right margin: ");
N = input("Enter the number of notes for formula: ");
assert(N > 0);
# Compute coefficients for ortagonal polynomial
A = zeros(N);
for i = 1:N
    for j = 1:N
	A(i, j) = getMoment(N + i - j - 1, a, b);
    end
end
B = -getMoment(N:(N * 2 - 1), a, b)';
puts "Moments of function: \n";
disp(getMoment(0:(N * 2 - 1), a, b)');
coeff = A \ B;
puts "Ortogonal polynomial: \n";
disp(polyout([1;coeff]', 'x'));
X = roots([1; coeff]');
rts = zeros(N);
for i = 1:N
  for j = 1:N
    rts(i, j) = X(j) ^ (i - 1);
  endfor
endfor
puts "Knots of formula: \n";
disp(X);
Ak = rts \ getMoment(0:N-1, a, b)';
puts "Coefficients of formula: \n";
disp(Ak);
function y = fw(x)
  y = f(x) .* abs(1 ./ sqrt(x));
endfunction
printf("%-40s %3.16f\n%-40s %3.16f\n%-40s %3.16f\n", 
       "Method gave the result", Ak' * f(X),
       "while the value calculated by octave is", quad(@fw, a, b), 
       "so the error is", abs(Ak' * f(X) - quad(@fw, a, b)));
format short
