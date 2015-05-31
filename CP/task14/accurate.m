function y = accurate(x, t)
  y = zeros(length(x), length(t));
  for i = 1:length(x)
      for j = 1:length(t)
	y(i, j) = exp(-t(j) / 4) * cos(x(i) / 2) + x(i) * (1 - x(i)) / (10 + t(j));
      endfor
  endfor
endfunction
