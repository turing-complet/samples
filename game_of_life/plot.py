import matplotlib.pyplot as plt
from matplotlib.animation import FuncAnimation
from game import next_gen
from patterns import random, gun


# g = random.seed(80)
g = gun.gun()


def generate(n):
    global g
    g = next_gen(g)
    return g


fig = plt.figure()
im = plt.imshow(generate(0))


def update(i):
    im.set_data(generate(i))


anim = FuncAnimation(fig, update)
plt.show()
