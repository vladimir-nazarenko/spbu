function ans = f(x = "print")
  if (x == "print")
    puts "f(x) = 1/10 * cos(x) * cos(x) - cos(x)\n";
  else
    ans =  1/10 * cos(x) * cos(x) - cos(x);
  endif
endfunction
