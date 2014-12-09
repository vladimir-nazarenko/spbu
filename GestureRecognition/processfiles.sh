#!bin/bash
ruby csvMaker.rb h 1 1.out
ruby csvMaker.rb 2 2.out
ruby csvMaker.rb 3 3.out
ruby csvMaker.rb 4 4.out
ruby csvMaker.rb 1 1_l.out
ruby csvMaker.rb 2 2_l.out
ruby csvMaker.rb 3 3_l.out
ruby csvMaker.rb 4 4_l.out
touch trainSet.csv
> trainSet.csv
cat 1.out.csv >> trainSet.csv
rm 1.out.csv
cat 2.out.csv >> trainSet.csv
rm 2.out.csv
cat 3.out.csv >> trainSet.csv
rm 3.out.csv
cat 4.out.csv >> trainSet.csv
rm 4.out.csv
cat 1_l.out.csv >> trainSet.csv
rm 1_l.out.csv
cat 2_l.out.csv >> trainSet.csv
rm 2_l.out.csv
cat 3_l.out.csv >> trainSet.csv
rm 3_l.out.csv
cat 4_l.out.csv >> trainSet.csv
rm 4_l.out.csv