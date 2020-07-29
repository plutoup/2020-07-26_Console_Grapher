import math
import importlib
import sys

def main(argument):
    y = "{y}"
    file = open("writer.py","w")
    file.write(f'import json\nimport math\n\ndata = ' + "{" + "}" + f'\ndata["y_coordinates"] = []\n\nfor x in range(-500, 500):\n\ty = {argument}\n\tdata["y_coordinates"].append(f\"{y}\")\n\nwith open("data.json", "w") as outfile:\n\tjson.dump(data, outfile)')
    file.close()

if __name__ == "__main__":
    main(sys.argv[1])