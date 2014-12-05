function predicted = interpolate(tableOfValues, degOfPoly, points)
  # build the polynomial
  assert(size(tableOfValues, 1) > degOfPoly);
  assert(size(tableOfValues, 2) == 2);
  knots = tableOfValues(:, 1)';
  predicted = [];
  for x = points
    sortedTable = sortrows([abs(x - knots)', tableOfValues])(1:degOfPoly + 1, 2:end);
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
    polyN = ones(n + 1, 1);
    for i = 2:n+1
      polyN(i:end, 1) = polyN(i:end, 1) .* (x - sortedTable(i - 1, 1));
    endfor
    polyN = sepDiff(1, 2:end) * polyN;
    predicted = [predicted; polyN];
  end
end
