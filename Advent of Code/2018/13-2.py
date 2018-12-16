import datetime
grid = []
cars = []
filename = "storage13.txt"
output = True

def Log(message):
    output = "{} : {}".format(datetime.datetime.now(), message)
    #print(output)
    if output:
        with open("./13/output13.txt", 'a') as f:
            f.write(output)


class Car():
    switchOrder = ('left', 'straight', 'right')
    switchCount = 0

    def __init__(self, name, symbol, x, y):
        self.name = name
        self.x = x
        self.y = y
        if symbol == '^':
            self.direction = 'up'
        elif symbol == 'v':
            self.direction = 'down'
        elif symbol == '>':
            self.direction = 'right'
        elif symbol == '<':
            self.direction = 'left'


    def move(self, turn):
        if self.direction == 'up':
            self.y -= 1
        elif self.direction == 'down':
            self.y += 1
        elif self.direction == 'left':
            self.x -= 1
        elif self.direction == 'right':
            self.x += 1

        track = grid[self.y][self.x]
        if track == ' ':
            Log("Car {} Turn: {} {},{} HEADER {} OFF TRACKS!".format(self.name, turn, self.x, self.y, self.direction))
        elif track == '+':
            Log("Car {} Headed {} found junction. Should turn {}.".format(self.name, self.direction, self.switchOrder[self.switchCount % len(self.switchOrder)]))            
            if self.direction == 'up':
                if self.switchOrder[self.switchCount % len(self.switchOrder)] == 'left':
                    self.direction = 'left'
                elif self.switchOrder[self.switchCount % len(self.switchOrder)] == 'right':
                    self.direction = 'right'
            elif self.direction == 'down':
                if self.switchOrder[self.switchCount % len(self.switchOrder)] == 'left':
                    self.direction = 'right'
                elif self.switchOrder[self.switchCount % len(self.switchOrder)] == 'right':
                    self.direction = 'left'
            elif self.direction == 'left':
                if self.switchOrder[self.switchCount % len(self.switchOrder)] == 'left':
                    self.direction = 'down'
                elif self.switchOrder[self.switchCount % len(self.switchOrder)] == 'right':
                    self.direction = 'up'
            elif self.direction == 'right':
                if self.switchOrder[self.switchCount % len(self.switchOrder)] == 'left':
                    self.direction = 'up'
                elif self.switchOrder[self.switchCount % len(self.switchOrder)] == 'right':
                    self.direction = 'down'
            self.switchCount += 1
        elif track == '\\':
            Log("Car {} Headed {} found {}.".format(self.name, self.direction, track))
            if self.direction == 'up':
                self.direction = 'left'
            elif self.direction == 'down':
                self.direction = 'right'
            elif self.direction =='left':
                self.direction = 'up'
            elif self.direction == 'right':
                self.direction = 'down'
        elif track == '/':
            Log("Car {} Headed {} found {}.".format(self.name, self.direction, track))
            if self.direction == 'up':
                self.direction = 'right'
            elif self.direction == 'down':
                self.direction = 'left'
            elif self.direction == 'left':
                self.direction = 'down'
            elif self.direction == 'right':
                self.direction = 'up'
        Log("Car {} New heading {}".format(self.name, self.direction))

def CheckCollision(turn, car):
    global cars
    otherCars = [x for x in cars if x.name != car.name and x.x == car.x and x.y == car.y]
    if otherCars:
        cars.remove(car)
        print("Turn {}. Car {} collided at {},{}".format(turn, car.name, car.x, car.y))
    for carx in otherCars:
        cars.remove(carx)
        print("Turn {}. Removing {} as well.".format(turn, carx.name))
       


def CreateCar(symbol, x, y):
    global cars
    cars.append(Car(len(cars)+1,symbol, x, y))
    Log("Placing car at {},{}. Headed {}".format(x, y, symbol))
    if symbol in ('<', '>'):
        return '-'
    elif symbol in ('v', '^'):
        return '|'

def SortCars():
    global cars
    cars = sorted(sorted(cars, key=lambda x: x.x), key=lambda x: x.y)

def WritePage(turn):
    return
    test = [[' ' for x in range(len(grid[y]))] for y in range(len(grid))]
    for y in range(len(grid)):
        for x in range(len(grid[y])):
            test[y][x] = grid[y][x]

    for car in cars:
        symbol = ''
        if car.direction == 'up':
            symbol = '^'
        elif car.direction == 'down':
            symbol = 'v'
        elif car.direction == 'left':
            symbol = '<'
        elif car.direction == 'right':
            symbol = '>'

        test[car.y][car.x] = symbol

    with open('.\\13\\{}.txt'.format(turn + 1000000000), 'w+') as f:
        for y in range(len(test)):
            f.write(''.join(test[y]))

def GetGrid():
    global grid
    with open(filename) as f:
        for line in f:
            grid.append(list(line))

    for y in range(len(grid)):
        for x in range(len(grid[y])):
            symbol = grid[y][x]
            if symbol in ('>','<','^','v'):
                grid[y][x] = CreateCar(symbol, x, y)

    SortCars()

def Run():
    count = 0
    GetGrid()    
    WritePage(count)
    while len(cars) > 1:        
        count += 1
        Log("Turn {}".format(count))
        SortCars()
        for car in cars:
            car.move(count)
            CheckCollision(count, car)      
            SortCars()
        WritePage(count)

Run()
print("Last car:  {},{}".format(cars[0].x, cars[0].y))