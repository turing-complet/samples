import numpy as np


def gun():
    grid = np.zeros((50, 50), dtype=int)
    grid[5:7, 1:3] = 1
    grid[5:8, 11] = 1

    grid[4, 12] = 1
    grid[8, 12] = 1

    grid[3, 13:15] = 1
    grid[9, 13:15] = 1

    grid[6, 15] = 1
    grid[4, 16] = 1
    grid[8, 16] = 1

    grid[5:8, 17] = 1
    grid[6, 18] = 1

    grid[3:6, 21:23] = 1
    grid[2, 23] = 1
    grid[6, 23] = 1
    grid[1:3, 25] = 1
    grid[6:8, 25] = 1

    grid[3:5, 35:37] = 1
    return grid

if __name__ == '__main__':
    g = gun()
    print(g[:20, 20:50])
