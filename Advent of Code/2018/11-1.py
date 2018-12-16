serial_number = 9798

size = 300
checkSize = 3

def GetValue(x, y):
    ans = x + 10
    ans *= y
    ans += serial_number
    ans *= x + 10
    ans = int(str(ans)[-3] if ans >= 100 else 0)
    return ans - 5

def getSum(x, y):
    total = 0
    for ysub in range(y, y+checkSize):
        for xsub in range(x, x+checkSize):
            total += grid[ysub][xsub]
    return total

grid = [[GetValue(x+1, y+1) for x in range(size)] for y in range(size)]

coordinates = (0,0)
answer = -10000000
goodCheckSize = 0
for i in range(size): 
    checkSize = i
    print("Checking grids with size {}. Current winner: {},{},{} {}".format(i, coordinates[0], coordinates[1], goodCheckSize, answer))
    for y in range(len(grid) - checkSize -1):
        for x in range(len(grid[y])-checkSize -1):
            temp = getSum(x,y)
            if temp > answer:
                answer = temp
                coordinates = (x+1,y+1)
                goodCheckSize = checkSize

print("{},{}:{}".format(coordinates, goodCheckSize, answer ))     