import numpy as np


def seed(n, p=0.5):
    """
    Start the game of life.

    Parameters:
        n (int): the size of the grid
        p (float): Optional - probability of 0
    Returns:
        np.array with a layer of padding, initialized randomly
    """
    frame = np.random.choice(a=[0, 1], size=(n, n), p=[p, 1 - p])
    return np.pad(frame, pad_width=1)
