format long
puts "Task 5. Complex integration formulas\n";
f(NaN);
a = input("Enter the left margin: ");
b = input("Enter the right margin: ");
m = input("Enter number of intervals: ");
assert(m > 0);
assert(a < b);
# matrix for storing computed approximations of the integral
# rows mean three methods to compute: middle rectangle, trapeeze and Simpson's method
# columns mean ways to compute: compelx with h distance, complex with h / q distance, Runge's principal
J = zeros(3, 3);
# compute with h distance
points = genPoints(a, b, m + 1);
h = (b - a) / m;
printf("%-35s %3.16f\n\n", "Actual value:", fint(b) - fint(a));

J(1, 1) = h * sum(f(points(1:end - 1) + h / 2));
printf("%-35s %3.16f %-15s %3.16f\n", "Middle rectangle gave an answer:", J(1, 1), "with an error", abs(J(1, 1) - fint(b) + fint(a)));
J(2, 1) = (h / 2) * sum(f(points(1:end-1)) + f(points(2:end)));
printf("%-35s %3.16f %-15s %3.16f\n", "Trapeze gave an answer:", J(2, 1), "with an error", abs(J(2, 1) - fint(b) + fint(a)));
J(3, 1) = (h / 6) * sum((f(points(1:end-1)) + 4 * f(points(1:end-1) + h / 2) + f(points(2:end)))); 
printf("%-35s %3.16f %-15s %3.16f\n", "Simpson's method gave an answer:", J(3, 1), "with an error", abs(J(3, 1) - fint(b) + fint(a)));
puts "\n\n";

# compute with h/q distance
q = input("Enter the fraction value (q): ");
points = genPoints(a, b, q * m + 1);
h = (b - a) / (q * m);
puts "Values with h/q distance:\n";

J(1, 2) = h * sum(f(points(1:end - 1) + h / 2));
printf("%-35s %3.16f %-15s %3.16f %-15s %1.16f\n", "Middle rectangle gave an answer:", J(1, 2), "with an error", abs(J(1, 2) - fint(b) + fint(a)), "with difference:", abs(J(1, 1) - J(1, 2)));
J(2, 2) = (h / 2) * sum(f(points(1:end-1)) + f(points(2:end)));
printf("%-35s %3.16f %-15s %3.16f %-15s %1.16f\n", "Trapeze gave an answer:", J(2, 2), "with an error", abs(J(2, 2) - fint(b) + fint(a)), "with difference:", abs(J(2, 1) - J(2, 2)));
J(3, 2) = (h / 6) * sum((f(points(1:end-1)) + 4 * f(points(1:end-1) + h / 2) + f(points(2:end)))); 
printf("%-35s %3.16f %-15s %3.16f %-15s %1.16f\n", "Simpson's method gave an answer:", J(3, 2), "with an error", abs(J(3, 2) - fint(b) + fint(a)), "with difference:", abs(J(3, 1) - J(3, 2)));
puts "\n\n";

# compute with Runge's principal
puts "Values with Runge's principal:\n";

J(1, 3) = J(1, 2) + ((J(1, 2) - J(1, 1)) / ((q ^ 2) - 1));
printf("%-35s %3.16f %-15s %3.16f\n", "Middle rectangle gave an answer:", J(1, 3), "with an error", abs(J(1, 3) - fint(b) + fint(a)));
J(2, 3) = J(2, 2) + ((J(2, 2) - J(2, 1)) / ((q ^ 2)  - 1));
printf("%-35s %3.16f %-15s %3.16f\n", "Trapeze gave an answer:", J(2, 3), "with an error", abs(J(2, 3) - fint(b) + fint(a)));
J(3, 3) = J(3, 2) + ((J(3, 2) - J(3, 1)) / ((q ^ 4) - 1));
printf("%-35s %3.16f %-15s %3.16f\n", "Simpson's method gave an answer:", J(3, 3), "with an error", abs(J(3, 3) - fint(b) + fint(a)));
puts "\n\n";



format short
