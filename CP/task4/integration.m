format long
puts "Task 4. Numerical integration\n";
f(NaN);
a = input("Enter the left margin: ");
b = input("Enter the right margin: ");
assert(a < b)
puts "Actual value: ";
disp(fint(b) - fint(a));
puts "Left rectangle: ";
val = (b - a) * f(a); 
disp(val);
puts "Error: ";
disp(abs(val - fint(b) + fint(a)));
puts "Right rectangle: ";
val = (b - a) * f(b); 
disp(val);
puts "Error: ";
disp(abs(val - fint(b) + fint(a)));
puts "Middle rectangle: ";
val = (b - a) * f((a + b) / 2); 
disp(val);
puts "Error: ";
disp(abs(val - fint(b) + fint(a)));
puts "Trapeze: ";
val = (b - a) * 1/2 * (f(a) + f(b)); 
disp(val);
puts "Error: ";
disp(abs(val - fint(b) + fint(a)));
puts "Simpson: ";
val = (b - a) * 1/6 * (f(a) + f(b) + 4 * f((a + b) / 2)); 
disp(val);
puts "Error: ";
disp(abs(val - fint(b) + fint(a)));
puts "Three eight: ";
val = (b - a) * 1/8 * (f(a) + 3 * f(a + (b - a) / 3) + 3 * f(b - (b - a) / 3) + f(b)); 
disp(val);
puts "Error: ";
disp(abs(val - fint(b) + fint(a)));

format short
