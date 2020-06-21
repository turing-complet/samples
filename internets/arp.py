from scapy.layers.l2 import Ether, ARP, srp
import scapy.packet

ans, unans = srp(Ether(dst="ff:ff:ff:ff:ff:ff") / ARP(pdst="192.168.1.0/24"), timeout=2)
ans.summary(lambda sr: sr[1].sprintf("%Ether.src% %ARP.psrc%"))
