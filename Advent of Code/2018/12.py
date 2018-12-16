from variables12 import rules
from variables12 import input  
import time, datetime
goal = 10000000

dictionaryAnswers= {}

def iterate(currentdict):
    global dictionaryAnswers

    if dictionaryAnswers.get(frozenset(currentdict.keys())) is not None:
        print("Found repeat")
        return dictionaryAnswers.get(frozenset(currentdict.keys()))

    newdict = {}
    for i in range(min(currentdict.keys()) - 2, max(currentdict.keys()) + 3):
        tempString = []
        for x in range(i - 2, i+ 3):
            if currentdict.get(x) is not None:
                tempString.append('#')
            else:
               tempString.append('.')        
        if rules.get(''.join(tempString)):
            newdict[i] = '#'
    
    dictionaryAnswers[frozenset(currentdict.keys())] = newdict

    return newdict

def initialState():
    currentdict = {}
    for i in range(len(input)):
        if input[i] == '#':
            currentdict[i] = '#'    
    return currentdict

def run():
    currentdict = initialState()
    turn = 0
    while turn < goal:        
        turn += 1
        if turn % 1000 == 0: 
            with open("out12.txt", 'a') as f:
                f.write("{}: {}\n".format(turn, sum(currentdict.keys())))
        currentdict = iterate(currentdict)    
    return currentdict 

dictionary = run()
print(sum(dictionary.keys()))
