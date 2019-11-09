""" autor: Agnieszka Harłozińska
    Algorytmy Numeryczne
    Zadanie 1
    Sumowanie szeregów potęgowych.
    Data ostatniej modyfikacji: 14.10.2019
    """

def fac(n):
    """Zwraca silnię liczby n: n!"""
    factorial = 1
    for i in range(1, n+1):
        factorial = factorial * i
    return factorial

def pow(x, n):
    """Zwraca n-tą potęgę liczby x: x^n """
    power = 1
    for i in range(1, n+1):
        power = power * x
    return power

def abs(a):
    """Zwraca  wartość bezwględną liczby a: |a|"""
    return a if a >= 0 else -a

def blad_w(a, p):
    """Zwraca błąd względny przybliżenia p liczby a.
    Argumenty:
        a - wartość dokładna liczby
        p - przybliżenie liczby a
    """
    return abs(a-p)/abs(a)

def _get_taylor_arr(x, n):
    """Zwraca listę wyrazów szeregu Taylora wyznaczonych bezpośrednio ze wzoru Taylora."""
    lista = []

    for i in range(n):
        znak = pow(-1, i)
        lista.append(znak * pow(x, (2*i + 1)) / fac(2*i + 1))
    return lista

def sin1(x, n):
    """Zwraca  wartosc sin(x) jako sumę szeregu potęgowego z wzoru Taylora, sumując w  kolejności od początku."""
    mac = 0.0
    for i in range(n):
        znak = pow(-1, i)
        mac = mac + znak * pow(x, (2*i + 1)) / fac(2*i + 1)
    return mac

def sin2(x, n):
    """Zwraca wartosc sin(x) jako sumę szeregu potęgowego z wzoru Taylora, sumując w  kolejności od końca."""
    mac = 0.0
    arr = _get_taylor_arr(x, n)
    for i in range(len(arr)-1, -1, -1):
        mac = mac + arr[i]
    return mac

def _get_sum_arr(x, n):
    """Zwraca listę wyrazów szeregu Taylora wyznaczonych na podstawie poprzedniego wyrazu."""
    lista = []
    lista.append(x)
    for i in range(1, n+1):
        q = ((-1) * x * x) / ((2 * i) * (2 * i + 1))
        lista.append(lista[i-1] * q)
    return lista

def sin3(x, n):
    """Zwraca wartosc sin(x) jako sumę szeregu potęgowego Taylora, obliczajac kolejny wyraz na podstawie poprzedniego
     z sumowaniem elementów szeregu od początku."""
    arr = _get_sum_arr(x, n)
    suma = 0
    for i in range(len(arr)):
        suma += arr[i]
    return suma


def sin4(x, n):
    """Zwraca wartosc sin(x) jako sumę szeregu potęgowego Taylora, obliczajac kolejny wyraz na podstawie poprzedniego
    z sumowaniem elementów szeregu od końca."""
    arr = _get_sum_arr(x, n)
    suma = 0
    for i in range(len(arr)-1, -1, -1):
        suma += arr[i]
    return suma

