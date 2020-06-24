import edu.princeton.cs.algs4.StdOut;
import edu.princeton.cs.algs4.StdRandom;
import edu.princeton.cs.algs4.StdStats;

public class PercolationStats {
    private final int trials;
    private final double[] results;
    private final double meanValue;
    private final double confidence;

    // perform independent trials on an n-by-n grid
    public PercolationStats(int n, int trials) {
        if (n < 1) throw new IllegalArgumentException("Value of n is invalid.");
        if (trials < 1) throw new IllegalArgumentException("Value of trials is invalid.");

        results = new double[trials];
        this.trials = trials;
        int totalNumberOfElements = n * n;
        for (int i = 0; i < trials; i++) {
            double openCount = runTrial(n);
            results[i] = openCount / totalNumberOfElements;
        }
        meanValue = StdStats.mean(results);
        confidence = calculateConfidence();
    }

    // test client (see below)
    public static void main(String[] args) {
        int n = Integer.parseInt(args[0]);
        int trials = Integer.parseInt(args[1]);

        PercolationStats stats = new PercolationStats(n, trials);
        String confidence = String.format("[%s, %s]", stats.confidenceLo(), stats.confidenceHi());
        StdOut.println("mean                    = " + stats.mean());
        StdOut.println("stddev                  = " + stats.stddev());
        StdOut.println("95% confidence interval = " + confidence);
    }

    private double runTrial(int n) {
        Percolation percolator = new Percolation(n);
        while (!percolator.percolates()) {
            int row = StdRandom.uniform(n) + 1;
            int col = StdRandom.uniform(n) + 1;
            percolator.open(row, col);
        }
        return percolator.numberOfOpenSites();
    }

    // sample mean of percolation threshold
    public double mean() {
        return meanValue;
    }

    // sample standard deviation of percolation threshold
    public double stddev() {
        return StdStats.stddev(results);
    }

    // low endpoint of 95% confidence interval
    public double confidenceLo() {
        return mean() - confidence;
    }

    // high endpoint of 95% confidence interval
    public double confidenceHi() {
        return mean() + confidence;
    }

    private double calculateConfidence() {
        double sum = 0;
        double mean = mean();
        for (double result : results) {
            double notSquared = result - mean;
            double squared = notSquared * notSquared;
            sum += squared;
        }

        double sSquared = sum / (trials - 1);
        double s = Math.sqrt(sSquared);
        double sqrtTrials = Math.sqrt(trials);
        return (1.96 * s) / sqrtTrials;
    }
}