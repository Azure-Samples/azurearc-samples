package main

import (
	"context"
	"log"

	"github.com/Azure/azure-sdk-for-go/sdk/azcore/to"
	"github.com/Azure/azure-sdk-for-go/sdk/azidentity"
	"github.com/Azure/azure-sdk-for-go/sdk/resourcemanager/kubernetesconfiguration/armkubernetesconfiguration/v2"
)

func Create_createExtension(ctx context.Context, clientFactory armkubernetesconfiguration.ClientFactory, resourceGroup string, clusterName string, extensionName string, clusterRp string, clusterResourceName string, extDef armkubernetesconfiguration.Extension) armkubernetesconfiguration.Extension {
	poller, err := clientFactory.NewExtensionsClient().BeginCreate(ctx, resourceGroup, clusterRp, clusterResourceName, clusterName, extensionName, extDef , nil)
	if err != nil {
		log.Fatalf("failed to finish the request: %v", err)
	}
	res, err := poller.PollUntilDone(ctx, nil)
	if err != nil {
		log.Fatalf("failed to pull the result: %v", err)
	}
	return res.Extension
}

func Get_getExtension(ctx context.Context, clientFactory armkubernetesconfiguration.ClientFactory, resourceGroup string, clusterName string, extensionName string, clusterRp string, clusterResourceName string) armkubernetesconfiguration.Extension {
	res, err := clientFactory.NewExtensionsClient().Get(ctx, resourceGroup, clusterRp, clusterResourceName, clusterName, extensionName, nil)
	if err != nil {
		log.Fatalf("failed to finish the request: %v", err)
	}
	// You could use response here. We use blank identifier for just demo purposes.
	return res.Extension
}

func Delete_deleteExtension(ctx context.Context, clientFactory armkubernetesconfiguration.ClientFactory, resourceGroup string, clusterName string, extensionName string, clusterRp string, clusterResourceName string) {
	poller, err := clientFactory.NewExtensionsClient().BeginDelete(ctx, resourceGroup, clusterRp, clusterResourceName, clusterName, extensionName, &armkubernetesconfiguration.ExtensionsClientBeginDeleteOptions{ForceDelete: nil})
	if err != nil {
		log.Fatalf("failed to finish the request: %v", err)
	}
	_, err = poller.PollUntilDone(ctx, nil)
	if err != nil {
		log.Fatalf("failed to pull the result: %v", err)
	}
}

func Update_updateExtension(ctx context.Context, clientFactory armkubernetesconfiguration.ClientFactory, resourceGroup string, clusterName string, extensionName string, clusterRp string, clusterResourceName string, patchExtension armkubernetesconfiguration.PatchExtension) armkubernetesconfiguration.Extension {
	poller, err := clientFactory.NewExtensionsClient().BeginUpdate(ctx, resourceGroup, clusterRp, clusterResourceName, clusterName, extensionName, patchExtension, nil)
	if err != nil {
		log.Fatalf("failed to finish the request: %v", err)
	}
	res, err := poller.PollUntilDone(ctx, nil)
	if err != nil {
		log.Fatalf("failed to pull the result: %v", err)
	}
	return res.Extension
}

func List_listExtensions(ctx context.Context, clientFactory armkubernetesconfiguration.ClientFactory, resourceGroup string, clusterName string, clusterRp string, clusterResourceName string) {
	pager := clientFactory.NewExtensionsClient().NewListPager(resourceGroup, clusterRp, clusterResourceName, clusterName, nil)
	for pager.More() {
		page, err := pager.NextPage(ctx)
		if err != nil {
			log.Fatalf("failed to advance page: %v", err)
		}
		for _, ext := range page.Value {
			log.Printf("Listed extension: %s, Type: %s, Provisioning Status: %s, Configuration:\n", *(ext.Name), (*(ext.Properties).ExtensionType), (*(ext.Properties).ProvisioningState))
			for k, v := range (ext.Properties).ConfigurationSettings {
					log.Printf("%s : %s", k, *v)
			}
		}
	}
}

