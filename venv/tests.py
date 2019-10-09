from algorytmy import sin_mac_beg, sin_mac_end, blad_w, sin_sum_end, sin_sum_beg
from math import sin

"""Testy algorytmów"""

def test_dla_x(x, N):
    for i in range (1,N):
        print ("n= {}".format(i))
        print("blad mac beg: {}"
          .format(blad_w(sin(x),sin_mac_beg(x,i)))
          )
        print("blad mac end : {}"
          .format(blad_w(sin(x),sin_mac_end(x,i)))
          )
        print("blad sum beg : {}"
          .format(blad_w(sin(x),sin_sum_beg(x,i)))
          )
        print("blad sum end : {}"
          .format(blad_w(sin(x),sin_sum_beg(x,i)))
          )

#for x in range (-1,1): #  od -1 do 1 z krokiem 0,1
test_dla_x(0.2, 13)
