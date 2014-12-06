# generate moment for function abs(x ^ (-1/2))
function mu = getMoment(order, a, b)
mu = 1 ./ (order + 1/2) .* (b .^ order .* (sign(b) *abs(sqrt(b))) - a .^ order .* (sign(a) * abs(sqrt(a))));
endfunction
