function rootIntervals = separateRoots(f, A, B, eps)
  assert(B - A >= eps, "eps is too big")
  leftMargin = A;
  rightMargin = min(A + eps, B);
  rootIntervals = [];
  while leftMargin < B
    if (f(leftMargin) * f(rightMargin) <= 0)
      rootIntervals = [rootIntervals; leftMargin, min(rightMargin, B)];
    endif
    leftMargin += eps;
    rightMargin += eps;
  endwhile
endfunction
