import sys
from storage import Question3

var = Question3.Part1

coveredarray = [[0 for x in range(1500)] for y in range(1500)] 

goodlist = set(var.keys())

for i in var:
    claim = i
    value = var[i]
    startposition = value[0]
    iterator = value[1]
    for x in range(iterator[0]):        
        for y in range(iterator[1]):
            if coveredarray[startposition[0]+x][startposition[1]+y] != 0:
                if claim in goodlist:                
                    goodlist.remove(claim)
                if coveredarray[startposition[0]+x][startposition[1]+y] in  goodlist:                    
                    goodlist.remove(coveredarray[startposition[0]+x][startposition[1]+y])
            else:
                coveredarray[startposition[0]+x][startposition[1]+y] = claim    

print(','.join(goodlist))
    




