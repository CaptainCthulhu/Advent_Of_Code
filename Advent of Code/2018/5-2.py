import storage5
import operator
from string import ascii_lowercase

def findLength(var): 
    var.append('!')
    while ('!' in var):
     var = [x for x in var if x != '!']
     for i in range(len(var) - 1):
         if var[i] != '!' and var[i].lower() == var[i+1].lower() and var[i] != var[i+1]:
             var[i] = '!'
             var[i+1] = '!'

    return len(var)

def RemoveLetter(letter):
    var = storage5.var
    num = findLength([x for x in var if x != letter.lower() and x != letter.upper()])
    print("Done {}".format(letter))
    return num
    
        

answers = {'a':0}
for c in ascii_lowercase:
    answers[c] = RemoveLetter(c)


print(min(answers.values()))


