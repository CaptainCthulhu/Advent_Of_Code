from storage6 import var as var

gridMax = 500
maxDistance = 10000

gameArea = [[0 for x in range(gridMax)] for y in range(gridMax)]

def CalculateDistance(coordinate1, coordinate2):
    return abs(coordinate1[0] - coordinate2[0]) + abs(coordinate1[1] - coordinate2[1])


def FindSum(coordinate):
    sum = 0
    for i in var:
        if sum > maxDistance:
            return maxDistance
        value = CalculateDistance(coordinate, i) 
        if value > maxDistance:
            return maxDistance       
        sum += value    
    return sum


def CountItems():
    global gameArea
    count = 0
    for y in range(len(gameArea)):
        for x in range(len(gameArea[y])):
            if FindSum((x,y)) < maxDistance:
                #gameArea[y][x] = 1
                count += 1
        if y % 250 == 0:
            print("Done {} rows.".format(y))       
    print(count)            

def FindArea():
    xMax = 0
    xMin = gridMax
    yMax = 0
    yMin = gridMax
    for y in range(len(gameArea)):
        if 1 in gameArea[y]:
            if y < yMin:
                yMin = y
            if y > yMax:
                yMax = y
            for x in range(len(gameArea[y])):
                if gameArea[y][x] == 1:
                    if x < xMin:
                        xMin = x
                    if x > xMax:
                        xMax = x

    print ((xMax-xMin) * (yMax - yMin))



def PrintGameArea():
    with open("Solution6.txt", "w+") as f:
        for y in range(len(gameArea)):
            f.write(' '.join([str(x) for x in gameArea[y]]) + '\r\n')
    print("Done printing the grid.")    

CountItems()
#FindArea()
#PrintGameArea()

print("Done.")
