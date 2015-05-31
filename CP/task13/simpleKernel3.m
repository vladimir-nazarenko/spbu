function [X Y] = simpleKernel3(x, y)
#  Y = [ones(size(y)) ./ 2, -y ./ 4, zeros(size(y))];
#  X = [ones(size(x)), x, zeros(size(x))];
  Y = [ones(size(y)), -(y - 0.5), (y - 0.5) .^ 2];
  X = [1 ./ (x .^ 2 + 2.25), 1 ./ (x .^ 2 + 2.25) .^ 2, (-(x .^ 2) - 1.25) ./ ((x .^ 2 + 2.25) .^ 3)];
endfunction
