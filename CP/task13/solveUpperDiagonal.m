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
