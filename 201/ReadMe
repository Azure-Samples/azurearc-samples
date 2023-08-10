
# 201 sample

This sample shows you how to use utilize Azure Key Vault, Azure Container Registry and Azure Arc enabled Key Vault Extension to dynamically pull ACR credentials from AKV and pull image from ACR.
## Prerequisites
* Compatible cluster, aks and aks ee is recommended 
* <a href = "https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/reference">AZD</a>

## Steps

* (Optional - Skip if already has an SP) Follow the steps to <a href="https://learn.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal#register-an-application-with-azure-ad-and-create-a-service-principal">create a service principal</a> in Azure. Take note of the Client ID, tenantID and Client Secret generated in this step.

* Use the client ID and Client Secret from the first step to create a Kubernetes secret on the connected cluster:
```
kubectl create secret generic secrets-store-creds --from-literal clientid="<client-id>" --from-literal clientsecret="<client-secret>"
```
* Label the created secret:
```
kubectl label secret secrets-store-creds secrets-store.csi.k8s.io/used=true
```
* Install prerequites AKV (acess policy), ACR (Token) and Azure Arc enabled Key Vault Extension. <br />
Fill in the 201\201bicep\infra\main.parameters.json file and deploy the bicep template using the below command in folder 201\201bicep
```powershell
azd provision
```
* (Optional) Skip if you don't want to create it yourself. Bicep template creates a token for you with pull permission to all repositories. <br />
Create a token resource in the ACR created in the bicep deployment. Take a note of the username and password. You can define the token scope yourself or use the default scopes. Make sure the scope covers the image you would use in the k8s deployment. It should have the permission to pull the requied image at least. <br />
![HowToCreateAToken](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/201/screenshots/Token.png) <br />
* Generating password for ACR token, either using portal or command <br />
![HowToGeneratePwd](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/201/screenshots/Pwd.png) <br />
```
$acrName = "{ACRName}" # Created in bicep
$tokenName = "{TokenName}" # Created in bicep or created yourself
$passwords = az acr token credential generate --registry $acrName --name $tokenName | ConvertFrom-Json
```
After generating the paswword, Generate the ACR credential <br />

```json
{"auths":{"ACR_URI":{"auth":"Base64(USERNAME:PASSWORD)"}}}
```
Sample:
```json
{"auths":{"sample.azurecr.io":{"auth":"dXNlcm5hbWU6cGFzc3dvcmQ="}}}
```
You can use this powershell to generate the credential string. Toke a note of the credential:
```
$username = "{USERNAME}"
$password = "{PASSWORD}"
$acrName = "{ACRName}"
$stringInBytes = [System.Text.Encoding]::UTF8.GetBytes("${username}:${password}")
$stringEncoded = [System.Convert]::ToBase64String($stringInBytes)
$credential = "{`"auths`":{`"${acrName}.azurecr.io`":{`"auth`":`"${stringEncoded}`"}}}"
$credential
```
* Save the ACR credential as a secret to the AKV created in bicep deployment. Take note of the secret name. <br />
![HowToCreateASecret](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/201/screenshots/KV.png)


* Push your image to the ACR <br />
Sample code to pull a hello-world image from docker.io and push to ACR (Ignore the warning).
```
$username = "{USERNAME}"
$password = "{PASSWORD}"
$acrName = "{ACRName}"
$imageTag = "{IMAGETAG}" # For example, sample.azurecr.io/samplerepo:latest 
docker pull hello-world:latest
docker tag hello-world:latest $imageTag
docker login "${acrName}.azurecr.io" -u $username -p $password
docker push $imageTag
```

* Go to 201\201chart\values.yaml and input the params
```
replicaCount: 1 # Optional
image:
  repository: "fill in image repository" # Required, change it to the image you pushed to ACR, like sample.azurecr.io/samplerepo
  pullPolicy: IfNotPresent # Optional
  tag: "fill in image tag" # Required, change it to the image version, like latest

nameOverride: ""  # Optional
fullnameOverride: ""  # Optional

keyvaultName: "fill in the key vault name" # Required, the key vault name in the main.parameters.json
keyvaultSecretName: "fill in the secret name in key vault" # Required, the secret name you created in the key vault
secretName: "fill in the secret name generated using 'kubectl create secret' command" # Requried, the secret name you created in k8s
tenantId: "fill in the tenant id of subscription" # Required, the tenantId of your SP
```

* Deploy the chart
```
helm install .\201\201chart\ --generate-name
```