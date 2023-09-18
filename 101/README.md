# 101 Sample
This sample guides you through the full development flow. Follow this README to get familiar with the processes to setup your dev environment and build/deploy/validate sample codes using the Azure Arc VS code extension or command line options.

## Recommended Dev Environment Setup
- Visual Studio Code with [Azure Arc Extension](https://marketplace.visualstudio.com/search?term=azure%20arc&target=VSCode&category=All%20categories&sortBy=Relevance) from VS code marketplace installed to simplify some development steps
- Docker desktop or other containerization engine
    - Please make sure the docker desktop is running in Linux Container. If not, please right click the Docker Icon and select switch to Linux containers.
    ![SwichToLinux](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/101/screenshots/SwitchToLinux.png)
- A Kubernetes cluster. If you don't already have one, you can use the Azure Arc VS code extension to create an AKS EE cluster.
- [Helm](https://helm.sh/docs/intro/install/) installed on your dev machine.

## Building
The same sample is implemented in 3 languages, build the corresponding Dockerfile to deploy to your K8S cluster.

1. Make sure you are logged in to a container registry ready to use. If you're using docker desktop, please make sure you've logged in to your account either through the app UI or with docker commands and specify the password when prompted:
    ```powershell
    docker login docker.io/<your username> -u <your username>
    ```

2. Determine your image repository for the build process. If you're using docker.io as your container registry, the image fullname should be in this format:
    ```powershell
    docker.io/<your username>/<your repo name>
    ```

    for example:

    ```powershell
    docker.io/johndoe/101sample
    ```

3. Build your docker image and push to a container registry
    - If the Azure Arc VS code extension is installed, you can:
        - Right click on the Dockerfile you selected and click on "Arc Extension: Build docker images" and specify your image full name. Or
        - In VS Code, press "F1" or "Ctrl+Shift+P" to bring up the command palette and select "Arc Extension: Build docker images", specify your image full name and select the Dockerfile to build from the menu.
        ![CommandPalette](https://raw.githubusercontent.com/Azure-Samples/azurearc-samples/main/101/screenshots/CommandPalette.png)
    - If you prefer the command line option, use the following commands:
    ```powershell
    docker build -t <image fullname>:<tag> -f <dockerfile path> <source code path>
    docker push <image fullname>:<tag>
    ```

    for example:
    ```powershell
    docker build -t docker.io/johndoe/101sample:latest -f .\101\dotnet\src\Dockerfile .\101\dotnet\src
    docker push docker.io/johndoe/101sample:latest
    ```

## Deploying
Once you have the image ready, you are ready to deploy the image to your K8S cluster.
1. Make sure your K8S cluster to have sufficient privileges to pull images from it, e.g., you might need to provide imagePullSecrets in the values.yaml of the helm chart, or configure your container registry to allow anonymous pull.
2. Fill out the variables in 101\101chart\values.yaml. The only mandatory field is the **fullImageName** property under the **image** section, which should be the full image name with tags from the build step, e.g., docker.io/johndoe/101sample:latest
3. Deploy the helm chart.
    - With the Azure Arc VS code extension, Press "F1" or "Ctrl+Shift+P" to bring up the command palette and select "Arc Extension: Install helm charts", and then select the chart of the 101 sample.
    - If you prefer the command line option, use the following commands:
    ```powershell
    helm install <chartDir path> --generate-name
    ```

    for example:
    ```powershell
    helm install .\101\101chart --generate-name
    ```

## Validation
Make sure the pods are running in the default namespace of your K8S cluster.

A pod name with prefix "101chart" should be running in default namespace. Check your pod status with:
```powershell
kubectl get pods
```

Inspect container logs with:
```powershell
kubectl logs <podName>
```
For example:

```powershell
kubectl logs 101chart-1691659165-7d44cb5994-bvkn4
```

You should see an incrementing counter printout.

## Cleanup
Uninstall the chart 

```bash
# Find the installed chart name
helm ls

# Uninstall the chart
helm uninstall <char name>
```
