#!/usr/bin/python

import requests
import subprocess
import json
import configparser

config = configparser.ConfigParser()
config.readfp(open('defaults.cfg'))
config.read(['pi.cfg', 'local.cfg'])

isRaspberryPi = config.getboolean('environment','isRaspberryPi')

if isRaspberryPi:
        tmp = float(subprocess.check_output(["/opt/vc/bin/vcgencmd","measure_temp"]).decode("utf-8").replace("temp=","").replace("'C\n","")) - 16.3
else:
	tmp = 6

authUrl = config.get('environment', 'authUrl')
tempAppUrl = config.get('environment','tempAppUrl')
print(authUrl)

client_id = config.get('authorisation', 'client_id')
scope = config.get('authorisation', 'scope')
client_secret = config.get('authorisation', 'client_secret')

payload = {'grant_type':'client_credentials','scope':scope,'client_id':client_id,'client_secret':client_secret}

authResponse = requests.post(authUrl, data=payload)
dict = authResponse.json()
accessToken = dict['access_token']

headers = {'Authorization':'Bearer '+accessToken, 'Content-Type':'application/json; charset=utf-8'}

tempAppParams = {'Temperature':tmp}
tempAppResponse =  requests.post(tempAppUrl, headers=headers, json=tempAppParams)
