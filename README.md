# Smile Direct Club Coding Test

An example .NET core 2.0 app for the smile direct club coding test

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites
To run the app without tests you will need a linux, windows, or mac machine with the latest version of docker installed. 

To run the tests for this project, you will need the dotnet core 2.0 SDK installed. https://www.microsoft.com/net/download/macos

### Installing
In the directory of this readmefile (root directory of repo) run the following docker command. 
1.) docker build -t skynet.codingtest.api  -f Api/Dockerfile .
Then to run the app do : 
2.) docker run -d -p 8080:80 --name nameyouwant skynet.codingtest.api
You can replace 8080 with another free port and 'nameyouwant' with a name you would like for the image 


## Running the tests
On a linux, windows, or mac machine with the dotnet core 2.0 sdk installed.
1.) Navigate to the Tests folder under SmileDirectClub/CodingTest/Tests
2.) Run the command 'dotnet Test SmileDirectClub.CodingTest.Tests.csproj' 


## Deployment
This application is designed to run on docker which should allow you to easily scale it out on the cloud
with the solution of your choice. This could be Amazon ECS or Kubernetes. 

## Other Notes
If you did decide to replace the API with a database, you would only need to implement the ILaunchPadRepository interface and update the startup CS to use your new
database implementation. Logs are put into the application's console. The configuration for the launchpad URL is in appsettings.json. The tests have 1 version of this
and the API have another version. You could also modify the docker file using environment variables to configure this value. Another solution you might consider for 
more senstitive configuration values is Amazon SSM, or Vault. 

