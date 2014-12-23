function diff = finiteDiff(nodes, order)
  assert(min(size(nodes)) == 1);
  if (size(nodes, 1) < size(nodes, 2))
     nodes = nodes';
  end
  assert(size(nodes, 1) > order, "Insufficient nodes to get the finite difference of given order");
  assert(order > 0);
  knotNumber = size(nodes);
  finDiff = zeros(knotNumber, order + 1);
  finDiff(:, 1) = nodes;
  for col = 2:order+1
    finDiff(1:knotNumber - col + 1, col) = finDiff(2:knotNumber - col + 2, col - 1) - finDiff(1:knotNumber - col + 1, col - 1);
  end
  diff = finDiff(1:end-1, 2:end);
end
