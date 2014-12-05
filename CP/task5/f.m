function res = f(x=NaN)
  if isnan(x)
     puts "Function is cos(x)\n";
  else
    res = cos(x);
#    res = x;
#    res = x .^ 3 + 2.16 * x .^ 2 - 0.17 * x - 0.04;
  endif
endfunction
