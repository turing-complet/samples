from functools import partial

# Y = λf.(λx.f(x x))(λx.f(x x))

def Y(f):
    def y(g):
        return lambda n: f(g(g))(n)
    return y(y)

Y = lambda f: ( lambda g: lambda n: f(g(g))(n) )( lambda g: lambda n: f(g(g))(n) )

#####################

Y_alt = lambda f: (lambda g: g(g)) (lambda y: f(lambda z: y(y)(z)))

Y_alt = lambda f: (lambda y: f(lambda z: y(y)(z))) (lambda y: f(lambda z: y(y)(z)))
Y_alt = lambda f: f(lambda z: (lambda y: f(lambda z: y(y)(z))) (lambda y: f(lambda z: y(y)(z))) (z))
Y_alt = lambda f: f(
    (lambda y: f(lambda z: y(y)(z))) (lambda y: f(lambda z: y(y)(z)))
    )

# this works, but is it a valid reduction?
Y_alt = lambda f: f(
    (lambda y: lambda z: f(y(y))(z) ) (lambda y: lambda z: f(y(y))(z))
    )

Y_alt = lambda f: f(Y)


fac = lambda f: lambda n: (1 if n<2 else n*f(n-1))
[ Y(fac)(i) for i in range(10) ]

#####################
# evaluation example

y = lambda g: lambda n: fac(g(g))(n)
# NOTE: Y(fac) = y(y)

y(y)
(lambda g: lambda n: fac(g(g))(n)) (lambda g: lambda n: fac(g(g))(n) )

lambda n: fac(y(y)) (n) 
lambda n: fac( (lambda g: lambda n: fac(g(g))(n)) (lambda g: lambda n: fac(g(g))(n) )) (n) 

Y(fac)(5)

# pass in 5 for n
fac(y(y)) (5)
fac( (lambda g: lambda n: fac(g(g))(n)) (lambda g: lambda n: fac(g(g))(n) )) (5)

# definition of fac, with f supplied
(lambda n: (1 if n<2 else n* y(y)(n-1))) (5) 
(lambda n: (1 if n<2 else n* ( (lambda g: lambda n: fac(g(g))(n)) (lambda g: lambda n: fac(g(g))(n)) ) (n-1))) (5) 

# fac logic
5* y(y)(4)
5* ( (lambda g: lambda n: fac(g(g))(n)) (lambda g: lambda n: fac(g(g))(n)) ) (4)

# bind g and expand defn
5* ( lambda n: fac(y(y))(n)) (4)
5* ( lambda n: fac( (lambda g: lambda n: fac(g(g))(n))(lambda g: lambda n: fac(g(g))(n)) )(n)) (4)

# pass in 4 for n
5* fac(y(y))(4)
5* ( fac( (lambda g: lambda n: fac(g(g))(n))(lambda g: lambda n: fac(g(g))(n)) )(4) )

# defn of fac
5* (lambda f: lambda n: (1 if n<2 else n*f(n-1)))( (lambda g: lambda n: fac(g(g))(n))(lambda g: lambda n: fac(g(g))(n)) )(4)
5* (lambda f: lambda n: (1 if n<2 else n*f(n-1))) (y(y))(4)

# getting lazy.. binding y(y) to f
5* (lambda n: (1 if n<2 else n*y(y)(n-1)))(4)

# bind 4 to n
5*4*y(y)(3)

# and so on..
# induction -> n * y(y)(n-1) = n*(n-1)*y(y)(n-2) for n >= 2

'''
Since y(y) = Y(fac), y(y) is a fixed point of fac. In other words, fac(y(y)) = y(y).
Once we have 5* y(y)(4) = 5* fac(y(y))(4)

'''

#####################
# substituting fac for g

(lambda g: lambda n: fac(g(g))(n) )
(lambda g: lambda n: (lambda f: lambda n: (1 if n<2 else n*f(n-1))) (g(g))) (n)

#####################

def g(h):
    def func(x):
        return 2 * h(x)
    return func

h = lambda x: x + 3

# should have f1 = f2
f1 = Y(g)
f2 = g(z)

s = compile('h(1)', '<string>', 'eval')
eval(s)

##########################################

def anon(improver):
    def fun(n):
        return 1 if n == 0 else n * improver(improver)(n-1)
    return fun

fx = anon(anon)

if __name__ == "__main__":
    import doctest
    doctest.testmod()