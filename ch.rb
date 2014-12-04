filename = "palm1"
f = File.open(filename, "r")
o = File.open("#{filename}_m", "w")
f.each_line do |line|
  line.split(';').each do |str|
    unless str.chomp.empty?
      o.write "#{str}\n"
    end
  end
end
f.close
o.close
