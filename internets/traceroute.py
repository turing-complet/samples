from scapy.all import *
from scapy.layers.inet import IP
import sys
import socket

DEST = ''

def probe(max_hops):
    pkt = IP(ttl=max_hops, dst=DEST)
    resp = sr(pkt)
    resp[0].show()


if __name__ == '__main__':
    host = sys.argv[1]
    addr =  [str(i[4][0]) for i in socket.getaddrinfo(host, 80)][0]

    DEST = addr
    print(f'Traceroute to {host} ({addr})')
    
    for i in range(12):
        probe(i)
