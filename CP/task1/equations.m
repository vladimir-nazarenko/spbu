puts "Численные методы решения нелинейных алгебраических и трансцедентных уравнений\n";

A = 0
B = 10
f;
eps = 0.0001
h = 0.1;
puts "Root intervals:\n";
intervals = separateRoots(@f, A, B, h);
output_precision(4);
disp(intervals);
puts "Running root elaboration:";
output_precision(9);
for i = 1:rows(intervals)
  puts "\nInterval: ";
  disp(intervals(i, :));
  puts "\nBisection method\n";
  [roots, approx, N, diff] = bisection(intervals(i, 1), intervals(i, 2), @f, eps)
  puts "\nNewton's method\n";
  [roots, approx, N, diff] = newton(intervals(i, 1), intervals(i, 2), @f, eps)
  puts "\nImproved Newton's method\n";
  [roots, approx, N, diff] = improvedNewton(intervals(i, 1), intervals(i, 2), @f, eps)
  puts "\nChord method\n";
  [roots, approx, N, diff] = chords(intervals(i, 1), intervals(i, 2), @f, eps)
  puts "\nSimple iteration\n";
  [roots, approx, N, diff] = simpleIteration(intervals(i, 1), intervals(i, 2), @f, eps)
  puts "________________________________________";
endfor
