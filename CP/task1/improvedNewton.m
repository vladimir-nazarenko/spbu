function [_roots, fst_approx, steps_n, diff] = improvedNewton(left_mar, right_mar, func, eps)
  function x = choose_notzero_derivative(left_mar, right_mar, func, recursion_level = 10)
    if recursion_level == 0 
      error("Can't choose appropriate first approximation. Maybe, function is constant?")
    endif
    x = (left_mar + right_mar) / 2;
    if deriv(func, x) == 0 
       x = choose_notzero_derivative(x, right_mar, func, recursion_level - 1)
    endif
  endfunction
  fst_approx = choose_notzero_derivative(left_mar, right_mar, func);
  old_approx = left_mar;
  approx = fst_approx;
  steps_n = 0;
  while abs(approx - old_approx) > eps
    old_approx = approx;
    approx = approx - func(approx) / deriv(func, fst_approx);
    steps_n++;
  endwhile
  diff = abs(func(approx));
  _roots = approx;
endfunction












