format long
puts "Task 4. Eigen values\n\n";
warning("off", "Octave:broadcast");
A = [-0.907011 -0.277165 0.445705;
     -0.277165 0.633725 0.077739;
     0.445705 0.077739 -0.955354]
y_0 = ones(size(A, 1), 1)
ep = 0.00001

puts "\n";
# Step 1. Find the maximum e.v. // completed
y_k = y_0;
l_k = 0;
l_k1 = 1000;
y_k1 = y_k;
iterno = 0;
do
  y_k1 = A * y_k;
  l_k = l_k1;
  l_k1 = max(y_k1 ./ y_k);
  y_k = y_k1 / max(abs(y_k1));
  iterno += 1;
until(abs(l_k1 - l_k) < ep)
puts "Using method of powers:\n";
ep = 0.00001
printf("#iters = %2i; max by absolute value eigen value = %3.16f\n", iterno, l_k1);
disp "Eigen vector: \n";
disp(y_k1 ./ max(y_k1));
puts "Difference: \n";
disp(max(abs(A * y_k1 - l_k1 * y_k1)));

puts "\n";
# Step 2. Method of scalar product //completed
y_k = y_0;
l_k = 0;
l_k1 = 1000;
y_k1 = y_k;
iterno = 0;
do
  y_k1 = A * y_k;
  l_k = l_k1;
  l_k1 = (y_k1' * y_k) / (y_k' * y_k) ;
  y_k = y_k1 / max(abs(y_k1));
  iterno += 1;
until(abs(l_k1 - l_k) < ep)
puts "Using method of a scalar product:\n";
ep = 0.00001
printf("#iters = %2i; max by absolute value eigen value = %3.16f\n", iterno, l_k1);
disp "Eigen vector: \n";
disp(y_k1 ./ max(y_k1));
puts "Difference: \n";
disp(max(abs(A * y_k1 - l_k1 * y_k1)));

puts "\n";
# Step 3. Reverse power method.
max_eig_A = l_k1;
B = A - max_eig_A * eye(size(A));
y_k = y_0;
l_k = 0;
l_k1 = 1000;
y_k1 = y_k;
iterno = 0;
do
  y_k1 = B * y_k;
  l_k = l_k1;
  l_k1 = (y_k1' * y_k) / (y_k' * y_k) ;
  y_k = y_k1 / max(abs(y_k1));
  iterno += 1;
until(abs(l_k1 - l_k) < ep)
l_k1 = l_k1 + max_eig_A;
puts "Using method of a scalar product for B = A - l_1 * E:\n";
ep = 0.00001
printf("#iters = %2i; max eigen value = %3.16f\n", iterno, l_k1);
puts "Difference: \n";
disp(max(abs(A * y_k1 - l_k1 * y_k1)));

puts "\n";
# Step 4. Find minimal by absolute value.
B = inv(A);
y_k = y_0;
l_k = 0;
l_k1 = 1000;
y_k1 = y_k;
iterno = 0;
do
  y_k1 = B * y_k;
  l_k = l_k1;
  l_k1 = (y_k1' * y_k) / (y_k' * y_k) ;
  y_k = y_k1 / max(abs(y_k1));
  iterno += 1;
until(abs(l_k1 - l_k) < ep)
l_k1 = 1 / l_k1;
puts "Using method of a scalar product for B = A^-1:\n";
ep = 0.00001
printf("#iters = %2i; min by absolute value eigen value = %3.16f\n", iterno, l_k1);
puts "Difference(between this calculation and one of the octave): \n";
disp(eig(A)(abs(eig(A)) == min(abs(eig(A)))) - l_k1);

puts "\n";
# Step 5. Find the lacking eigen value 
ev3 = trace(A) - max_eig_A - l_k1;
printf("With min by abs eigen value: %3.16f and max: %3.16f the thirds eigen values is %3.16f\n", l_k1, max_eig_A, ev3);
puts "Difference(between this calculation and one of the octave): \n";
disp(min(abs(ev3 - eig(A))));

puts "\n";
# Step 6. The method of Jacoby
A_cur = A;
i_k = 0;
j_k = 0;
V = eye(size(A));
niter = 0;
while(max(max(abs(A_cur - diag(diag(A_cur))))) > ep) 
  niter += 1;
  A_drop_diag = A_cur - diag(diag(A_cur));
  [i_k j_k] = find(abs(A_drop_diag) == max(max(abs(A_drop_diag))));
  i_k = i_k(1, 1);
  j_k = j_k(1, 1);
  next_v_mult = eye(size(A));
  d = sqrt((A_cur(i_k, i_k) - A_cur(j_k, j_k)) ^ 2 + 4 * A_cur(i_k, j_k)^2);
  s = sign(A_cur(i_k, j_k) * (A_cur(i_k, i_k) - A_cur(j_k, j_k))) * sqrt((1 - abs(A_cur(i_k, i_k) - A_cur(j_k, j_k)) / d) / 2);
  c = sqrt((1 + abs(A_cur(i_k, i_k) - A_cur(j_k, j_k)) / d) / 2);
  next_v_mult(i_k, i_k) = next_v_mult(j_k, j_k) = c;
  next_v_mult(i_k, j_k) = -s;
  next_v_mult(j_k, i_k) = s;
  V = V * next_v_mult;
  A_new = A_cur;
  A_new(:, i_k) = A_new(i_k, :) = c * A_cur(:, i_k) + s * A_cur(:, j_k);
  A_new(:, j_k) = A_new(j_k, :) = -s * A_cur(:, i_k) + c * A_cur(:, j_k);
  A_new(i_k, i_k) = c ^ 2 * A_cur(i_k, i_k) + 2 * c * s * A_cur(i_k, j_k) + s ^ 2 * A_cur(j_k, j_k);
  A_new(j_k, j_k) = s ^ 2 * A_cur(i_k, i_k) - 2 * c * s * A_cur(i_k, j_k) + c ^ 2 * A_cur(j_k, j_k);
  A_new(i_k, j_k) = A_new(j_k, i_k) = 0;
  A_cur = A_new;
endwhile
puts "Jacoby's rotation method\n";
ep = 0.00001
printf("#Iterations = %i\n", niter);
puts "Converted matrix: \n";
disp(A_cur);
puts "Eigen values of the matrix:\n";
disp(sort(diag(A_cur)));
puts "Eigen vectors:\n";
disp(V ./ max(V));
puts "Difference: \n";
disp(max(abs(V * diag(diag(A_cur)) - A * V)));

