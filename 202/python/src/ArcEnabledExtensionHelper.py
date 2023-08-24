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

    # def introduce(self):
    #     # A function that prints an introduction message

    # def change_name(self, new_name):
    #     # A function that changes the name field
    # def change_hobby(self, new_hobby):
    #     # A function that changes the hobby field