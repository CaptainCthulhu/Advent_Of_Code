import sys
from storage import Question1 

var = Question1.Part1
answers = {0}
found = False
total = 0

while not found:
    for i in var:
        total += i
        if total in answers:
            print(total)
            found = True
            break
        else:
            answers.add(total)



        
    