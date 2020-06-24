import edu.princeton.cs.algs4.WeightedQuickUnionUF;

public class Percolation {

    private final int size;
    private final boolean[][] siteStates;
    private final int virtualTopIndex;
    private final int virtualBottomIndex;
    private final WeightedQuickUnionUF unionFind;
    private final WeightedQuickUnionUF unionFindForFull;
    private int openSiteCounter;

    // creates n-by-n grid, with all sites initially blocked
    public Percolation(int n) {
        if (n <= 0)
            throw new IllegalArgumentException("Value of n has to be positive");
        size = n;
        unionFind = new WeightedQuickUnionUF(size * size + 2);
        unionFindForFull = new WeightedQuickUnionUF(size * size + 2);
        siteStates = new boolean[size][];
        initializeSiteStates();

        // Virtual top element
        virtualTopIndex = size * size;
        initializeVirtualTop();

        // Virtual bottom element
        virtualBottomIndex = size * size + 1;
        initializeVirtualBottom();
    }

    // test client (optional)
    public static void main(String[] args) {
        System.out.println("Percolation test");
    }

    private void initializeSiteStates() {
        for (int i = 0; i < size; i++) {
            siteStates[i] = new boolean[size];
        }
    }

    private void initializeVirtualTop() {
        for (int i = 0; i < size; i++) {
            int index = calculateUnionFindIndex(0, i);
            unionFind.union(index, virtualTopIndex);
            unionFindForFull.union(index, virtualTopIndex);
        }
    }

    private void initializeVirtualBottom() {
        for (int i = 0; i < size; i++) {
            int index = calculateUnionFindIndex(size - 1, i);
            unionFind.union(index, virtualBottomIndex);
        }
    }

    // opens the site (row, col) if it is not open already
    public void open(int row, int col) {
        checkCoordinates(row, col);
        if (isOpenSkipCheck(row, col))
            return;

        siteStates[row - 1][col - 1] = true;
        openSiteCounter++;
        int index = calculateUnionFindIndex(row - 1, col - 1);

        unionIfOpen(row, col + 1, index);
        unionIfOpen(row, col - 1, index);
        unionIfOpen(row + 1, col, index);
        unionIfOpen(row - 1, col, index);
    }

    private void unionIfOpen(int row, int col, int index) {
        boolean isOpen = isOpenNonThrowing(row, col);
        if (isOpen) {
            int indexOther = calculateUnionFindIndex(row - 1, col - 1);
            unionFind.union(index, indexOther);
            unionFindForFull.union(index, indexOther);
        }
    }

    // is the site (row, col) open?
    public boolean isOpen(int row, int col) {
        checkCoordinates(row, col);
        return isOpenSkipCheck(row, col);
    }

    // is the site (row, col) full?
    public boolean isFull(int row, int col) {
        checkCoordinates(row, col);
        boolean isOpen = isOpen(row, col);
        if (!isOpen)
            return false;

        int index = calculateUnionFindIndex(row - 1, col - 1);
        return unionFindForFull.find(index) == unionFindForFull.find(virtualTopIndex);
    }

    // returns the number of open sites
    public int numberOfOpenSites() {
        return openSiteCounter;
    }

    // does the system percolate?
    public boolean percolates() {
        if (size == 1)
            return openSiteCounter == 1;

        return unionFind.find(virtualBottomIndex) == unionFind.find(virtualTopIndex);
    }

    private boolean isInvalidCoordinatePart(int x) {
        return x < 1 || x > size;
    }

    private int calculateUnionFindIndex(int rowZeroBased, int colZeroBased) {
        return rowZeroBased * size + colZeroBased;
    }

    private boolean isOpenSkipCheck(int row, int col) {
        return siteStates[row - 1][col - 1];
    }

    private boolean isOpenNonThrowing(int row, int col) {
        if (isInvalidCoordinatePart(row) || isInvalidCoordinatePart(col))
            return false;
        return isOpenSkipCheck(row, col);
    }

    private void checkCoordinates(int row, int col) {
        if (isInvalidCoordinatePart(row))
            throw new IllegalArgumentException("Value of row has to be between the interval [1;N].");
        if (isInvalidCoordinatePart(col))
            throw new IllegalArgumentException("Value of col has to be between the interval [1;N].");
    }
}