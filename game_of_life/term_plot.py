import numpy as np
from patterns.gun import gun
from patterns.random import seed
from time import sleep
from game import next_gen

black_sq = "■"
white_sq = "□"


def term_plot(g):
    for (i, j), cell in np.ndenumerate(g[1:-1, 1:-1]):
        if cell == 0:
            print(black_sq, end="", flush=True)
        else:
            print(white_sq, end="", flush=True)
        if j == g.shape[1] - 3:
            print("")
            if i == g.shape[0] - 3:
                print("\n")


if __name__ == "__main__":
    init = gun()
    g = init
    for _ in range(500):
        term_plot(g)
        sleep(0.5)
        g = next_gen(g)
