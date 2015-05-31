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
