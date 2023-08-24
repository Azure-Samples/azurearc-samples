from azure.identity import DefaultAzureCredential
from azure.mgmt.kubernetesconfiguration import SourceControlConfigurationClient

class ArcEnabledExtensionHelper:
    # A class with 6 string fields and four functions

    def __init__(self, subscriptionId, resourceGroupName, clusterName, extensionName):
        # The constructor method that initializes the fields
        self.subscriptionId = subscriptionId
        self.resourceGroupName = resourceGroupName
        self.clusterName = clusterName
        self.extensionName = extensionName
        self.clusterRp = "Microsoft.Kubernetes"
        self.clusterResourceName = "connectedClusters"
        self.client = SourceControlConfigurationClient(
            credential=DefaultAzureCredential(),
            subscription_id=subscriptionId,
        )

    def Get_GetExtension(self):
        response = self.client.extensions.get(
            resource_group_name=self.resourceGroupName,
            cluster_rp=self.clusterRp,
            cluster_resource_name=self.clusterResourceName,
            cluster_name=self.clusterName,
            extension_name=self.extensionName,
        )
        return response

    def Update_UpdateExtension(self, patch):
        response = self.client.extensions.begin_update(
            resource_group_name=self.resourceGroupName,
            cluster_rp=self.clusterRp,
            cluster_resource_name=self.clusterResourceName,
            cluster_name=self.clusterName,
            extension_name=self.extensionName,
            patch_extension=patch,
        ).result()
        return response

    def List_ListExtension(self):
        response = self.client.extensions.list(
            resource_group_name=self.resourceGroupName,
            cluster_rp=self.clusterRp,
            cluster_resource_name=self.clusterResourceName,
            cluster_name=self.clusterName,
        )
        return response

    def Delete_DeleteExtension(self):
        response = self.client.extensions.begin_delete(
            resource_group_name=self.resourceGroupName,
            cluster_rp=self.clusterRp,
            cluster_resource_name=self.clusterResourceName,
            cluster_name=self.clusterName,
            extension_name=self.extensionName,
        ).result()
        return response
    
    def Create_CreateExtension(self, extDef):
        response = self.client.extensions.begin_create(
            resource_group_name=self.resourceGroupName,
            cluster_rp=self.clusterRp,
            cluster_resource_name=self.clusterResourceName,
            cluster_name=self.clusterName,
            extension_name=self.extensionName,
            extension=extDef,
        ).result()
        return response


subId = "746a51ba-0bd4-497f-89cc-f955a5db3bc8"
rgName = "jesseaks"
clusterName = "jesseaksee"
extensionName = "akvsecretsprovider"
helper = ArcEnabledExtensionHelper(subId, rgName, clusterName, extensionName)

# Create extension
definition= {
                "properties": {
                    "autoUpgradeMinorVersion": True,
                    "configurationSettings": {
                        "secrets-store-csi-driver.rotationPollInterval": "2m",
                        "secrets-store-csi-driver.syncSecret.enabled": "true",
                        "secrets-store-csi-driver.enableSecretRotation": "true",
                    },
                    "extensionType": "Microsoft.AzureKeyVaultSecretsProvider",
                    "releaseTrain": "stable",
                    "scope": {"cluster": {"releaseNamespace": "kube-system"}},
                }
            }
ext = helper.Create_CreateExtension(definition)
print(f"Created extension: {ext.name}, Type: {ext.extension_type}, Provisioning Status: {ext.provisioning_state}, Configuration:" )
print(ext.configuration_settings)

# # Get extension
# ext = helper.Get_GetExtension()
# print(f"Found extension: {ext.name}, Type: {ext.extension_type}, Provisioning Status: {ext.provisioning_state}, Configuration:" )
# print(ext.configuration_settings)

# # Update extension
# patch = {
#             "properties": {
#                 "configurationSettings": {
#                     "secrets-store-csi-driver.rotationPollInterval": "15m",
#                 },
#             }
#         }
# ext = helper.Update_UpdateExtension(patch)
# print(f"Updated extension: {ext.name}, Type: {ext.extension_type}, Provisioning Status: {ext.provisioning_state}, Configuration:" )
# print(ext.configuration_settings)

# # List extension
# exts = helper.List_ListExtension()
# for ext in exts:
#     print(f"Found extension: {ext.name}, Type: {ext.extension_type}, Provisioning Status: {ext.provisioning_state}, Configuration:" )
#     print(ext.configuration_settings)

# # Delete extension
# ext = helper.Delete_DeleteExtension()
# print(f"successfully deleted extension: {extensionName}")