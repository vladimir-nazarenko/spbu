# define constants for task
x_0 = 0;
y_0 = 0.25;
# y_0 = 1;
# N from task descriptionn
N_inp = 10;
# Length of the vectors from zero
N = N_inp + 1;
h = 0.1;
taylorL = 6;
function u = f(x, y)
  u = -y .+ 2 * y .^ 2 + 1;
#  u = -y .* (2 .- cos(x));
endfunction

# points for approximation
x_k = linspace(x_0, x_0 + N * h, N + 1);
# calculate derivatives at the x_0
d(1) = 1/4;#y_0;
d(2) = 7/8;#f(x_0, y_0);
d(3) = 0;#-d(2) + 4 * d(1) * d(2);
d(4) = 49/16;#-d(3) + 4 * ((d(2)) ^ 2 + d(1) * d(3));
d(5) = 0;#-d(4) + 4 * (3 * d(3) * d(2) + d(1) * d(4))
d(6) = 343/8;
# calculate taylor series coefficients
taylorPoly = 0;
diff = 1;
for i = 1:(taylorL)
  p1 = taylorPoly;
  p2 = conv(d(i), diff) ./ factorial(i - 1);
  taylorPoly = [zeros(1, size(p1,2)-size(p2,2)) p2] + [zeros(1, size(p2,2)-size(p1,2)) p1];
  diff = conv([1, -x_0], diff);
endfor
# Euler's method
euler(1) = y_0;
for i = 2:N
    euler(i) = euler(i - 1) + h * f(x_k(i), euler(i - 1));
endfor
# Improved euler
euler_imp(1) = y_0;
for i = 2:N
    half = euler_imp(i - 1) + h * f(x_k(i), euler_imp(i - 1)) / 2;
    euler_imp(i) = euler_imp(i - 1) + h * f(x_k(i) + h / 2, half);
endfor
# Euler-Cauchy
eulerCauchy(1) = y_0;
for i = 2:N
  eulerCauchy(i) = eulerCauchy(i - 1) + h * (f(x_k(i), eulerCauchy(i - 1)) + f(x_k(i + 1), eulerCauchy(i - 1) + h * f(x_0, y_0))) / 2;
endfor
# Runge-Kutt
rungeKutt(1) = y_0;
for i = 2:N
  k1 = h * f(x_k(i - 1), rungeKutt(i - 1));
  k2 = h * f(x_k(i - 1) + h / 2, rungeKutt(i - 1) + k1 / 2);
  k3 = h * f(x_k(i - 1) + h / 2, rungeKutt(i - 1) + k2 / 2);
  k4 = h * f(x_k(i - 1) + h, rungeKutt(i - 1) + k3);
  rungeKutt(i) = rungeKutt(i - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
endfor
# Adams
adams = [];
for i = -2:2
    pt = x_0 + i * h;
    adams(i + 3) = solution(pt);#polyval(taylorPoly, pt);
endfor
consts = [1, 1 / 2, 5 / 12, 3 / 8, 251 / 720];
x_k = [x_0 - 2 * h, x_0 - h, x_k];
for i = 3:N
  indexes = (i - 5 + 3):(i - 1 + 3);
  finDif = finiteDiff(h .* f(x_k(indexes), adams(indexes)), 4);
#  [h .* f(x_k(indexes), adams(indexes))', [finDif; zeros(1, 4)]]
  impDiff = [h * f(x_k(i - 1 + 3), adams(i - 1 + 3)); diag(rotdim(finDif, 3))];
  adams(i + 3) = adams(i - 1 + 3) + consts * impDiff;
endfor
x_k = x_k(3:end);
adams = adams(3:end);

# printing
printf("%-19s   %-19s   %-19s   %-19s\n", "Argument", "Value", "Taylor approx.", "Taylor error");
for i = -2:N_inp
    pt = x_0 + i * h;
    sol = solution(pt);
    tay = polyval(taylorPoly, pt);
    dif = abs(sol - tay);
    printf("%+3.16f   %+3.16f   %+3.16f   %+3.16f\n", pt, sol, tay, dif);
endfor
#printf("\n%-19s   %-19s   %-19s   %-19s   %-19s   %-19s   %-19s\n", "Argument", "Euler", "Error", "Improved euler", "Error", "Euler-Cauchy", "Error");
for i = 1:N
    pt = x_k(i);
    sol = solution(pt);
    e1 = euler(i);
    e2 = euler_imp(i);
    e3 = eulerCauchy(i);
    printf("%+3.16f   %+3.16f   %+3.16f   %+3.16f   %+3.16f   %+3.16f   %+3.16f\n", pt, e1, abs(sol - e1), e2, abs(sol - e2), e3, abs(sol - e3));
endfor
printf("\n%-19s   %-19s   %-19s   %-19s   %-19s\n", "Argument", "Runge-Kutt", "Error", "Adams", "Error");
for i = 1:N
    pt = x_k(i);
    sol = solution(pt);
    rk = rungeKutt(i);
    ad = adams(i);
    printf("%+3.16f   %+3.16f   %+3.16f   %+3.16f   %+3.16f\n", pt, rk, abs(sol - rk), ad, abs(sol - ad));
endfor
