import math
array = [3,7]
index1 = 0
index2 = 1

goal = "323081"
after = 10
score = ''

count = 0
found = False

def is_slice_in_list(s):
    full = ''.join([str(x) for x in s])    
    return goal in full

while not found:    
    count += 1
    newNum = array[index1] + array[index2]
    score += str(newNum)
    if newNum > 9:
        array.append(int(math.floor(newNum / 10)))
    array.append(newNum % 10)
    index1 = (1 + index1 + array[index1]) % len(array)
    index2 = (1 + index2 + array[index2]) % len(array)
    if count % 10000000 == 0:
        print("{:,} runs done.".format(count))
        found = is_slice_in_list(array)
        if not found:
            print("Not found.")


    
stringReponse = ''.join([str(x) for x in array])

print(len(stringReponse[0:stringReponse.index(goal)]))