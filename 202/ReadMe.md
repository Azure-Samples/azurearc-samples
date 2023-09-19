# 202 sample - Manage arc extensions with different languages

## Scenario
An operator working with an Arc-enabled K8S cluster would like to manage the cluster extensions in his/her own way. This sample provides some basic usage of the extension SDKs, including CRUD and list. 

### Required
- A native K8S cluster connected to Arc. The Azure Arc VS code extension can help to create an AKS EE cluster and connect to Arc if needed.
- [AZS CLI](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/reference)
- A service principal with password secret is required.
    - If you don't have one yet, follow [these steps](https://learn.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal#register-an-application-with-azure-ad-and-create-a-service-principal) to create an SP with password (not with certificate).
    - If you'd like to reuse an existing SP, make sure it has a password secret. You can associate a new secret with your existing SP with these [instructions](https://learn.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal#option-3-create-a-new-application-secret).
  
    Please take note of the Client ID (a.k.a., App ID), tenantID, service principal object ID and Client Secret generated in this step.

### Recommended
- VS code with [Azure Arc Extension](https://marketplace.visualstudio.com/search?term=azure%20arc&target=VSCode&category=All%20categories&sortBy=Relevance)
- An AKS or AKS EE cluster that's connected to Arc to minimize compatibility issues
  > The Azure Arc Extension can help creating AKS EE clusters and connecting compatible K8S clusters to Arc.

## Steps
1.  Assign the service principal with Azure Arc Kubernetes Admin.
    - go to portal cluster page and click Access control (IAM)
    - click add role assignment
    ![AddRoleAssignment](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/202/screenshots/RoleAssignment.png)
    - choose Azure Arc Kubernetes Admin as Job function roles 
    - input the SP name and select it
    ![AddRoleAssignment](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/202/screenshots/ChooseSP.png)
2. Input the ClientId, tenantId and secret in the Dockerfile. For instacne: 
```
ENV AZURE_TENANT_ID="<TenantId>"
ENV AZURE_CLIENT_ID="<ClientId>"
ENV AZURE_CLIENT_SECRET="<ClientSecret>"
```
3. Docker build and run the image.