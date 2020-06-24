import numpy as np
from time import sleep
from patterns import random, gun


def _next_cell_state(g, row, col):
    neighbors = g[row - 1 : row + 2, col - 1 : col + 2].copy()
    current_state = neighbors[1, 1]
    neighbors[1, 1] = 0
    live = np.count_nonzero(neighbors == 1)
    if current_state == 1 and live in (2, 3):
        return 1
    if current_state == 0 and live == 3:
        return 1
    return 0

def next_gen(g):
    """
    Proceed to the next generation. Takes a numpy array and returns a new one
    with each cell updated based on its neighbors.
    Parameters:
        g (np.array): the current state of life
    Returns:
        new_grid (np.array): the next iteration
    """
    new_grid = np.zeros(g.shape, dtype=int)
    for (i,j), _ in np.ndenumerate(g[1:-1, 1:-1]):
        if i in(0, g.shape[1] - 1) or j in (0, g.shape[0]-1):
            continue
        new_grid[i,j] = _next_cell_state(g, i, j)
    return new_grid


if __name__ == '__main__':
    init = random.seed(10)
    # init = gun.gun()
    g = init
    for _ in range(500):
        print(g)
        sleep(1)
        g = next_gen(g)
