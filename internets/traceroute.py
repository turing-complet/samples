from scapy.all import *
from scapy.layers.inet import IP, ICMP
import sys
import socket

DEST = '216.58.217.46'

def probe(max_hops):
    pkt = IP(ttl=max_hops, dst=DEST)/ICMP()
    resp = sr(pkt, verbose=0)
    resp[0].show()
    return resp


if __name__ == '__main__':
    host = sys.argv[1]
    addr =  [str(i[4][0]) for i in socket.getaddrinfo(host, 80)][0]

    DEST = addr
    print(f'Traceroute to {host} ({addr})')
    
    for i in range(10):
        probe(i)
