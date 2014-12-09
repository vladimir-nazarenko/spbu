function res = f(x=NaN)
  if isnan(x)
     puts "Function is cos(x)\n";
  else
    res = cos(x);
#    res = sqrt(1 - x .^ 2);
#    res = exp(-x .^ 4) - 2 * x;
  endif
endfunction
