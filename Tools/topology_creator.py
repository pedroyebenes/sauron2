#!/usr/bin/python3

import json
import argparse
import topology_functions

def parse_args():
	parser = argparse.ArgumentParser(description='Topology generator.')
	
	parser.add_argument("-nm", "--number_modules", type=int, default=10,     help="Modules in the topology")
	parser.add_argument("-mn", "--module_name",   type=str, default="Node", help="Module's name")
	parser.add_argument("-mt", "--module_type",   type=str, default="Node", help="Module's type")
	parser.add_argument("-mp", "--module_ports",  type=int, default=2,      help="Ports of the module")
	
	parser.add_argument("-fp", "--first_port",  type=int, default=0,      help="First port to be connected")
	parser.add_argument("-lp", "--last_port",   type=int, default=1,      help="Last port to be connected")
	
	parser.add_argument("-t",  "--topology",      type=str, default="ring", help="Connection pattern")
	
	return parser.parse_args()

def get_JSON(module, connections):
	jo = dict(Connections=[], Modules=[])
	jo['Modules'].append(module)
	jo['Connections'].append(connections)

	return json.dumps(jo, indent=4, separators=(', ', ': '), sort_keys=True)

def main():
	args = parse_args()	

	if args.topology == "ring" :
		mod, conns = topology_functions.ring(args)
	else :
		print("Topology '%s' is not defined" % args.topology)
		exit(1)

	print(get_JSON(mod, conns))

if __name__ == "__main__":
    main()

