function [_roots, fst_approx, steps_n, diff] = newton(left_mar, right_mar, func, eps)
  fst_approx = (left_mar + right_mar) / 2;
  old_approx = left_mar;
  approx = fst_approx;
  steps_n = 0;
  while abs(approx - old_approx) > eps
    old_approx = approx;
    approx = approx - func(approx) / deriv(func, approx);
    steps_n++;
  endwhile
  diff = abs(func(approx));
  _roots = approx;
endfunction
