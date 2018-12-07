from storage6 import var2 as var

xMax = max([x[0] for x in var])
xMin = min([x[0] for x in var])
yMax = max([x[1] for x in var])
yMin = min([x[1] for x in var])

gameArea = [[0 for x in range(xMax +1)] for y in range(yMax + 1)]


def FindKey(dictionary, val):
    answer = None
    for key, value in dictionary.items():
        if val == value:
            if answer is not None:
                return False
            else:
                answer = key
    return answer


def CalculateDistance(coordinate1, coordinate2):
    return abs(coordinate1[0] - coordinate2[0]) + abs(coordinate1[1] - coordinate2[1])


def FindClosest(coordinate):
    answers = {}

    for i in range(len(var)):
        value = CalculateDistance(coordinate, var[i])        
        answers[i + 1] = value
    
    answer = FindKey(answers, min(answers.values()))
    if answer:
        return answer


def FillGrid():
    global gameArea
    for y in range(yMin, yMax + 1):
        for x in range(xMin, xMax + 1):
            gameArea[y][x] = FindClosest((x,y)) or 0
    print("Done filling the grid.")            


def PrintGameArea():
    with open("Solution6.txt", "w+") as f:
        for y in range(len(gameArea)):
            f.write(' '.join([str(x) for x in gameArea[y]]) + '\r\n')
    print("Done printing the grid.")

def GetNums():
    nums = {}
    for y in range(len(gameArea)):
        for x in gameArea[y]:
            if x != 0:
                nums[x] = (nums.get(x) or 0) + 1

    return nums    

def GetMaxArea():
    nums = GetNums()
    print(max(nums.values()))

FillGrid()
PrintGameArea()
GetMaxArea()

print("Done.")
