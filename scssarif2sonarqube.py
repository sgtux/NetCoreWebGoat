#!/bin/python3

import json, argparse, sys, re, pathlib

try:
    parser = argparse.ArgumentParser(description='Converter Horusec sonar output format to SonarQube format ', allow_abbrev=True)
    parser.add_argument('--file', type=str , help='Sarif file to parse', required='True')
    parser.add_argument('--scsoutput', type=str , help='Security Code Scan text output', required='True')
    args = parser.parse_args()
except:
    sys.exit(1)

lines = []

scsCwes = {}

with open(args.scsoutput) as f:
    line = f.readline()
    while(line):
        if line.__contains__('SCS'):
            matchScs = re.search(r'SCS[0-9]{1,}', line)
            matchCwe = re.search(r'CWE-[0-9]{1,}', line)
            if matchScs and matchCwe and matchScs.group() not in scsCwes:
                    scsCwes[matchScs.group()] = matchCwe.group()
        line = f.readline()

sarif = None
issues = []

with open(args.file) as f:
    sarif = json.loads(f.read())

for item in sarif['runs'][0]['results']:
    obj = {}
    obj['type'] = 'VULNERABILITY'
    obj['ruleId'] = 'SecurityCodeScan'
    obj['engineId'] = 'SCS'
    obj['severity'] = 'BLOCKER'
    obj['effortMinutes'] = 0
    location = {}
    location['message'] = scsCwes[item['ruleId']] + ' ' + item['message']['text']
    location['textRange'] = item['locations'][0]['physicalLocation']['region']
    location['filePath'] = item['locations'][0]['physicalLocation']['artifactLocation']['uri'].replace('file://', '').replace(str(pathlib.Path(__file__).parent.absolute()) + '/', '')
    obj['primaryLocation'] = location
    issues.append(obj)

with open('./sonar-parsed-v2.json', 'w') as f:
    f.write(json.dumps({'issues': issues}))