# 101 Sample

This sample give a quick start to build docker images and use helm chart to deploy a kubernetes pod using the docker image.

## Getting Started

### Prerequisites


- Visual Studio Code
- "Azure Arc" Extension on Visual Studio Code
- Docker Desktop

### Quickstart

1. Fill in the variables in 101\101chart\values.yaml.
    - Most of the parameters are optional except **repository** under **image** section which should be the Docker repository where to push new Docker image
2. "Ctrl + Shift + P" and select "Arc Extension: Build to Deploy"
    - Use the repository which you enter in values.yaml for pushing Docker image
    - Select a Docker file in Python, Go or Dotnet language
    - Select 101chart for deployment template

## Validation

1. Kubectl get pod
    - a pod with prefix "101chart" should be running in default namespace
2. kubectl logs "podName"
    - output prints integers from 1 to infinite

