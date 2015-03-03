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

#testMatrix = [1 2 3; 0 2 3; 0 0 3];
#testFree = [1, 2, 3];
#solveUpperDiagonal(testMatrix, testFree)

#B = makeUpperDiagonal([A b])
#solveUpperDiagonal(B(:, 1:end-1), B(:, end))
#A \ b

#inverse(A)
#inv(A)

#[B, dt] = makeUpperDiagonal(A)

puts "Joined matrix:\n";
disp([A b]);

puts "Precise solution:\n";
disp((A \ b)');

puts "Find solution using the Gauss scheme:\n";
B = makeUpperDiagonal([A b]);
sol = solveUpperDiagonal(B(:, 1:end-1), B(:, end));
disp(sol);

puts "Find the solution using the square root method:\n";
[x, U] = solveSquareRoot(A, b);
disp(x);

puts "Find the determinant of the matrix:\n";
[B, dt] = makeUpperDiagonal(A);
disp(dt);

puts "Find the reversed matrix:\n";
disp(inverse(A));

puts "Find the solution using the reversed matrix:\n";
disp((inverse(A) * b)');

puts "Find the conditional number of the matrix using inf norm\n";
norm = max(sum(abs(A'))) * max(sum(abs(inverse(A'))));
disp(norm);
