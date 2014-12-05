function [_roots, fst_approx, steps_n, diff] = bisection(left_mar, right_mar, func, eps)
  assert(eps > 0);
  assert(right_mar > left_mar);
  fst_mar = left_mar;
  sec_mar = right_mar;
  fst_approx = fst_mar;
  steps_n = 0;
  diff = 0;
  _roots = [];
  # check _roots on the edges !!! maybe this is incorrect?
  if func(fst_mar) == 0
    _roots = [_roots, fst_mar];
  endif
  if f(sec_mar) == 0
    _roots = [_roots, sec_mar];
  endif
  if size(_roots) != 0
    return;
  endif
  # actually run the method
  fst_approx = (right_mar + left_mar) / 2;
  while abs(func(fst_mar)) >= eps
    center = (sec_mar + fst_mar) / 2;
    if func(center) * func(sec_mar) <= 0
      fst_mar = center;
    elseif func(center) * func(fst_mar) <= 0
      sec_mar = center;
    else
      error("wrong interval");
    endif
    steps_n++;
  endwhile
  _roots = fst_mar;
  diff = abs(func(fst_mar));
endfunction      
		    

  
			      
						