func main(){

	// Set up parameters
	var subscriptionId = "746a51ba-0bd4-497f-89cc-f955a5db3bc8"
	var resourceGroup = "jesseaks"
	var clusterName = "jesseaksee"
	var extensionName = "akvsecretsprovider"
	var clusterRp = "Microsoft.Kubernetes"
	var clusterResourceName = "connectedClusters"

	cred, err := azidentity.NewDefaultAzureCredential(nil)
	if err != nil {
		log.Fatalf("failed to obtain a credential: %v", err)
	}
	ctx := context.Background()
	clientFactory, err := armkubernetesconfiguration.NewClientFactory(subscriptionId, cred, nil)
	if err != nil {
		log.Fatalf("failed to create client: %v", err)
	}

	// Create ext
	var extDef = armkubernetesconfiguration.Extension{
		Properties: &armkubernetesconfiguration.ExtensionProperties{
			AutoUpgradeMinorVersion: to.Ptr(true),
			ConfigurationSettings: map[string]*string{
				"secrets-store-csi-driver.rotationPollInterval": to.Ptr("2m"),
				"secrets-store-csi-driver.syncSecret.enabled": to.Ptr("true"),
				"secrets-store-csi-driver.enableSecretRotation": to.Ptr("true"),
			},
			ExtensionType: to.Ptr("Microsoft.AzureKeyVaultSecretsProvider"),
			ReleaseTrain:  to.Ptr("stable"),
			Scope: &armkubernetesconfiguration.Scope{
				Cluster: &armkubernetesconfiguration.ScopeCluster{
					ReleaseNamespace: to.Ptr("kube-system"),
				},
			},
		},
	}
	var ext = Create_createExtension(ctx, *clientFactory, resourceGroup, clusterName, extensionName, clusterRp, clusterResourceName, extDef)
	log.Printf("Created extension: %s, Type: %s, Provisioning Status: %s, Configuration:\n", *(ext.Name), (*(ext.Properties).ExtensionType), (*(ext.Properties).ProvisioningState))
	for k, v := range (ext.Properties).ConfigurationSettings {
			log.Printf("%s : %s", k, *v)
	}

	// Get ext
	ext = Get_getExtension(ctx, *clientFactory, resourceGroup, clusterName, extensionName, clusterRp, clusterResourceName)
	log.Printf("Get extension: %s, Type: %s, Provisioning Status: %s, Configuration:\n", *(ext.Name), (*(ext.Properties).ExtensionType), (*(ext.Properties).ProvisioningState))
	for k, v := range (ext.Properties).ConfigurationSettings {
			log.Printf("%s : %s", k, *v)
	}

	// Update ext
	var extPatch = armkubernetesconfiguration.PatchExtension{
		Properties: &armkubernetesconfiguration.PatchExtensionProperties{
			ConfigurationSettings: map[string]*string{
				"secrets-store-csi-driver.rotationPollInterval": to.Ptr("7m"),
			},
		},
	}
	ext = Update_updateExtension(ctx, *clientFactory, resourceGroup, clusterName, extensionName, clusterRp, clusterResourceName, extPatch)
	log.Printf("Updated extension: %s, Type: %s, Provisioning Status: %s, Configuration:\n", *(ext.Name), (*(ext.Properties).ExtensionType), (*(ext.Properties).ProvisioningState))
	for k, v := range (ext.Properties).ConfigurationSettings {
			log.Printf("%s : %s", k, *v)
	}

	// List ext
	List_listExtensions(ctx, *clientFactory, resourceGroup, clusterName, clusterRp, clusterResourceName)

	// Delete ext
	Delete_deleteExtension(ctx, *clientFactory, resourceGroup, clusterName, extensionName, clusterRp, clusterResourceName)
	log.Printf("Deleted extension: %s.", extensionName)
}

