function res = f(x=NaN)
  if isnan(x)
     puts "Function is sin(x)\n";
  else
    res = sin(x);
  endif
endfunction
