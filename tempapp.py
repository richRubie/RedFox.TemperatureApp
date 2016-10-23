
import requests
#import subprocess
import json

#tmp = float(subprocess.check_output(["/opt/vc/bin/vcgencmd","measure_temp"]).decode("utf-8"$
tmp = 81
authUrl = 'http://localhost.fiddler/redfox.identityserver/connect/token'
tempAppUrl = 'http://localhost.fiddler/redfox.temperatureapp/api/temperature'

#authUrl = 'http://localhost.fiddler:5000/connect/token'
#tempAppUrl = 'http://localhost.fiddler:5001/api/temperature'


payload = {'grant_type':'client_credentials','scope':'api1','client_id':'client','client_secret':'secret'}

authResponse = requests.post(authUrl, data=payload)
dict = authResponse.json()
accessToken = dict['access_token']

headers = {'Authorization':'Bearer '+accessToken, 'Content-Type':'application/json; charset=utf-8'}

tempAppParams = {'Temperature':tmp}
tempAppResponse =  requests.post(tempAppUrl, headers=headers, json=tempAppParams)
print(tempAppResponse)
