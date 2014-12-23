function y = solution(x)
  y = (sqrt(7) .* tan(sqrt(7) .* x ./ 2) .+ 1) ./ 4;
  #y = exp(sin(x) .- 2 * x);
endfunction
