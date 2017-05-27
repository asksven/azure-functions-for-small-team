# Azure Function Apps hosting for small teams

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
6. At this point you can either refer to github project with `--template-uri` or run everything locally as the Azure CLI has a git client
7. Validate deployment: `az group deployment validate -g <rg-name> --template-uri https://raw.githubusercontent.com/asksven/azure-functions-for-small-team/master/template.json --verbose`
8. Check if the functions webapp does not exist: `wget http://<value-of-components_name-from-template.json>` (if it exists you must change the name of the web-app in the ARM template and re-test) should return `unable to resolve`. *If the web-site exists already the validation will not detect it but the deployment will fail!*
9. Deploy using the ARM template: `az group deployment create -g <rg-name> --template-uri https://raw.githubusercontent.com/asksven/azure-functions-for-small-team/master/template.json --verbose`
10. go to the Azure functions all, disconnect and re-connect the github deployment

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
