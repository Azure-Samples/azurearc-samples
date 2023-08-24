using Azure.ResourceManager.KubernetesConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202;
internal class Program
{
    static void Main(string[] args)
    {
        string subId = "746a51ba-0bd4-497f-89cc-f955a5db3bc8";
        string rgName = "jesseaks";
        string clusterName = "jesseaksee";
        string extensionName = "akvsecretsprovider";
        var helper = new ArcEnabledExtensionHelper(subId, rgName, clusterName, extensionName);

        //// Get extension
        //var result = helper.Get_GetExtension().GetAwaiter().GetResult();
        //Console.WriteLine($"Found extension: {result.Name}, Type: {result.ExtensionType}, Provisioning Status: {result.ProvisioningState}, Configuration: ");
        //result.ConfigurationSettings.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

        // Update extension
        KubernetesClusterExtensionPatch patch = new KubernetesClusterExtensionPatch()
        {
            ConfigurationSettings =
            {
                ["secrets-store-csi-driver.rotationPollInterval"] = "5m"
            }
        };
        var result = helper.Update_UpdateExtension(patch).GetAwaiter().GetResult();
        Console.WriteLine($"Found extension: {result.Name}, Type: {result.ExtensionType}, Configuration: ");
        result.ConfigurationSettings.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

        // Delete extension
        //helper.Delete_DeleteExtension().GetAwaiter().GetResult();
        //Console.WriteLine($"Extension: {result.Name}, Type: {result.ExtensionType} deleted");

        Console.WriteLine("hello");

    }
}

