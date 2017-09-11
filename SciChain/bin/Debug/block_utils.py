import sys
import hashlib
import uuid
import json

import argparse

#parser = argparse.ArgumentParser(description='Hashes and Mines Blocks')
#parser.add_argument("data", help="Block's data")
#parser.add_argument("nonce", help="Block's nonce")
#parser.add_argument("block_hash", help="Block's hash")
#parser.add_argument("previous_hash", help="Block's previous hash")
#parser.add_argument("-m", "--mine", action="count",
                    #help="Mine block")

#args = parser.parse_args()


def hash_is_valid(the_hash):
        return the_hash.startswith('0000')

def hash(nonce, data, previous_hash):

	message = hashlib.sha256()
	message.update(str(nonce).encode('utf-8'))
	message.update(str(data).encode('utf-8'))
	message.update(str(previous_hash).encode('utf-8'))

	return message.hexdigest()

def hash_blocks():
	f = open('chain.json', 'r')
	arr = json.loads(f.read())

	for i in arr:
		i["hash"] = hash(i['nonce'], i['data'], i['previousHash'])

	f = open('chain.json', 'w')
	f.write(json.dumps(arr))
	f.truncate()
	f.close()

def mine():
	f = open('chain.json', 'r')
	arr = json.loads(f.read())

	for i in arr:
		the_nonce = int(i['nonce'])
		while 1:
			i['hash'] = hash(the_nonce, i['data'], i['previousHash'])
			if hash_is_valid(i['hash']):
				i['nonce']= the_nonce
				break
			else:
				the_nonce += 1
	f = open('chain.json', 'w')
	f.write(json.dumps(arr))
	f.truncate()
	f.close()



#if args.mine:
mine()
#else:
#print(hash_blocks())