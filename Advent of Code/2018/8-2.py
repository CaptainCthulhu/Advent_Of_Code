from storage81 import input as input 
import uuid

input_list = input.split(" ")

nodes = []

name = 0
def GetName():
    global name
    name += 1
    return name

value = 0

total_metadata = 0

class node():
    metadataStart = 0
    def __init__(self, name, parent, index):
        self.name = name
        self.parent = parent
        self.startIndex = index
        self.childnodes = int(input_list[index])
        self.metadata = int(input_list[index + 1])

def grabMetadata(index, metadata):    
    global value   
    tempValue = 0
    for i in range(index , index +metadata):
        tempValue += int(input_list[i])    
    value += tempValue

def grabMetadataChildren(index, metadata):
    childReferences = []
    for i in range(index, index+metadata):
        childReferences.append(int(input_list[i]))
    return childReferences

def childReferences(metaDataStart, metadata):
    returnNodes = []
    for i in range(metaDataStart, metaDataStart + metadata):
        returnNodes.append(int(input_list[i]))
    return returnNodes

def GetNodeValue(currentNode):   
    childNodes = [x for x in nodes if x.parent == currentNode.name]    
    if childNodes:
        for i in childReferences(currentNode.metadataStart, currentNode.metadata):
            if i - 1< len(childNodes):
                GetNodeValue(childNodes[i -1]) or 0
    else:
        grabMetadata(currentNode.metadataStart, currentNode.metadata)
        

def getNode(index, parent):    
    currentNode = node(GetName(), parent, index)
    nodes.append(currentNode)        
    #go into the child nodes
    index += 2
    for _ in range(currentNode.childnodes):
        index = getNode(index, currentNode.name) 
    currentNode.metadataStart = index        
    return index + currentNode.metadata    
    
index = 0
while index < len(input_list):
    index = getNode(index, None)

GetNodeValue(nodes[0])

print(value)