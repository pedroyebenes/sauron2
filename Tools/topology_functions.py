#!/usr/bin/python3

def create_module(type, name, gates, number):
	d = dict()
	d['Type'] = type
	d['Name'] = name
	d['Gates'] = gates
	d['Number'] = number
	return d

def create_connection(moduleA, indexA, portA, moduleB, indexB, portB):
	d = dict()
	d['ModuleA'] = moduleA
	d['ModuleB'] = moduleB
	d['IndexA'] = indexA
	d['IndexB'] = indexB
	d['PortA'] = portA
	d['PortB'] = portB
	return d


def ring(args):
	assert(args.module_ports >= 2)
	assert(args.first_port >= 0)
	assert(args.last_port > 0)
	assert(args.first_port < args.last_port)
	assert(args.number_modules >= 2)

	conns = []
	for i in range(0, args.number_modules):	
		if i !=  args.number_modules-1:
			c = create_connection(args.module_name, i, args.last_port, args.module_name, i+1, args.first_port)	
		else :
			c = create_connection(args.module_name, args.number_modules-1, args.last_port, args.module_name, 0, args.first_port)
		conns.append(c)

	mod = create_module(args.module_type, args.module_name, args.module_ports, args.number_modules)

	return mod, conns
