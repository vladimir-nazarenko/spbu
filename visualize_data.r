#Check Session -> Set Working Directory -> To Source File Location
results <- read.csv("Comparasion_results_with_unnoised.csv")
noiseRates = 0:19
noiseRates <- noiseRates / 20
pdf("graphics/Perf_unnoised.pdf", 8, 5)
plot(noiseRates,results$NaiveBayes, ann=FALSE, xlim=range(c(0.0, 1.0)), ylim=range(c(0, 110)), type='o', lwd=3, col="red")
lines(noiseRates, results$SMO, lwd=3, col="green", type='o')
lines(noiseRates, results$RandomTree, lwd=3, col="blue", type='o')
legend(0.75, 100, c("Naive Bayes", "Random Tree", "SMO"), cex=0.8, col=c("red", "blue", "green"), pch=21, lty=1, lwd=3);
title(xlab="Noise Rate", ylab="Percent Correct", main="Algorithms Perfomance")
dev.off()

results <- read.csv("Comparasion_results.csv")
pdf("graphics/Perf_noised.pdf", 8, 5)
plot(noiseRates,results$NaiveBayes, ann=FALSE, xlim=range(c(0.0, 1.0)), ylim=range(c(0, 110)), type='o', lwd=3, col="red")
lines(noiseRates, results$SMO, lwd=3, col="green", type='o')
lines(noiseRates, results$RandomTree, lwd=3, col="blue", type='o')
legend(0.75, 100, c("Naive Bayes", "Random Tree", "SMO"), cex=0.8, col=c("red", "blue", "green"), pch=21, lty=1, lwd=3);
title(xlab="Noise Rate", ylab="Percent Correct", main="Algorithms Perfomance")
dev.off()