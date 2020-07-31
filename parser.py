import math
import importlib
import sys

def main(argument):
    y = "{y}"
    file = open("scriber.py","w")
    file.write(f'import json\nimport math\n\ndef main():\n\ttry:\n\t\tdata = ' + "{" + "}" + f'\n\t\tdata["y_coordinates"] = []\n\t\tfor x in range(-500, 500):\n\t\t\ty = {argument}\n\t\t\tif type(y) is float:\n\t\t\t\tdata["y_coordinates"].append(f"{y}")\n\t\t\telif type(y) is int:\n\t\t\t\tdata["y_coordinates"].append(f"{y}")\n\t\t\telse:\n\t\t\t\traise Exception(\'Improper input : Usage -> graph \\\"<function>\\\"\\n\\tExample : graph \\\"math.pow(x,2)\\\"\')\n\n\t\twith open("data.json", "w") as outfile:\n\t\t\tjson.dump(data, outfile)\n\texcept NameError:\n\t\tprint(\"Improper input : Usage -> graph \\\"<function>\\\"\\\t\\\tExample : graph \\\"math.pow(x,2)\\\"\")\nif __name__ == "__main__":\n\tmain()')
    file.close()

if __name__ == "__main__":
    main(sys.argv[1])