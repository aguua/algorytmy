""" autor: Agnieszka Harłozińska
    Algorytmy Numeryczne
    Zadanie 1
    Sumowanie szeregów potęgowych.
    Data ostatniej modyfikacji: 08.10.2019
    """
from math import fabs

def fac(n):
    """Zwarca silnię liczby n: n!"""
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

def blad_w(a, p):
    """Zwraca błąd względny przybliżenia p liczby a.
    Argumenty:
        a - wartość dokładna liczby
        p - przybliżenie liczby a
    """
    return fabs(a-p)/a

def sin_mac_beg(x, n):
    """Zwraca  wartosc sin(x) jako sumę szeregu potęgowego z wzoru Taylora, sumując w  kolejności od początku."""
    mac = 0
    for i in range(1, n):
        s = pow(-1, i-1) / fac(2*i-1) * pow(x, (2*i-1))
        mac += s
    return mac

def sin_mac_end(x, n):
    """Zwraca wartosc sin(x) jako sumę szeregu potęgowego z wzoru Taylora, sumując w  kolejności od końca."""
    mac = 0
    for i in range(n, 0, -1):
        s = pow(-1, i-1) / fac(2*i-1) * pow(x, (2*i-1))
        mac += s
    return mac

def _get_taylor_arr(x, n):
    """Zwraca listę wyrazów szeregu Taylora wyznaczonych na podstawie poprzedniego wyrazu."""
    lista = []

    lista.append(x)
    for i in range(1, n+1):
        q = (-1) / ((2 * i) * (2 * i + 1)) * x * x
        lista.append(lista[i-1] * q)
    return lista

def sin_sum_beg(x, n):
    """Zwraca wartosc sin(x) jako sumę szeregu potęgowego Taylora, obliczajac kolejny wyraz na podstawie poprzedniego
     z sumowaniem elementów szeregu od początku."""
    arr = _get_taylor_arr(x, n)
    suma = 0
    for i in range(0, len(arr)-1):
        suma += arr[i]
    return suma


def sin_sum_end(x, n):
    """Zwraca wartosc sin(x) jako sumę szeregu potęgowego Taylora, obliczajac kolejny wyraz na podstawie poprzedniego
    z sumowaniem elementów szeregu od końca."""
    arr = _get_taylor_arr(x, n)
    suma = 0
    for i in range(len(arr)-1, -1, -1):
        suma += arr[i]
    return suma

