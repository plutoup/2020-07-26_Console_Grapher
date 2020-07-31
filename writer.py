import json
import math

data = {}
data["y_coordinates"] = []

for x in range(-500, 500):
	y = 3*x
	data["y_coordinates"].append(f"{y}")

with open("data.json", "w") as outfile:
	json.dump(data, outfile)