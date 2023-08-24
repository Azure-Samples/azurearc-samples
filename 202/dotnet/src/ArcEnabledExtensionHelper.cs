// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.KubernetesConfiguration;
using Azure.ResourceManager.KubernetesConfiguration.Models;

namespace _202
{
    public class ArcEnabledExtensionHelper
    {
        private readonly string subscriptionId;
        private readonly string resourceGroupName;
        private readonly string clusterRp = "Microsoft.Kubernetes";
        private readonly string clusterResourceName = "connectedClusters";
        private readonly string clusterName;
        private readonly string extensionName;

        public ArcEnabledExtensionHelper(string subscriptionId, string resourceGroupName, string clusterName, string extensionName)
        {
            this.subscriptionId = subscriptionId;
            this.resourceGroupName = resourceGroupName;
            this.clusterName = clusterName;
            this.extensionName = extensionName;
        }



        // Get Extension
        public async Task<KubernetesClusterExtensionData> Get_GetExtension()
        {
            // Generated from example definition: specification/kubernetesconfiguration/resource-manager/Microsoft.KubernetesConfiguration/stable/2022-11-01/examples/GetExtension.json
            // this example is just showing the usage of "Extensions_Get" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this KubernetesClusterExtensionResource created on azure
            // for more information of creating KubernetesClusterExtensionResource, please refer to the document of KubernetesClusterExtensionResource
            ResourceIdentifier kubernetesClusterExtensionResourceId = KubernetesClusterExtensionResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, clusterRp, clusterResourceName, clusterName, extensionName);
            KubernetesClusterExtensionResource kubernetesClusterExtension = client.GetKubernetesClusterExtensionResource(kubernetesClusterExtensionResourceId);

            // invoke the operation
            KubernetesClusterExtensionResource result = await kubernetesClusterExtension.GetAsync();

            // the variable result is a resource, you could call other operations on this instance as well
            // but just for demo, we get its data from this resource instance
            KubernetesClusterExtensionData resourceData = result.Data;
            
            return resourceData;
        }

        // Get Extension with Plan
        public async Task<KubernetesClusterExtensionData> Get_GetExtensionWithPlan()
        {
            // Generated from example definition: specification/kubernetesconfiguration/resource-manager/Microsoft.KubernetesConfiguration/stable/2022-11-01/examples/GetExtensionWithPlan.json
            // this example is just showing the usage of "Extensions_Get" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this KubernetesClusterExtensionResource created on azure
            // for more information of creating KubernetesClusterExtensionResource, please refer to the document of KubernetesClusterExtensionResource
            ResourceIdentifier kubernetesClusterExtensionResourceId = KubernetesClusterExtensionResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, clusterRp, clusterResourceName, clusterName, extensionName);
            KubernetesClusterExtensionResource kubernetesClusterExtension = client.GetKubernetesClusterExtensionResource(kubernetesClusterExtensionResourceId);

            // invoke the operation
            KubernetesClusterExtensionResource result = await kubernetesClusterExtension.GetAsync();

            // the variable result is a resource, you could call other operations on this instance as well
            // but just for demo, we get its data from this resource instance
            KubernetesClusterExtensionData resourceData = result.Data;
            // for demo we just print out the id
            return resourceData;
        }

        // Delete Extension
        public async Task Delete_DeleteExtension()
        {
            // Generated from example definition: specification/kubernetesconfiguration/resource-manager/Microsoft.KubernetesConfiguration/stable/2022-11-01/examples/DeleteExtension.json
            // this example is just showing the usage of "Extensions_Delete" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this KubernetesClusterExtensionResource created on azure
            // for more information of creating KubernetesClusterExtensionResource, please refer to the document of KubernetesClusterExtensionResource
            ResourceIdentifier kubernetesClusterExtensionResourceId = KubernetesClusterExtensionResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, clusterRp, clusterResourceName, clusterName, extensionName);
            KubernetesClusterExtensionResource kubernetesClusterExtension = client.GetKubernetesClusterExtensionResource(kubernetesClusterExtensionResourceId);

            // invoke the operation
            await kubernetesClusterExtension.DeleteAsync(WaitUntil.Completed);
        }

        // Update Extension
        public async Task<KubernetesClusterExtensionData> Update_UpdateExtension(KubernetesClusterExtensionPatch patch)
        {
            // Generated from example definition: specification/kubernetesconfiguration/resource-manager/Microsoft.KubernetesConfiguration/stable/2022-11-01/examples/PatchExtension.json
            // this example is just showing the usage of "Extensions_Update" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            //var options = new ArmClientOptions();
            //var resourceType = new ResourceType("Microsoft.KubernetesConfiguration/extensions");
            //options.SetApiVersion(resourceType, "2023-05-01");
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this KubernetesClusterExtensionResource created on azure
            // for more information of creating KubernetesClusterExtensionResource, please refer to the document of KubernetesClusterExtensionResource
            ResourceIdentifier kubernetesClusterExtensionResourceId = KubernetesClusterExtensionResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, clusterRp, clusterResourceName, clusterName, extensionName);
            KubernetesClusterExtensionResource kubernetesClusterExtension = client.GetKubernetesClusterExtensionResource(kubernetesClusterExtensionResourceId);
           
            ArmOperation<KubernetesClusterExtensionResource> lro = await kubernetesClusterExtension.UpdateAsync(WaitUntil.Completed, patch);
            KubernetesClusterExtensionResource result = lro.Value;

            // the variable result is a resource, you could call other operations on this instance as well
            // but just for demo, we get its data from this resource instance
            KubernetesClusterExtensionData resourceData = result.Data;
            // for demo we just print out the id
            return resourceData;
        }
    }
}