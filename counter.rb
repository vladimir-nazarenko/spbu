require 'matrix'

f = File.open("palm_test", 'r')
o = File.open("normpalm_test", 'w')
text = f.read
lines = text.each_line
puts "Lines count: #{lines.count}"
old_count = lines.count
counts = lines.map{|line| line.split(';').count}
# conts.each{|c| puts c}
sum = 0
counts.each{|c| sum += c}
avg = sum.to_f / counts.count.to_f
puts "avg: #{avg.to_i}"
lines = lines.select{|l| l.split(';').count >= avg.to_i - 2}
puts "Throwed #{old_count - lines.count} lines"
pts = lines.map{|l|
  l.split(';').select{|s| not s.chomp.empty?}.map{|s|
      s.split(',').map{|s| s.to_f}
  }
}
def calcDist(x1y1, x2y2)
  (x1y1[0] - x2y2[0]) ** 2 + (x1y1[1] - x2y2[1]) ** 2
end


def findNearestIndexes(arr)
  minDist = 100000
  i1 = nil
  i2 = nil
  arr.each_with_index{|pair1, ind1|
    arr.each_with_index{|pair2, ind2|
      unless ind1 >= ind2
        dist = calcDist(pair1, pair2)
        if dist < minDist
          minDist = dist
          i1 = ind1
          i2 = ind2
        end
      end     
    }
  }
  [i1, i2]
end
# function modifies arr!
def aggregateToLength(len, arr)
  while (arr.size > len)
    nearest = findNearestIndexes(arr)
    i1 = nearest[0]
    i2 = nearest[1]
    mean = [(arr[i1][0] + arr[i2][0]).to_f / 2.to_f, (arr[i1][1] + arr[i2][1]).to_f / 2.to_f]
    mean = arr[i1]
    arr.delete_if.with_index{|_, ind| nearest.include? ind}
    arr.push(mean)
  end
  arr
end
pts.map{|vector| aggregateToLength(avg.to_i - 3, vector)}
# p pts
# pts.each{|c| puts c.size}
# pts.each{|ln| ln.each{|pt| o.write("#{pt[0]},#{pt[1]}\n")}}
names = (0...10).to_a.repeated_permutation(2).to_a.map{|pair| pair.join("_")}.join(',')
o.write("#{names},label\n")
centers = Matrix.build(10, 10){|row, col| [col * 0.1 + 0.05, row * 0.1 + 0.05]}
features = pts.map{|pvector|
  featureVector = Array.new(100)
  pvector.each{|pt|
    ceil = [(pt[0] * 10).floor, (pt[1] * 10).floor]
    ceil = ceil.map{|c| if c == 10 then 9 else c end}
    featureVector[ceil[1] * 10 + ceil[0]] = calcDist(pt, centers[ceil[0], ceil[1]])
  }
  featureVector.map{|val| if val.nil? then 100 else val end}  
}
features.each{|ln| o.write("#{ln.join(',')},1\n")}
f.close
o.close
