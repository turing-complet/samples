import matplotlib.pyplot as plt
from game import seed, next_gen

plt.ion()
fig = plt.figure()


def game_of_life(size):
    g = seed(size)

    im = plt.imshow(g)
    while True:
        g = next_gen(g)
        im.set_data(g)
        fig.canvas.draw()


if __name__ == "__main__":
    import sys

    grid_size = int(sys.argv[1])
    game_of_life(int(grid_size))
