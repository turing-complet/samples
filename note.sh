#!/bin/bash
if [ $# -eq 0 ]
then
    name=''
else
    name=-$1
fi

cd ~/Dropbox/notes && vim `date +%Y-%m-%d`"$name"
