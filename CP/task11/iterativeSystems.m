warning("off", "Octave:broadcast");
puts "Task 3. Iterative solving system of linear equations\n";
A = [2.17426 0.39894 0.22180; 0.39894 3.11849 0.60613; 0.22180 0.60613 4.79210]
b = [2.13909 6.30779 5.98485]'
ep = 0.00001
x_0 = ones(size(b))

puts "\n";
# step 1: find the solution by the gauss scheme.
# Using algorithm from the previous task
function x = solveUpperDiagonal(A, b)
  assert(size(A, 1) == size(A, 2) && length(b) == length(A));
  x = zeros(1, length(A));
  for i = fliplr(1:length(A))
    s = x(1, i+1:length(A)) * A(i, i+1:length(A))';
    if all(size(s) == [0 0]) 
       s = zeros(1, 1);
    endif
    x(i) = (b(i) - s) / A(i, i);
  endfor
endfunction

function [B, dt] = makeUpperDiagonal(A)
#  assert(size(A, 1) >= size(A, 2));
  N = size(A, 1);
  dt = 1;
  for i = 1:N-1
    # find all indexes of rows with non-zero first item
    rows = find(A(i:end, i) != 0) + i - 1;
    # check that there are such rows
    if length(rows) != 0
      baseRowIndex = rows(1);
      # assert(baseRowIndex >= i);
      # flip first and base rows
      if i != baseRowIndex
	temp = A(i, :);
	A(i, :) = A(baseRowIndex, :);
	A(baseRowIndex, :) = temp;
	dt = -dt;
      endif
      dt = dt / A(i, i);
      A(i, :) = A(i, :) ./ A(i, i);
      # ready to subtract a row
      sub = find(A(i+1:end, i) != 0) + i;
      A(sub, :) = A(sub, :) - A(i, :) .* A(sub, i);
    endif
  endfor
  dt = dt / A(N, N);
  A(N, :) = A(N, :) ./ A(N, N);
  dt = 1 / dt;
  B = A;
endfunction

puts "Solution found by Gauss method: \n";
ud = makeUpperDiagonal([A b]);
sol = solveUpperDiagonal(ud(:, 1:end-1), ud(:, end));
disp(sol);
printf("Difference: %3.16f\n", max(abs(A * sol' - b)));
# disp(A \ b)

puts "\n";
# Step 2. Find the borders of the spectrum for the matrix A

m = min(diag(A) - sum(abs(A - diag(diag(A)))')');
M = max(diag(A) + sum(abs(A - diag(diag(A)))')');
printf("m and M such as for all eigen value v met m <= v <= M: m = %f; M = %f\n", m, M);
alpha = 2 / (m + M);
printf("Optimal parameter alpha = 2 / (m + M): alpha = %f\n", alpha);

puts "\n";
# Step 3. Transform the system:

B_alpha = eye(size(A)) - alpha .* A;
c_alpha = alpha .* b;
puts "Transform system to special form x = B_alpha * x + c_alpha, where B_alpha = E - alpha * A, c_alpha = alpha * b\n";
B_alpha
c_alpha
puts "Norm of the B_alpha: ";
b_norm = max(sum(abs(B_alpha')));
disp(b_norm);

puts "\n";
# Step 4. Find the apriori estimation of the k for a given eps
x_1 = B_alpha * x_0 + c_alpha;
k = log(ep * (1 - b_norm) / max(abs(x_1 - x_0))) / log(b_norm);
printf("Apriori estimation of k: %f\n", k);

puts "\n";
# Step 5. Find the solution using the simple iteration method
puts "\n************Simple iteration method**************\n";
current_sol = x_0;
prev_sol = x_0;
iteration = 1;
do 
  prev_sol = current_sol;
  current_sol = B_alpha * current_sol + c_alpha;
  printf("Iteration: %2i Error: %3.16f Aproiri estimation: %3.16f Aposteriori estimation: %3.16f\n", iteration, max(abs(sol'-current_sol)), b_norm ^ iteration / ((1 - b_norm) * max(abs(x_1 - x_0))), b_norm * max(abs(prev_sol - current_sol)) / (1 - b_norm));
  iteration += 1;
until(max(abs(current_sol - prev_sol)) <= ep)
puts "Solution: \n";
disp(current_sol);
printf("Difference: %3.16f\n", max(abs(A * current_sol - b)));

puts "\n";
# Step 6. Zeydel's method
puts "\n************Zeydel's method**************\n";
current_sol = x_0;
iteration = 1;
do 
   current_sol(1, 1) = (b(1) - A(1, 2:end) * current_sol(2:end, 1)) / A(1, 1);
   for i=2:size(b, 1)-1
     current_sol(i, 1) = (b(i) - A(i, 1:i-1) * current_sol(1:i-1, 1) - A(i, i+1:end) * current_sol(i+1:end, 1)) / A(i, i) ;
   endfor
   current_sol(end, 1) = (b(end) - A(end, 1:end-1) * current_sol(1:end-1, 1)) / A(end, end);
   printf("Iteration: %2i Error: %3.16f\n", iteration, max(abs(current_sol - sol')));
   iteration += 1;
until(max(abs(A * current_sol - b)) <= ep)
puts "Solution: \n";
disp(current_sol);
printf("Difference: %3.16f\n", max(abs(A * current_sol - b)));
