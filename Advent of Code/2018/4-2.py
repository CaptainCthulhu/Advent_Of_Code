from datetime import datetime
from storage41 import input

def GetTimeLine():

    timeDict = {}
    for i in input.keys():
        timeDict[datetime.strptime(i, '%Y-%m-%d %H:%M')] = input[i]

    sortedTimes = []
    sortedTimes = sorted(timeDict)

    sortedDict = {}
    for i in sortedTimes:
        sortedDict[i] = timeDict[i]

    return sortedDict

def GetGuardSleepTotals(timeLine):  
    Guards = {}

    currentGuard = ''     
    previousTimestamp = datetime.strptime('0001-01-01 00:00', '%Y-%m-%d %H:%M')

    for i in timeLine:        
        if "Guard" in timeLine[i]:
            guardNum = timeLine[i].split(' ')[1].replace("#", "").strip()
            if Guards.get(guardNum) is None:
                Guards[guardNum] = [0 for x in range(60)]
                currentGuard = guardNum
            else:
                currentGuard = guardNum            
        elif "wakes" in timeLine[i]:           
            for i in range(previousTimestamp.minute, i.minute):
                Guards[guardNum][i] += 1
        previousTimestamp = i
        
    return Guards


timeLine = GetTimeLine()
guardSleep = GetGuardSleepTotals(timeLine)

winningGuard = 0
sleepTime = 0

for i in guardSleep:
    if max(guardSleep[i]) > sleepTime:
        sleepTime = max(guardSleep[i])
        winningGuard = i

print(int(winningGuard) * guardSleep[winningGuard].index(sleepTime))

print("Narf!")