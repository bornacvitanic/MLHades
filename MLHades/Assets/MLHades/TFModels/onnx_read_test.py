import onnx
import json

model = onnx.load('./MLHades.onnx')
graph=model.graph
for _input in graph.input:
    print (_input.name, end=": ")
    # get type of input tensor
    tensor_type = _input.type.tensor_type
    # check if it has a shape:
    if (tensor_type.HasField("shape")):
        # iterate through dimensions of the shape:
        for d in tensor_type.shape.dim:
            # the dimension may have a definite (integer) value or a symbolic identifier or neither:
            if (d.HasField("dim_value")):
                print (d.dim_value, end=", ")  # known dimension
            elif (d.HasField("dim_param")):
                print (d.dim_param, end=", ")  # unknown dimension with symbolic name
            else:
                print ("?", end=", ")  # unknown dimension with no name
    else:
        print ("unknown rank", end="")
    print()

for _output in graph.output:
    print (_output.name, end=": ")
    print()

for _node in graph.node:
    print(_node.name)
    
with open("input.json", "w") as file:
    file.write(str(graph.input))
with open("node.json", "w") as file:
    file.write(str(graph.node))
with open("output.json", "w") as file:
    file.write(str(graph.output))
