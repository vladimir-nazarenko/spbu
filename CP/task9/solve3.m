# function solves 3-diagonal system of linear equations by difference method
function Y = solve3(upperDiagonal, mainDiagonal, lowerDiagonal, freeTerms, out = false)
  if (out) 
    puts "Solving matrix equation\n";
  endif
  # checks
  assert(all(size(upperDiagonal) == size(mainDiagonal)), "Wrong dimentions of the arguments");
  assert(all(size(mainDiagonal) == size(lowerDiagonal)), "Wrong dimentions of the arguments");
  assert(all(size(lowerDiagonal) == size(freeTerms)), "Wrong dimentions of the arguments");
  assert(size(upperDiagonal)(1) == 1 && size(upperDiagonal)(2) > 1, 'Wrong dimentions of the arguments');
  # print extended matrix
  N = size(upperDiagonal)(2);
  matrix = diag(mainDiagonal); 
  matrix(1:end-1, 2:end) += diag(upperDiagonal)(1:end-1, 1:end-1); # assumes matrix is at least 2x2
  matrix(2:end, 1:end-1) += diag(lowerDiagonal)(2:end, 2:end);
  matrix = [matrix, freeTerms'];
  if (out)
    puts "Extended matrix\n"; disp(matrix); 
  endif
  # solving
  Y = zeros(1, N);
  coeffM = zeros(1, N); # contains only N - 1 elements, first is displacement
  coeffK = zeros(1, N); # contains only N - 1 elements, first is displacement
  # compute the coefficients, forwarding
  coeffM(2) = -upperDiagonal(1) / mainDiagonal(1);
  coeffK(2) = freeTerms(1) / mainDiagonal(1);
  for i = 3:N
    denominator = mainDiagonal(i - 1) + lowerDiagonal(i - 1) * coeffM(i - 1);
    coeffK(i) = (freeTerms(i - 1) - lowerDiagonal(i - 1) * coeffK(i - 1)) / denominator;
    coeffM(i) = -upperDiagonal(i - 1) / denominator;
  endfor
  if (out) 
    puts "Coefficients:\n"; disp(coeffK); disp(coeffM); 
  endif
  # compute the solution, backwarding
  Y(N) = (freeTerms(N) - coeffK(N) * lowerDiagonal(N)) / (mainDiagonal(N) + lowerDiagonal(N) * coeffM(N));
  for i = fliplr(1:N-1)
    Y(i) = coeffK(i + 1) + coeffM(i + 1) * Y(i + 1);
  endfor
  if (out)
    puts "Solution:\n"; disp(Y'); puts "Difference:\n"; disp(matrix(:, 1:end-1) * Y' - freeTerms'); 
  endif
  Y = Y';
endfunction

%!test
%! testMatrix = [1, 3, 0, 0; 4, 6, 7, 0; 0, 5, 8, 9; 0, 0, 1, 9];
%! testFree = [1, 2, 3, 4];
%! assert (solve3([diag(testMatrix(1:end-1, 2:end)); 0]', diag(testMatrix)', [0; diag(testMatrix(2:end, 1:end-1))]', testFree), testMatrix \ testFree', 10 ^ -16)

%!test
%! testMatrix = [1, 2, 0; 3, 4, 5; 0, 6, 7];
%! testFree = [1, 2, 3];
%! assert (solve3([diag(testMatrix(1:end-1, 2:end)); 0]', diag(testMatrix)', [0; diag(testMatrix(2:end, 1:end-1))]', testFree), testMatrix \ testFree', 10 ^ -16)
