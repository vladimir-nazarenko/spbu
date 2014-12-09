format long
puts "Task 7. Formulas of the highest degree of precision\n";
f(NaN);
N = input("Enter the number of knots for formulas: ");
puts "Calculating integral with w(x) = 1\n";
a = input("Enter the left margin: ");
b = input("Enter the right margin: ");
assert(N > 0);
# first case
function l = legendre(N)
  assert(N >= 0)
  p0 = [1];
  p1 = [1, 0];
  if(N == 0)
     l = p0;
  elseif(N == 1)
    l = p1;
  else
    cur = p1;
    prev = p0;
    for i = 2:N
      p1 = conv([(2 * i - 1) / i, 0], cur);
      p2 = -conv((i - 1) / i, prev);
      calc = [zeros(1, size(p1,2)-size(p2,2)) p2] + [zeros(1, size(p2,2)-size(p1,2)) p1];
      prev = cur;
      cur = calc;
    endfor
    l = cur;
  endif
endfunction
pol = legendre(N);
Xk = roots(pol);
puts("Knots of formula: \n");
disp(Xk);
Ak = 2 ./ ((1 - Xk .^ 2) .* (polyval(polyder(pol), Xk) .^ 2));
puts "Integral sum coefficients: \n";
disp(Ak);
Bk = Ak .* ((b - a) / 2);
Tk = ((b - a) / 2) * Xk + ((b + a) / 2);
printf("%-30s %3.16f\n%-30s %3.16f\n%-30s %3.16f\n", 
       "Method gave the result", Bk' * f(Tk),
       "while the precise value is", fint(b) - fint(a), 
       "so the error is", abs(Bk' * f(Tk) - (fint(b) - fint(a))));


# second case
puts "Calculating integral for given function with w(x) = 1 / sqrt(1 - x^2) from -1 to 1\n";
Xk = cos((2 * pi * [1:N] - pi) / (2 * N))';
puts "Knots of formula: \n";
disp(Xk);
Ak = repmat(pi / N, N, 1);
puts "Integral sum coefficients: \n";
disp(Ak);
function res = fch1(x)
  res = f(x) ./ sqrt(1 - x .^ 2);
endfunction
printf("%-40s %3.16f\n%-40s %3.16f\n%-40s %3.16f\n", 
       "Method gave the result", Ak' * f(Xk),
       "while the value calculated by octave is", quad(@fch1, -1, 1), 
       "so the error is", abs(Ak' * f(Xk) - quad(@fch1, -1, 1)));

#third case
puts "Calculating integral for given function with w(x) = sqrt(1 - x^2) from -1 to 1\n";
Xk = cos([1:N] * pi / (N + 1))';
puts "Knots of formula: \n";
disp(Xk);
Ak = (pi / (N + 1)) * (sin([1:N] * pi / (N + 1))) .^ 2';
puts "Integral sum coefficients: \n";
disp(Ak);
function res = fch2(x)
  res = f(x) .* sqrt(1 - x .^ 2);
endfunction
printf("%-40s %3.16f\n%-40s %3.16f\n%-40s %3.16f\n", 
       "Method gave the result", Ak' * f(Xk),
       "while the value calculated by octave is", quad(@fch2, -1, 1), 
       "so the error is", abs(Ak' * f(Xk) - quad(@fch2, -1, 1)));

# forth case
puts "Calculating integral for given function with w(x) = e ^ -x^2 from -inf to inf\n";
function p = hermit(N)
  assert(N >= 0)
  p = 1;
  for i = 1:N
    p1 = conv([-2, 0], p);
    p2 = polyder(p);
    p = [zeros(1, size(p1,2)-size(p2,2)) p2] + [zeros(1, size(p2,2)-size(p1,2)) p1];
  endfor
  p = conv(p, (-1) ^ N);
endfunction
pol = hermit(N);
Xk = roots(hermit(N));
puts "Knots of formula: \n";
disp(Xk);
Ak = zeros(N, 1);
for i = 1:N
  Ak(i, 1) = (2 ^ (N + 1) * factorial(N) * sqrt(pi) / polyval(polyder(pol), Xk(i, 1)) .^ 2);
endfor
puts "Integral sum coefficients: \n";
disp(Ak);
function res = fche(x)
  res = f(x) .* exp(- x .^ 2);
endfunction
printf("%-40s %3.16f\n%-40s %3.16f\n%-40s %3.16f\n", 
       "Method gave the result", Ak' * f(Xk),
       "while the value calculated by octave is", quad(@fche, -Inf, Inf), 
       "so the error is", abs(Ak' * f(Xk) - quad(@fche, -Inf, Inf)));

# fifth case
puts "Calculating integral for given function with w(x) = x ^ a * e ^ -x from 0 to inf\n";
clear global alpha;
global alpha = input("Enter alpha > -1: ");
assert(alpha > -1);
function l = laguerre(N)
  global alpha;
  assert(N >= 0);
  p0 = [1];
  p1 = [-1, 1 + alpha];
  if(N == 0)
     l = p0;
  elseif(N == 1)
    l = p1;
  else
    cur = p1;
    prev = p0;
    for i = 2:N
      p1 = conv([-1, 2 * (i - 1) + 1 + alpha], cur) ./ i;
      p2 = conv(-alpha - (i - 1), prev) ./ i;
      calc = [zeros(1, size(p1,2)-size(p2,2)) p2] + [zeros(1, size(p2,2)-size(p1,2)) p1];
      prev = cur;
      cur = calc;
    endfor
    l = cur;
  endif
endfunction
pol = laguerre(N);
Xk = roots(laguerre(N));
puts "Knots of formula: \n";
disp(Xk);
Ak = zeros(N, 1);
for i = 1:N
  Ak(i, 1) = (gamma(N + alpha + 1)) / (gamma(N + 1)  * Xk(i, 1) * (polyval(polyder(pol), Xk(i, 1))) .^ 2);
endfor
puts "Integral sum coefficients: \n";
disp(Ak);
function res = fla(x)
  global alpha;
  res = f(x) .* exp(- x) .* (x .^ alpha);
endfunction
printf("%-40s %3.16f\n%-40s %3.16f\n%-40s %3.16f\n", 
       "Method gave the result", Ak' * f(Xk),
       "while the value calculated by octave is", quad(@fla, 0, Inf), 
       "so the error is", abs(Ak' * f(Xk) - quad(@fla, 0, Inf)));



# evaluate p(x)
function res = polynomial(p, x)
  res = polyval(p, x);
endfunction
x = linspace(-1, 1, 100);
figure;
leg = legendre(N);
ch1 = chebyshevpoly(1, N);
ch2 = chebyshevpoly(2, N);
plot(x, polynomial(leg, x), "color", "c", "linewidth", 2);
hold on;
plot(x, polynomial(ch1, x), "color", "b", "linewidth", 2);
hold on;
plot(x, polynomial(ch2, x), "color", "g", "linewidth", 2);
[hleg, hleg_obj, hplot, labels] = legend("Legendre's polynomial", "Chebyshev's polynomial of the first type", "Chebyshev's polynomial of the second type");
set(hleg_obj,'linewidth', 3);
title("Ortogonal polynomials");
set([gca; findall(gca, 'Type','line')], 'linewidth', 2);
grid on;

hold off;
figure(2)
che = hermit(N);
x = linspace(-2.5, 2.5, 1000);
plot(x, polynomial(che, x), "color", "r", "linewidth", 2);
hold on;
[hleg, hleg_obj, hplot, labels] = legend("Chebyshov-Ermit's polynomialf");
set(hleg_obj,'linewidth', 3);
title("Ortogonal polynomials");
set([gca; findall(gca, 'Type','line')], 'linewidth', 2);
grid on;

hold off;
figure(3)
leg = laguerre(N);
x = linspace(0, 10, 1000);
plot(x, polynomial(leg, x), "color", "b", "linewidth", 2);
hold on;
[hleg, hleg_obj, hplot, labels] = legend("Leguerre's polynomial");
set(hleg_obj,'linewidth', 3);
title("Ortogonal polynomials");
set([gca; findall(gca, 'Type','line')], 'linewidth', 2);
grid on;

hold off;

format short
