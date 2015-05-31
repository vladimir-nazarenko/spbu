puts "Task 2. Precise solving of systems of equations\n";
warning("off", "Octave:broadcast")
A = [5.02 7 6 5.02;
     7 10.02 8 7;
     6 8 10.02 9;
     5.02 7 9 10.02]
b = [23 32 33 31]'


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

function x = solveLowerDiagonal(A, b)
  assert(size(A, 1) == size(A, 2) && length(b) == length(A));
  x = zeros(1, length(A));
  for i = 1:length(A)
    s = x(1, 1:i - 1) * A(i, 1:i - 1)';
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

function [B, dt] = makeUpperDiagonalLazy(A)
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
	temp = A(i, (i+1):end);
	A(i, (i+1):end) = A(baseRowIndex, (i+1):end);
	A(baseRowIndex, (i+1):end) = temp;
	dt = -dt;
      endif
      dt = dt / A(i, i);
      A(i, (i+1):end) = A(i, (i+1):end) ./ A(i, i);
      # ready to subtract a row
      sub = find(A(i+1:end, i) != 0) + i;
      A(sub, (i+1):end) = A(sub, (i+1):end) - A(i, (i+1):end) .* A(sub, i);
    endif
  endfor
  dt = dt / A(N, N);
  A(N, N:end) = A(N, N:end) ./ A(N, N);
  dt = 1 / dt;
  B = A;
endfunction


function B = inverse(A)
  assert(size(A, 1) == size(A, 2));
  AE = [A eye(size(A))];
  [AE, _] = makeUpperDiagonal(AE);
  N = size(A, 1);
  # make left part of matrix diagonal
  for i = 2:N
    AE(1:i-1, :) = AE(1:i-1, :) - AE(1:i-1, i) .* AE(i, :);
  endfor
  B = AE(:, N+1:end);
endfunction

function [x, U] = solveSquareRoot(A, b)
  assert(size(A, 1) == size(A, 2) && length(b) == length(A));
  # method of the square root
  # the idea is to decompose A as U'*U where U is upper-triagonal and A' = A
  U = zeros(size(A));
  for i = 1:length(A)
    U(i, i) = sqrt(A(i, i) - sumsq(U(1:i-1, i)));
    for j = i+1:length(A)
      U(i, j) = (1 / U(i, i)) * (A(i, j) - U(1:i-1, i)' * U(1:i-1, j));
    endfor
  endfor
  y = solveLowerDiagonal(U', b);
  x = solveUpperDiagonal(U, y');
endfunction

function Y = removeBottom(X)
  Y = eye(size(X));
  for i = 1:size(Y, 1)
      for j = 1:size(Y, 2)
	if (j > i) 
	   Y(i, j) = X(i, j);
	endif
      endfor
  endfor
endfunction

#testMatrix = [1 2 3; 0 2 3; 0 0 3];
#testFree = [1, 2, 3];
#solveUpperDiagonal(testMatrix, testFree)

#B = makeUpperDiagonal([A b])
#solveUpperDiagonal(B(:, 1:end-1), B(:, end))
#A \ b

#inverse(A)
#inv(A)

#[B, dt] = makeUpperDiagonal(A)

#[C, dt] = makeUpperDiagonalLazy(A)



puts "Joined matrix:\n";
disp([A b]);

puts "Precise solution:\n";
pres = (A \ b)';
disp(pres);
printf("max(Ax-b) = %3.16f\n", max(A * pres' - b));

B = makeUpperDiagonalLazy([A b]);
puts "Converted by compact gauss transformation matrix:\n";
disp(B);
puts "Find solution using the compact Gauss scheme:\n";
B = removeBottom(B);
#B = makeUpperDiagonal([A b])
sol = solveUpperDiagonal(B(:, 1:end-1), B(:, end));
disp(sol);
printf("max(abs(Ax-b)) = %3.16f\n", max(abs(A * sol' - b)));

puts "Find the solution using the square root method:\n";
[x, U] = solveSquareRoot(A, b);
disp(x);
printf("max(abs(Ax-b)) = %3.16f\n", max(abs(A * x' - b)));

puts "Find the determinant of the matrix:\n";
dt = prod(diag(U)) ^ 2;
disp(dt);
puts "Differense between computed by square root and octave's:\n";
disp(abs(dt - det(A)));

puts "Find the reversed matrix:\n";
disp(inverse(A));
puts "Difference:\n";
disp(max(max(abs(inverse(A) - inv(A)))));

puts "Find the solution using the reversed matrix:\n";
disp((inverse(A) * b)');
printf("max(abs(Ax-b)) = %3.16f\n", max(abs(A * (inverse(A) * b) - b)));

puts "Find the conditional number of the matrix using inf norm\n";
norm = max(sum(abs(A'))) * max(sum(abs(inverse(A'))));
disp(norm);

