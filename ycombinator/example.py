"""
Various definitions of the y combinator:
Y = λf.(λx.f(x x))(λx.f(x x))

"""

fac = lambda f: lambda n: (1 if n<2 else n*f(n-1))
"""
Will use this for testing.
fac = lambda f: lambda n: (1 if n<2 else n*f(n-1))
"""

def Y1(f):
    """
    Easiest to read. 
    >>> Y1(fac)(5)
    120
    """
    def y(g):
        return lambda n: f(g(g))(n)
    return y(y)

Y2 = lambda f: ( lambda g: lambda n: f(g(g))(n) )( lambda g: lambda n: f(g(g))(n) )
"""
An equivalent definition:
>>> Y2(fac)(5)
120
"""

if __name__ == "__main__":
    import doctest
    doctest.testmod()