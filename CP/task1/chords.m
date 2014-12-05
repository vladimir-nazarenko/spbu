function [_roots, first_approx, steps_n, diff] = chords(left_mar, right_mar, func, eps)
  first_approx = (left_mar + right_mar) / 2;
  old_approx = left_mar;
  very_old_approx = right_mar;
  approx = first_approx;
  steps_n = 0;
  while abs(approx - old_approx) > eps
    old_approx = approx;
    approx = approx - (func(approx) * (old_approx - very_old_approx)) / (func(old_approx) - func(very_old_approx));
    steps_n++;
  endwhile
  diff = abs(func(approx));
  _roots = approx;
endfunction
