"""Testy algorytmów"""

from sin_algorytm import *  # sin1, sin2, sin4, sin3, blad_w
from math import sin

def avg(val, n):
    return val / n

def test_blad(x, N):
    """Test ilości sumowanyc składników."""
    f1 = open('testyb_1.txt', 'w')
    f2 = open('testyb_2.txt', 'w')
    f3 = open('testyb_3.txt', 'w')
    f4 = open('testyb_4.txt', 'w')
    print(sin(x))
    for i in range(1, N):
        f1.write(str(blad_w(sin(x), sin1(x, i))) + "\n")
        f2.write(str(blad_w(sin(x), sin2(x, i))) + "\n")
        f3.write(str(blad_w(sin(x), sin3(x, i))) + "\n")
        f4.write(str(blad_w(sin(x), sin4(x, i))) + "\n")

def test_x(N):
    """Test wszystkich algorytmów dla miliona argumentów w zakresie [-Pi;Pi] dla N liczby składników """
    Pi = 3.141592653589793
    krok = 2 * Pi / 10 ** 6
    x = -1 * Pi
    i = 0
    suma1 = 0
    suma2 = 0
    suma3 = 0
    suma4 = 0
    f1 = open('t_sin1.txt', 'w')
    f2 = open('t_sin2.txt', 'w')
    f3 = open('t_sin3.txt', 'w')
    f4 = open('t_sin4.txt', 'w')

    print('Start calculating...')
    while (x < Pi):
        i += 1
        x = x + krok
        suma1 = suma1 + blad_w(sin(x), sin1(x, N))
        suma2 = suma2 + blad_w(sin(x), sin2(x, N))
        suma3 = suma3 + blad_w(sin(x), sin3(x, N))
        suma4 = suma4 + blad_w(sin(x), sin4(x, N))
        if (i % 20000 == 0):
            f1.write(str(avg(suma1, i)) + "\n")
            f2.write(str(avg(suma2, i)) + "\n")
            f3.write(str(avg(suma3, i)) + "\n")
            f4.write(str(avg(suma4, i)) + "\n")
            suma1 = 0
            suma2 = 0
            suma3 = 0
            suma4 = 0
            i = 0

    print('done')

#test_x(30)
#test_blad(2.5,12)


