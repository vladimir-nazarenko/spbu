warning ("off", "Octave:broadcast");
puts "Task 6. The diffusion equation";
a = 0;
b = 1;
A = 1;
printf("Solving equation for x from 0 to 1; parameter a = 1\n"); 
N_h = 10;
N_tau = 1000;
tau = 0.001;
h = (b - a) / N_h;
assert(tau <= h * h / 2);
T = tau * N_tau;
printf("Using grid for x with %f nodes. Therefore h = %f; tau = %f; #iters=%f\n", N_h, h, tau, N_tau);
assert(T > 0);
xs = linspace(a, b, N_h);
ts = linspace(0, T, N_tau+1);
magic_const = tau * A / (h * h);

puts "Obvious difference scheme:\n";
u = zeros(N_h, N_tau+1);
u(:, 1) = g(xs);
u(1, :) = alpha(ts);
u(end, :) = beta(ts);
for j = 2:(N_tau+1)
  for i = 2:(N_h-1)
    u(i, j) = tau * f(xs(i), ts(j-1)) + u(i + 1, j - 1) * magic_const + (1 - 2 * magic_const) * u(i, j - 1) + magic_const * u(i-1, j-1);
  endfor
endfor
printf("t = %f\n", T);
out = [xs', u(:, end), abs(accurate(xs, T) - u(:, end))]';
printf("Point: %f Solution: %3.16f Error: %3.16f\n", out);

N_h = 10;
N_tau = 1000;
tau = 0.0005;
h = (b - a) / N_h;
T = tau * N_tau;
printf("Using grid for x with %f nodes. Therefore h = %f; tau = %f; #iters=%f\n", N_h, h, tau, N_tau);
assert(T > 0);
xs = linspace(a, b, N_h);
ts = linspace(0, T, N_tau);
puts "Implicit difference method:\n";
u = zeros(N_h, N_tau);
u(:, 1) = g(xs);
u(1, :) = alpha(ts);
u(end, :) = beta(ts);
for k = 2:N_tau
    # build 3-diagonal matrix
    F = zeros(N_h, 1);
    F(1, 1) = alpha(ts(k));
    F(N_h, 1) = beta(ts(k));
    F(2:(N_h-1)) = u(2:(N_h-1), k-1) + tau .* f(xs(2:end-1), ts(k));
    mainDiagonal = [1; ones(N_h-2, 1) .* (1 + 2 * A * tau / (h * h)); 1];
    lowerDiagonal = [ones(N_h-1, 1) .* (-A * tau / (h * h)); 0];
    upperDiagonal = [0; ones(N_h-1, 1) .* (-A * tau / (h * h))];
    u(:, k) = solve3(upperDiagonal', mainDiagonal', lowerDiagonal', F');
endfor
printf("t = %f\n", T);
out = [xs', u(:, end), abs(accurate(xs, T) - u(:, end))]';
printf("Point: %f Solution: %3.16f Error: %3.16f\n", out);
