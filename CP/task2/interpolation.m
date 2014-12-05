# make a table
knotNumber = input("Enter number of points");
assert(knotNumber > 1);
a = input("Enter left margin");
b = input("Enter right margin");
assert(a < b);
knots = genPoints(a, b, knotNumber);
valuesInKnots = f(knots);
puts "Function values at interpolation points\n";
tableOfValues = [knots; valuesInKnots]';
disp(tableOfValues);
# build the polynomial
degOfPoly = input("Enter the degree of the polinomial");
assert(degOfPoly <= knotNumber);
x = input("Enter interpolation point");
sortedTable = sortrows([abs(x - knots)', tableOfValues])(1:degOfPoly + 1, 2:end);
puts "SortedTable\n";
disp(sortedTable);
n = degOfPoly;
sepDiff = zeros(n + 1, n + 2);
sepDiff(1:end, 1:2) = sortedTable(1:end, 1:2);
for j = 3:n+2
  for i = 1:(n + 1 - j + 2)
    step = j - 2;
    sepDiff(i, j) = (sepDiff(i + 1, j - 1) - sepDiff(i, j - 1)) / (sepDiff(i + step, 1) - sepDiff(i, 1));
  endfor
endfor
polyN = ones(n + 1, 1);
for i = 2:n+1
    polyN(i:end, 1) = polyN(i:end, 1) .* (x - sortedTable(i - 1, 1));
endfor
polyN = sepDiff(1, 2:end) * polyN;
puts "Value of Newtons polynomial\n";
polyN
puts "Error\n";
abs(f(x) - polyN)
l = zeros(n + 1, 1);
w = zeros(n + 1, 1);
for i=1:n+1
    w(i) = x - sortedTable(i, 1);
endfor
for i=1:n+1
    w1 = zeros(n + 1, 1);
    for k=1:n+1
      w1(k) = sortedTable(i, 1) - sortedTable(k, 1);
    endfor
  l(i) = prod(w(1:i-1)) * prod(w(i+1:end)) / (prod(w1(1:i-1)) * prod(w1(i+1:end)));
endfor
polyL = sortedTable(1:end, 2)' * l;
puts "Value of Lagrange polynomial\n";
polyL
puts "Error\n";
abs(f(x) - polyL)
