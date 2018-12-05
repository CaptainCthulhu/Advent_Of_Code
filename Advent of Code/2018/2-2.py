import sys
from storage import Question2 

var = Question2.Part1

def OffByOne(word1, word2):    
    items = ''
    goal = len(word1) - 1
    for x in range(len(word1)):
        if word1[x] == word2[x]:
            items += word1[x]
    if len(items) == goal:
        return items
    return False

for i in range(len(var) - 1):
    for x in range(i, len(var)):
        result = OffByOne(var[i], var[x])
        if result:
            print(result)
            break



    