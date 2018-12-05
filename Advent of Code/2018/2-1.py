import sys
from storage import Question2

var = Question2.Part1

two = 0
three = 0

for i in var:
    letters = {}
    for x in i:        
        letters[x] = (letters.get(x) or 0) + 1
    
    if len([x for x in letters.values() if x == 3]) > 0:
        three += 1
    if len([x for x in letters.values() if x == 2]) > 0:
        two += 1

print (two * three)



