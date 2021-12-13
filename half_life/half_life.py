import seaborn as sns
import numpy as np

sns.set_theme(style="darkgrid")


def decay(x, c):
    return 2 ** (-x + c)


def cum_sum(n):
    return sum(decay(n, i) for i in range(1, n + 1))


if __name__ == "__main__":
    x = range(1, 11)
    y = [cum_sum(i) for i in x]
    sns.relplot(x=x, y=y)
