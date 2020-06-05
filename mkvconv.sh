#!/bin/bash

OIFS="$IFS"
IFS=$'\n'
for file in "$1"*.mkv
do
	echo $file
	mkvpropedit "${file}" --edit track:2 --set flag-default=0
	mkvpropedit "${file}" --edit track:3 --set flag-default=1
	mkvpropedit "${file}" --edit track:4 --set flag-default=0
	mkvpropedit "${file}" --edit track:5 --set flag-default=1
done
IFS="$OIFS"
