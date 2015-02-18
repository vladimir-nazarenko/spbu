puts "Task 1. Solving a first order ODE.\n";
# define the equation
function y = p(x)
  y = ones(size(x));
endfunction
function y = q(x)
  y = 1 + log(x + 2);
endfunction
function y = r(x)
  y = -1 - x;
endfunction
function y = f(x)
  y = exp(x ./ 2);
endfunction
# Boundary values
alpha = [0.6, -1];
beta = [-0.3, -1];
A = 0;
B = 0;
left = 0;
right = 1;
assert(left < right);
N = 10;
h = (right - left) / N;
# computing the 3-diagonal matrix
a = zeros(1, N);
b = zeros(1, N);
c = zeros(1, N);
d = zeros(1, N);
pts = linspace(left, right, N);
pj = p(pts)(2:N-1);
qj = q(pts)(2:N-1);
rj = r(pts)(2:N-1);
fj = f(pts)(2:N-1);
a(2:N-1) = pj - qj * h / 2;
b(2:N-1) = -2 * pj + rj * h ^ 2;
c(2:N-1) = pj + qj * h / 2;
d(2:N-1) = fj * h ^ 2;
# computing edge values with presicion O(h)
a(N) = -beta(2);
b(1) = alpha(1) * h - alpha(2);
b(N) = beta(1) * h + beta(2);
c(1) = alpha(2);
d(1) = A * h;
d(N) = B * h;
# get the result
y1 = solve3(c, b, a, d);
# computing edge values with presicion O(h^2)
a(N) = -beta(2) * (4 + b(N-1) / a(N-1));
b(1) = 2 * h * alpha(1) + alpha(2) * (a(2) / c(2) - 3);
b(N) = 2 * h * beta(1) + 3 * beta(2) - beta(2) * c(N-1) / a(N-1);
c(1) = alpha(2) * (b(2) / c(2) + 4);
d(1) = 2 * h * A + alpha(2) * d(2) / c(2);
d(N) = 2 * h * B - beta(2) * d(N-1) / a(N-1);
y2 = solve3(c, b, a, d);
puts "Approximate solutions:\n";
printf("%10s%10s%10s\n", "point", "O(h)", "O(h^2)");
disp([pts', y1, y2]);
# max(abs(y2 - y1))
