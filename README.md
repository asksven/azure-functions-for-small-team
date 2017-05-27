# MSaaS (MicroService as a Service)

## Azure Enviroment
The Azure enviroment consists of:
- a Resource Group: rgmsaas
- a Function App: msaas-dev (msaas-stg and/or msaas-prd may be added as needed)
- a storage account: samsaasdev
- Application Insights

### Create the environment
The environment is easily create from the Azure CLI 2.0 in the portal.

1. Log-on to the Azure portal and go to your subscription
2. Open the CLI by clicking the `>_` (upper-right corner)
3. Check that you are in the right subscription: `az account show`
4. Check if the resource group exists: `az group exists --name <rg-name>` (should return `false`)
5. Create the resource group: `az group create --name <rg-name> --location westeurope`

## Deployment
The Azure Function App is configured for continous deployment from github, using a branch per deployment tier:
branch DEV deploys to msaas-dev

# Repo structure
- MASTER contains this doc as well as the artefacts to create the environment
- DEV contains one directory per function, each directory contains a file `function.json` (definition) and a file `run.csx` (function code)

# API key management
Each function is protected by 1..n API keys (e.g. one key per API-consumer).

## Manually managing keys in the portal
In the portal go to the function app, select the function and "manage". There you can add, edit, read, revoke or renew keys.

## Manage keys from the storage account
1. go to the management console (Kudu): https://<function-app-name-here>.scm.azurewebsites.net/DebugConsole/?shell=powershell
2. navigate to `data/Functions/secrets`

<function-name>.json contains the keys

# Testing
Functions can easily be tested from e.g. PowerShell:
```
$url = "<your-azure-function-endpoint-goes-here>" # append ?code=<valid-api-key>
$body = '{"name": "value"}'

Invoke-RestMethod $url -Body $body -Method Post -ContentType 'application/json'
```

## Return codes
- 200: OK
- 401 Unauthorized: if the API key is wrong or no API key was passed