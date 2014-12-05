noiseRates <- 0:8
noiseRates<- noiseRates / 10
#plot perfomance degradation
perf <- read.csv("perfomance_table.csv")
perf <- perf[2:6]
tim <- read.csv("time_training.csv")
tim <- tim[2:6]
pdf("graphics/c_param.pdf",width = 11 ,height = 5,family= "URWHelvetica", encoding="CP1251") 
par(mfrow=c(1, 2))
boxplot(perf, names=c(1.0, 0.1, 0.01, 0.001, 0.0001), xlab="Value of the parameter", ylab="Percent of correctly classified")
boxplot(log(tim), names=c(1.0, 0.1, 0.01, 0.001, 0.0001), xlab="Value of the parameter", ylab="ln(training time(sec))")
dev.off()