from storage10 import var as vars
import os

threshhold = 10
defaultChar = "."
indicator = "X"

def Update():
    for i in vars:
        velocity = i[1]
        position = i[0]
        position[0] += velocity[0]
        position[1] += velocity[1]

def Organize():
    positions = [x[0] for x in vars]
    minX = min([x[0] for x in positions])
    maxX = max([x[0] for x in positions]) +1 
    minY = min([x[1] for x in positions])
    maxY = max([x[1] for x in positions])   + 1 

    if abs(maxX - minX) > 150 or abs(maxY - minY) > 150:
        return None

    xOffset = minX
    yOffset = minY

    Organized = [[defaultChar for x in range(maxX - xOffset + 1)] for y in range(maxY - yOffset + 1)] 

    for y in range(minY, maxY):
        for x in range(minX, maxX):
            if [x,y] in positions:
                Organized[y-yOffset][x-xOffset] = indicator

    return Organized

def Search(organized):
    yCount, xCount, sideWaysPlus, sideWaysNeg  = 0,0,0,0
    
    for y in range(len(organized)):
        for x in  range(len(organized)):
            if organized[y][x] == indicator:
                #y 
                yCount = sum([1 for i in range(y, min(y+threshhold, len(organized)))
                    if organized[i][x] == indicator])

                #x
                xCount = sum([1 for i in range(x, min(x+threshhold, len(organized[y]))) 
                    if organized[y][i] == indicator])
                #sideways+
                #sidways-
                if yCount >= threshhold or xCount >= threshhold:
                    return True
    return False


def PrintFile(organized, runNum):
    with open(".\\10\\Output{}.txt".format(runNum), "w+") as f:
        for y in range(len(organized)):           
            f.write(str(''.join(organized[y])) + '\n')

def Draw(organized):
    os.system('cls')   
    for y in range(len(organized)):           
            print(str(''.join(organized[y])))

def Go():
    runNum = 0
    while True:
        runNum += 1
        Update()
        organized = Organize() 
        if organized is not None:            
            #Draw(organized)     
            print("Searching")
            if Search(organized):        
                Draw(organized)  
                input("Waiting...")      
        
            
            


Go()
    