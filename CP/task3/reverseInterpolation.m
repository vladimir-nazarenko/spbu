# make a table
puts "Reverse Interpolation\n";
knotNumber = input("Enter the number of points: ");
assert(knotNumber > 1);
a = input("Enter the left margin: ");
b = input("Enter right margin: ");
assert(a < b);
knots = genPoints(a, b, knotNumber);
valuesInKnots = f(knots);
puts "Function values at interpolation points\n";
tableOfValues = [knots; valuesInKnots]';
disp(tableOfValues);
while(true)
y = input("Enter the interpolation point: ");
assert(min(valuesInKnots) <= y && y <= max(valuesInKnots));
degOfPoly = min(knotNumber - 1, 7);
# FIRST approach
puts "First approach\n";
reversedTable = [valuesInKnots; knots]';
val = interpolate(reversedTable, degOfPoly, y);
diff = abs(f(val) - y');
puts "Interpolated parameter: ";
disp(val);
puts "\nDifference value: ";
disp(diff);
# SECOND approach
puts "Second approach\n";
# build the polynomial
centerPoint = NaN;
# Check if we interpolate in the node of interpolation
if (sum(valuesInKnots == y) > 0) 
   centerPoint = y;
else
  centerPoint = (min(valuesInKnots(valuesInKnots > y)) - max(valuesInKnots(valuesInKnots < y))) / 2;
endif
sortedTable = sortrows([abs(centerPoint - valuesInKnots)', tableOfValues])(1:degOfPoly + 1, 2:end);
n = degOfPoly;
# compute separated differences
sepDiff = zeros(n + 1, n + 2);
sepDiff(1:end, 1:2) = sortedTable(1:end, 1:2);
for j = 3:n+2
  for i = 1:(n + 1 - j + 2)
    step = j - 2;
    sepDiff(i, j) = (sepDiff(i + 1, j - 1) - sepDiff(i, j - 1)) / (sepDiff(i + step, 1) - sepDiff(i, 1));
  endfor
endfor
# Build polynom in Newtown's form by defnition
polyN = sepDiff(1, 2);
cumFactor = 1;
for i = 1:n
  cumFactor = conv(cumFactor, [1, -sepDiff(i, 1)]);
  newTerm = conv(cumFactor, sepDiff(1, i + 2));
  polyN = [zeros(1, size(newTerm,2)-size(polyN,2)) polyN] + [zeros(1, size(polyN,2)-size(newTerm,2)) newTerm];
endfor
polyWithRoot = polyN - [zeros(1, size(polyN, 2) - 1) y];
root = bisection(a, b, polyWithRoot, 10 ^ -6);
puts "\nInterpolated parameter: ";
disp(root);
puts "\nValue of the difference: ";
disp(abs(f(root) - y'));
end
