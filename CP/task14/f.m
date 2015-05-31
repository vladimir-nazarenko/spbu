function y = f(x, t)
  y = zeros(length(x), length(t));
  for i = 1:length(x)
      for j = 1:length(t)
	y(i, j) = -x(i) * (1 - x(i)) / ((10 + t(j)) ^ 2) + 2 / (10 + t(j));
      endfor
  endfor
endfunction
