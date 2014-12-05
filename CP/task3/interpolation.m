format long
# make a table
knotNumber = input("Enter number of points (m+1 value)");
assert(knotNumber > 1);
a = input("Enter left margin");
b = input("Enter right margin");
assert(a < b);
knots = genPoints(a, b, knotNumber);
step = (b - a) / (knotNumber - 1);
valuesInKnots = f(knots);
puts "Function values at interpolation points\n";
tableOfValues = [knots; valuesInKnots]';
disp(tableOfValues);


while(true)
  # build the polynomial
  degOfPoly = input("Enter the degree of the polinomial (should be less or equal m) ");
  while degOfPoly >= knotNumber
    puts "Degree polynomial should be less or equal m!\n";
    degOfPoly = input("Enter the degree of the polynomial ");
  endwhile
  assert(0 < degOfPoly <= knotNumber);
  printf("Enter the interpolation point between\n %f and %f for interpolating in the beginning;\n %f and %f -- in the middle;\n %f and %f -- in the end  \n value: ", a, a + step, knots(1 + floor((degOfPoly + 1) / 2)), knots(knotNumber - floor((degOfPoly + 1)/ 2)) , b - step, b);
  x = input("");
  finDiff = finiteDiff(valuesInKnots, degOfPoly);
  factorial_mult = cumprod(1:degOfPoly)';

  # select interpolation method (by the beginning, by the ending or by the middle of the table
  if (knots(1) <= x && x <= knots(2))
    puts "Interpolating by the beginning of the table\n";
    # Build the table of finite differences
    # Actually gives error in 2-nd or 3-rd sign in some cases
    mean = (x - knots(1)) / step;
    T = zeros(degOfPoly, 1);
    for i = 0:degOfPoly-1
      T(i + 1) = mean - i;
    end
    puts "Predicted value: ";
    predicted = valuesInKnots(1) + finDiff(1, :) * (cumprod(T) ./ factorial_mult);
    disp(predicted);
    printf("Difference value: %3.16f\n", abs(f(x) - predicted));

  elseif (knots(knotNumber - 1) <= x && x <= knots(knotNumber))
    puts "Interpolating by the ending of the table\n";
    mean = (x - knots(end)) / step;
    T = zeros(degOfPoly, 1);
    for i = 0:degOfPoly-1
      T(i + 1) = mean + i;
    end
    puts "Predicted value: ";
    finDiffSecondDiag = diag(rotdim(finDiff, 3))';
    predicted = valuesInKnots(end) + finDiffSecondDiag * (cumprod(T) ./ factorial_mult);
    disp(predicted);
    printf("Difference value: %3.16f\n", abs(f(x) - predicted));

  elseif(knots(1 + floor((degOfPoly + 1) / 2)) <= x && x <= knots(knotNumber - floor((degOfPoly + 1)/ 2)))
    puts "Interpolating by the middle of the table\n";
    indexOfNearestLess = find(knots == max(knots(knots <= x))) ;
    mean = (x - knots(indexOfNearestLess)) / step;
    importantDiff = zeros(1, degOfPoly);
    T = zeros(degOfPoly, 1);
    for i = 0:degOfPoly-1
      importantDiff(i + 1) = finDiff(indexOfNearestLess - floor((i + 1) / 2), i + 1);
      T(i + 1) = mean + (-1) ^ i * floor((i + 1) / 2);
    end
    predicted = valuesInKnots(indexOfNearestLess) + importantDiff * (cumprod(T) ./ factorial_mult);
    puts "Predicted value: ";
    disp(predicted);
    printf("Difference value: %3.16f\n", abs(f(x) - predicted));
  else
    puts "Can't interpolate function at this point and/or with this degree of polynomial\n";
    
  end
endwhile
format short
