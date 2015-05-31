warning ("off", "Octave:broadcast");
puts "Task 5. Solving integral equation"
# Part 2.
puts "Kernel is sin(xy)\n";
puts "f=1+x^2\n";
puts "Approximate kernel having the rank 3 is xy - 1/6 * (xy)^3 + 1/120 * (xy)^5\n";
a = 0
b = 1
N = 10
N_interp = N;
# compute scalar product of betas and f
# defIntegral(0, 1, 1, [0 0.47943 0.84147; 0 0.47943 0.84147]')
# i-th column of y is values for given vector of beta_i

### trace the dimentions
[x y] = simpleKernel3(linspace(a, b, 2 * N_interp - 1)', linspace(a, b, 2 * N_interp - 1)');
#sum(x .* y, 2)
#[x y] = simpleKernel4(linspace(a, b, 2 * N_interp - 1)', linspace(a, b, 2 * N_interp - 1)');
#sum(x .* y, 2)
#for i = linspace(a, b, 2 * N_interp - 1)'
#  for j = linspace(a, b, 2 * N_interp - 1)'
#      K(i, j)    
#  endfor
#endfor
f_vals = f(linspace(a, b, 2 * N_interp - 1)');
# scalar products
F = zeros(1, 3);
#y
for c = 1:3
  F(1, c) = defIntegral(a, b, (b - a) / (N_interp - 1), y(:, c) .* f_vals);
endfor
#new_F = defIntegral(a, b, (b - a) / N_interp, y .* f_vals); has the same value as F
G = eye(3);
for i = 1:3
  for j = 1:3
    G(i, j) += defIntegral(a, b, (b - a) / (N_interp - 1), x(:, j) .* y(:, i));
  endfor
endfor
ud = makeUpperDiagonal([G F']);
A = solveUpperDiagonal(ud(:, 1:end-1), ud(:, end));
#A = G \ F has the same precision
x = simpleKernel3(linspace(a, b, N)', 0);
res3 = f(linspace(a, b, N))' -  sum(x .* A, 2);
#puts "Solution for kernel of the rank 3 is:\n";
#disp(res3);


### part 4
[x y] = simpleKernel4(linspace(a, b, 2 * N_interp - 1)', linspace(a, b, 2 * N_interp - 1)');
f_vals = f(linspace(a, b, 2 * N_interp - 1)');
#y
#y .* f_vals
F = zeros(1, 4);
for i = 1:4
  F(1, i) = defIntegral(a, b, (b - a) / (N_interp - 1), y(:, i) .* f_vals);
endfor
G = eye(4);
for i = 1:4
  for j = 1:4
    G(i, j) += defIntegral(a, b, (b - a) / (N_interp - 1), x(:, j) .* y(:, i));
  endfor
endfor
#G???
ud = makeUpperDiagonal([G F']);
A = solveUpperDiagonal(ud(:, 1:end-1), ud(:, end));
x = simpleKernel4(linspace(a, b, N)', 0);
A = G \ F';
res4 = f(linspace(a, b, N))' -  sum(x .* A', 2);
#puts "Solution for kernel of the rank 4 is:\n";
#disp(res4);
#puts "Maximum of the difference between solutions: ";
#disp(max(abs(res3 - res4)));


### part 7
N_n = 5; # N for ntwn 
pol = legendre(N_n);
Xk = roots(pol);
#puts("Knots of formula: \n");
#disp(Xk);
Ak = 2 ./ ((1 - Xk .^ 2) .* (polyval(polyder(pol), Xk) .^ 2));
#puts "Integral sum coefficients: \n";
#disp(Ak);
Bk = Ak .* ((b - a) / 2);
Tk = ((b - a) / 2) * Xk  + ((b + a) / 2);
#sum(Bk .* exp(Tk)) #integration is correct
H = eye(N_n);
F = f(Tk);
for i = 1:N_n
  for j = 1:N_n
    H(i, j) += K(Tk(i), Tk(j)) * Bk(j, 1);
  endfor
endfor
u_j = H \ F;
x = linspace(a, b, N);
r = zeros(size(x, 2), size(Tk, 1));
for i = 1:size(Tk, 1)
    for k = 1:N
      r(k, i) = K(x(k), Tk(i)) * Bk(i, 1) * u_j(i, 1);
    endfor
endfor
resmmk4 = f(x') - sum(r, 2);
#puts "Maximum of the difference between solutions: ";
#disp(max(abs(resmmk4 - res4)));
#res = f(x) - sum(Bk .* K(Tk, 

N_n = 6; # N for ntwn 
pol = legendre(N_n);
Xk = roots(pol);
#puts("Knots of formula: \n");
#disp(Xk);
Ak = 2 ./ ((1 - Xk .^ 2) .* (polyval(polyder(pol), Xk) .^ 2));
#puts "Integral sum coefficients: \n";
#disp(Ak);
Bk = Ak .* ((b - a) / 2);
Tk = ((b - a) / 2) * Xk  + ((b + a) / 2);
#sum(Bk .* exp(Tk)) #integration is correct
H = eye(N_n);
F = f(Tk);
for i = 1:N_n
  for j = 1:N_n
    H(i, j) += K(Tk(i), Tk(j)) * Bk(j, 1);
  endfor
endfor
u_j = H \ F;
x = linspace(a, b, N);
r = zeros(size(x, 2), size(Tk, 1));
for i = 1:size(Tk, 1)
    for k = 1:N
      r(k, i) = K(x(k), Tk(i)) * Bk(i, 1) * u_j(i, 1);
    endfor
endfor
resmmk6 = f(x') - sum(r, 2);
#puts "Maximum of the difference between solutions: ";
#disp(max(abs(resmmk4 - resmmk6)));
ress = [res3'; res4'; resmmk4'; resmmk6'];
disp([res3'; res4'; resmmk4'; resmmk6']);
errs = zeros(4);
puts "Matrix of differences:\n";
for i = 1:4
  for j = 1:4
      errs(i, j) = max(abs(ress(i, :) - ress(j, :)));
  endfor
endfor
disp(errs);
