format long
puts "Task 7. Formulas of the highest degree of precision\n";
f(NaN);
a = -1; #input("Enter the left margin: ");
b = 1; #input("Enter the right margin: ");
N = 3; #input("Enter the number of notes for formula: ");
assert(N > 0);
function res = polyPower(pol, N)
  cur = pol;
  for i = 1:floor(log2(N))
    cur = conv(cur, cur);
  endfor
  for i = 1:(N - 2 ^ floor(log2(N)))
      cur = conv(cur, pol);
  endfor
  res = cur;
endfunction
# first case
function l = legendre(N)
	 
  pol = polyPower([1, 0, -1], N);
  for i = 1:N
      pol = polyder(pol);
  end
  l = conv(pol, 1 / (2 ^ N * factorial(N)));
endfunction
legendre(10)
format short
