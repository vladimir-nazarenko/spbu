function [_roots, first_approx, steps_n, diff] = simpleIteration(left_mar, right_mar, func, eps)
  function x = choose_notzero(left_mar, right_mar, func, recursion_level = 10)
    if recursion_level == 0 
      error("Can't choose appropriate first approximation. Maybe, function is constant?")
    endif
    x = (left_mar + right_mar) / 2;
    if func(x) == 0 
       x = choose_notzero(x, right_mar, func, recursion_level - 1)
    endif
  endfunction
  first_approx = choose_notzero(left_mar, right_mar, func);
  old_approx = left_mar;
  approx = first_approx;
  steps_n = 0;
  while abs(approx - old_approx) > eps
    old_approx = approx;
    approx = acos(1/10 * cos(approx) * cos(approx));
    steps_n++;
  endwhile
  diff = abs(func(approx));
  _roots = approx;
endfunction
