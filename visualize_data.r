#Check Session -> Set Working Directory -> To Source File Location
results <- read.csv("Comparasion_results.csv")
noiseRates = 0:9
noiseRates <- noiseRates / 20
plot(noiseRates,results$NaiveBayes, ann=FALSE, xlim=range(c(0.0, 0.45)), ylim=range(c(15, 100)), type='o', lwd=3, col="red")
lines(noiseRates, results$SMO, lwd=3, col="green", type='o')
lines(noiseRates, results$RandomTree, lwd=3, col="blue", type='o')
legend(0, 45, c("Naive Bayes", "Random Tree", "SMO"), cex=0.8, col=c("red", "blue", "green"), pch=21, lty=1, lwd=3);
title(xlab="Noise Rate", ylab="Percent Correct", main="Algorithms Perfomance")
