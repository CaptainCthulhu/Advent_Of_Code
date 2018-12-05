import sys
from storage import Question3

var = Question3.Part1

coveredarray = [[0 for x in range(1500)] for y in range(1500)] 

for i in var:
    value = var[i]
    startposition = value[0]
    iterator = value[1]
    for x in range(iterator[0]):        
        for y in range(iterator[1]):
            coveredarray[startposition[0]+x][startposition[1]+y] += 1   

total = 0

for x in range(len(coveredarray)):
    for y in range(len(coveredarray[x])):
        if coveredarray[x][y] > 1:
            total += 1

print(total)
    




