function points = genPoints(a, b, number)
  assert(number > 1);
  number -= 1;
  assert(a < b);
  points = 0:number;
  points = a + points * (b - a) / number;
endfunction
  
