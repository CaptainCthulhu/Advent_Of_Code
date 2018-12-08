from storage81 import input as input 

input_list = input.split(" ")

total_metadata = 0  


def grabMetadata(index, metadata):
    global total_metadata
    for i in range(index, index+metadata):
        total_metadata += int(input_list[i])
    return index+metadata

def getNode(index):    
    childNodes = int(input_list[index])
    metadata = int(input_list[index + 1])   
    #go into the child nodes
    index += 2
    for _ in range(childNodes):
        index = getNode(index)    
    return grabMetadata(index, metadata)
    
index = 0
while index < len(input_list):
    index = getNode(index)

print(total_metadata)



