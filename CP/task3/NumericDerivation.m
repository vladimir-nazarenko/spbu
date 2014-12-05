# make a table
puts "Numeric derivation\n";
knotNumber = input("Enter number of points: ");
assert(knotNumber >= 3);
a = input("Enter the left margin: ");
b = input("Enter the right margin: ");
step = (b - a) / (knotNumber - 1);
assert(a < b);
knots = genPoints(a, b, knotNumber);
valuesInKnots = f(knots);
puts "Function values at interpolation points\n";
tableOfValues = [knots; valuesInKnots]';
disp(tableOfValues);
# derivative at the first point
tableOfValues(1, 3) = ( - 3 * tableOfValues(1, 2) + 4 * tableOfValues(2, 2) - tableOfValues(3, 2)) / (2 * step);
# derivative at the last point
tableOfValues(end, 3) = (3 * tableOfValues(end, 2) - 4 * tableOfValues(end - 1, 2) + tableOfValues(end - 2, 2)) /  (2 * step);
# in the middle
tableOfValues(2:end-1, 3) = (tableOfValues(3:end, 2) - tableOfValues(1:end - 2, 2)) ./ (2 * step);
# Errors
tableOfValues(:, 4) = tableOfValues(:, 3) - deriv(@f, knots)';
tableOfValues(2:end-1, 5) = (tableOfValues(3:end, 2) - 2 * tableOfValues(2:end-1, 2) + tableOfValues(1:end-2, 2)) / (step ^ 2);
tableOfValues
