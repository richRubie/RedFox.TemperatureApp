#!/usr/bin/python

import requests
# import json
import configparser
import Adafruit_DHT

config = configparser.ConfigParser()
config.readfp(open('defaults.cfg'))
config.read(['pi.cfg', 'local.cfg'])

isRaspberryPi = config.getboolean('environment','isRaspberryPi')

if isRaspberryPi:
	gpioPin = config.getint('environment','gpioPin')
	humidity, temperature = Adafruit_DHT.read_retry(Adafruit_DHT.AM2302, gpioPin)
else:
	temperature = 6
	humidity = 8

location = config.get('environment','location');

authUrl = config.get('environment', 'authUrl')
tempAppUrl = config.get('environment','tempAppUrl')

client_id = config.get('authorisation', 'client_id')
scope = config.get('authorisation', 'scope')
client_secret = config.get('authorisation', 'client_secret')

payload = {'grant_type':'client_credentials','scope':scope,'client_id':client_id,'client_secret':client_secret}

authResponse = requests.post(authUrl, data=payload)
responseDict = authResponse.json()
accessToken = responseDict['access_token']

headers = {'Authorization':'Bearer '+accessToken, 'Content-Type':'application/json; charset=utf-8'}

tempAppParams = {'Temperature':temperature, 'Humidity': humidity, 'Location': location}
tempAppResponse =  requests.post(tempAppUrl, headers=headers, json=tempAppParams)
