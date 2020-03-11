from matplotlib import pyplot as plt
import numpy as np

plt.xkcd()

fig = plt.figure()
ax = fig.add_subplot()

drugs = {
    "weed" : (20, 20),
    "nicotine" : (15, 30),
    "alcohol" : (50, 65),
    "heroin" : (95, 95),
    "cocaine": (70, 80),
    "lsd" : (80, 35)
}

x = [ coord[0] for coord in drugs.values() ]
y = [ coord[1] for coord in drugs.values() ]

plt.scatter(x, y)
plt.axis([0, 100, 0, 100])

for i, txt in enumerate(drugs.keys()):
    ax.annotate(txt, (x[i]-5, y[i]-5))

xline = range(10, 90)
yline = [p * .9 + 5 for p in xline]
plt.plot(xline, yline)

plt.xlabel('how good it is')
plt.ylabel('danger')

plt.title("DRUG TRADEOFFS")

plt.show()