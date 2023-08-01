@description('The name of the connected k8s to have KV extension.')
param aksName string

@description('The name of the key vault to be created.')
param vaultName string

@description('The location of the key vault')
param location string

@secure()
param secretValue string

@description('The appid of service principal to grant keyvault access')
param appid string
@description('The objid of service principal to grant keyvault access')
param objid string

resource aks 'Microsoft.Kubernetes/connectedClusters@2022-10-01-preview' existing = {
  name: aksName
}

resource symbolicname 'Microsoft.KubernetesConfiguration/extensions@2022-11-01' = {
  name: 'akvsecretsprovider'
  scope: aks
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
  name: vaultName
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

resource secret 'Microsoft.KeyVault/vaults/secrets@2022-07-01' = {
  name: 'aks'
  parent: vault
  properties: {
    attributes: {
      enabled: true
    }
    value: secretValue
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
          certificates: [
            'all'
          ]
          keys: [
            'all'
          ]
          secrets: [
            'all'
          ]
          storage: [
            'all'
          ]
        }
        tenantId: subscription().tenantId
      }
    ]
  }
}

