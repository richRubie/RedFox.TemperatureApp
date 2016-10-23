import requests

t = requests.get('http://192.168.0.32/redfox.temperatureapp/api/values/32')
print(t)
tmp = 4

authUrl = 'http://192.168.0.32/redfox.identityserver/connect/token'
tempAppUrl = 'http://192.168.0.32/redfox.temperatureapp/api/temperature'


payload = {'grant_type':'client_credentials','scope':'api1','client_id':'client','client_secret':'secret'}

authResponse = requests.post(authUrl, data=payload)
dict = authResponse.json()
accessToken = dict['access_token']

headers = {'Authorization':'Bearer '+accessToken, 'Content-Type':'application/json; charset=utf-8'}

tempAppParams = {'Temperature':tmp}
tempAppResponse =  requests.post(tempAppUrl, headers=headers, json=tempAppParams)

print(tempAppResponse)
