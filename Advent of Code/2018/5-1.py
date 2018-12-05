import storage5

var = storage5.var
var = var + '!'
print(len(var))

while ('!' in var):
 var = [x for x in var if x != '!']
 for i in range(len(var) - 1):
     if var[i] != '!' and var[i].lower() == var[i+1].lower() and var[i] != var[i+1]:
         var[i] = '!'
         var[i+1] = '!'

print(len(var))

