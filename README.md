# Key Vault Backup & Restore Sample #

This provides sample codes to backup and restore an Azure Key Vault secrets through the Logic App workflow.


## Prerequisites ##

* [Azure Account (Free)](https://azure.microsoft.com/free/?WT.mc_id=devkimchicom-github-juyoo)
* [Visual Studio Code](https://code.visualstudio.com/?WT.mc_id=devkimchicom-github-juyoo)


## Readings ##

* [Writing ARM Templates in YAML](https://devkimchi.com/2018/08/07/writing-arm-templates-in-yaml/)
* [Introducing YARM CLI](https://devkimchi.com/2018/08/04/introducing-yarm-cli/)
* [Separation of Concerns: Logic App from ARM Template](https://devkimchi.com/2018/06/14/separation-of-concerns-logic-app-from-arm-template/)


## Getting Started ##

### Login to Azure ###

```powershell
Connect-AzureRmAccount
```

```bash
az login
```


### Convert ARM Templates in YAML to JSON ###

```powershell
$rootDirectory = "[ROOT_DIRECTORY_OF_CLONED_REPOSITORY]"

Convert-Templates.ps1 -RootDirectory $rootDirectory
```


### Provision Resources through ARM Templates ###

Run the ARM template deployment in the following order:

1. Integration Account: `integrationAccount.json`
1. Storage Account: `storageAccount.json`
1. API Connection for Azure Blob: `connection.azureblob.json`
1. Logic App for backup: `logicApp.json`
1. Logic App for restore: `logicApp.json`
1. Key Vault for backup: `keyVault.json`
1. Key Vault for restore: `keyVault.json`

```powershell
New-AzureRmResourceGroupDeployment `
    -Name "[DEPLOYMENT_NAME]" `
    -ResourceGroupName "[RESOURCE_GROUP_NAME]"
    -TemplateFile "integrationAccount.json" `
    -TemplateParameterFile "integrationAccount.parameters.json" `
    -Verbose
```


### Add Workflow to Logic App ###

```powershell
Set-LogicAppWorkflow.ps1 `
    -ResourceGroupName "[RESOURCE_GROUP_NAME]" `
    -LogicAppName "[LOGIC_APP_NAME]" `
    -DefinitionFile "[WORKFLOW_DEFINITION_FILE]" `
    -ParameterFile "[WORKFLOW_DEFINITION_PARAMETER_FILE]"
```


### Add Sample Secrets to Key Vault ###

```powershell
$secrets = @{ one = "lorem"; two = "ipsum"; three = "hello"; four = "workd"; }

Set-AzureKeyVaultSecrets.ps1 `
    -KeyVaultName "[KEY_VAULT_INSTANCE_NAME]" `
    -Secrets $secrets `
    -IsLocal
```


### Run Logic Apps ###

Run the Logic App for backup, followed by Logic App for restore


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work with corresponding tests, please send us a pull request onto our `master` branch for review.


## License ##

This is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2019 [Dev Kimchi](https://devkimchi.com)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
