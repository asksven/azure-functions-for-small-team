# MSaaS (MicroService as a Service)

## Enviroment
- Resource Group: rgmsaas
- Function App: msaas-dev (msaas-stg and/or msaas-prd may be added as needed)
- storage account: samsaasdev
- Application Insights: on

## Deployments
The Azure Function App is configured for continous deployment from github, using a branch per deployment tier:
branch DEV deploys to msaas-dev

# Repo structure
- MASTER contains this doc as well as the artefacts to create the environment
- DEV contains one directory per function, each directory contains a file `function.json` (definition) and a file `run.csx` (function code)