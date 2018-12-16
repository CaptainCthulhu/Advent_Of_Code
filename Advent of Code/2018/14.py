import math
array = [3,7]
index1 = 0
index2 = 1

goal = 323081
after = 10
score = ''

count = 0

while str(goal) not in ''.join(array):
    count += 1
    newNum = array[index1] + array[index2]
    score += str(newNum)

    if newNum > 9:
        array.append(int(math.floor(newNum / 10)))
    array.append(newNum % 10)
    index1 = (1 + index1 + array[index1]) % len(array)
    index2 = (1 + index2 + array[index2]) % len(array)
    
stringReponse = ''.join(array)

print(stringReponse[0:stringReponse.index(str(goal))])