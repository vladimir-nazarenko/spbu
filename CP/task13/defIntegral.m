function I = defIntegral(a, b, h, Y)
  # may want to do some complex assert statement for dimension
  # Y should contain 2 * N - 1 points
  # a - b / h should be equal to N where N is number of nodes
  odd = 1:2:size(Y, 1);
  even = 2:2:size(Y, 1);
  I = (h / 6) * sum((Y(odd(1:end-1), :) .+ 4 .* Y(even, :) .+ Y(odd(2:end), :)), 1);
endfunction 
