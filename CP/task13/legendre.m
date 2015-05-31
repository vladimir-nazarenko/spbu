################ legendre ???
# calculate the legendre's polynomial
function l = legendre(N)
  assert(N >= 0)
  p0 = [1];
  p1 = [1, 0];
  if(N == 0)
     l = p0;
  elseif(N == 1)
    l = p1;
  else
    cur = p1;
    prev = p0;
    for i = 2:N
      p1 = conv([(2 * i - 1) / i, 0], cur);
      p2 = -conv((i - 1) / i, prev);
      calc = [zeros(1, size(p1,2)-size(p2,2)) p2] + [zeros(1, size(p2,2)-size(p1,2)) p1];
      prev = cur;
      cur = calc;
    endfor
    l = cur;
  endif
endfunction
