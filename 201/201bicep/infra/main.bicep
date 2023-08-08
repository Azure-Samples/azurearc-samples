@description('The name of the connected k8s to have KV extension.')
param ArcK8sName string

@description('The name of the key vault to be created.')
param VaultName string

@description('The location of the key vault')
param location string = resourceGroup().id

@description('The appid of service principal to grant keyvault access')
param appid string
@description('The objid of service principal to grant keyvault access')
param objid string

@description('The name of the Azure Container Registry')
param acrName string

@description('The name of the ACR token')
param tokenName string

resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: acrName
  location: location
  sku: {
    name: 'Standard'
  }
}

resource scope 'Microsoft.ContainerRegistry/registries/scopeMaps@2023-01-01-preview' existing = {
  name: '_repositories_pull'
  parent: acr
}

resource token 'Microsoft.ContainerRegistry/registries/tokens@2023-01-01-preview' = {
  name: tokenName
  parent: acr
  properties: {
    credentials: {
      passwords: [
      ]
    }
    scopeMapId: scope.id
  }
}

resource k8s 'Microsoft.Kubernetes/connectedClusters@2022-10-01-preview' existing = {
  name: ArcK8sName
}

resource symbolicname 'Microsoft.KubernetesConfiguration/extensions@2022-11-01' = {
  name: 'akvsecretsprovider'
  scope: k8s
  properties: {
    autoUpgradeMinorVersion: true
    configurationProtectedSettings: {}
    configurationSettings: {}
    extensionType: 'Microsoft.AzureKeyVaultSecretsProvider'
    releaseTrain: 'Stable'
    scope: {
      cluster: {
        releaseNamespace: 'kube-system'
      }
    }
  }
}

resource vault 'Microsoft.KeyVault/vaults@2021-11-01-preview' = {
  name: VaultName
  location: location
  properties: {
    accessPolicies:[]
    enableRbacAuthorization: false
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: false
    tenantId: subscription().tenantId
    sku: {
      name: 'standard'
      family: 'A'
    }
    networkAcls: {
      defaultAction: 'Allow'
      bypass: 'AzureServices'
    }
  }
}

// Grant accessPolicy of KeyVault to application
resource keyvaultadd 'Microsoft.KeyVault/vaults/accessPolicies@2022-07-01' = {
  parent: vault
  name: 'add'
  properties: {
    accessPolicies: [
      {
        applicationId: appid
        objectId: objid
        permissions: {
          secrets: [
            'get'
            'list'
          ]
        }
        tenantId: subscription().tenantId
      }
    ]
  }
}
