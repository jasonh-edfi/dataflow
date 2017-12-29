# Data Flow

## Overview

Data Flow is a toolset designed to help ease the burden of data migration and utilization in learning environments.  It does so by providing methods to extract information out of spreadsheet-based data (CSV and Excel files), and transform and load to the Ed-Fi Alliance Operational Data Store (ODS) API (see (https://www.ed-fi.org/what-is-ed-fi/ed-fi-technology/)).

Data Flow is a tool set primarily based a .C# NET toolset with a web administration panel in ASP.NET to view data and job status, and server components as .NET command-line applications to process data.  Data Flow also contains a Google Chrome extension prototype to help automate the delivery of data from web sites without SFTP/FTPS export ability.  Data Flow is designed to run on-premise or within cloud hosted environments like Microsoft Azure or Amazon Web Services.  This is designed to match the Ed-Fi ODS and API operating model of choice by education-serving entities.

## Use Cases

* Obtain CSV or Excel files via SFTP, FTPS, web site via Chrome extension or manual upload
* Map source data to Ed-Fi API endpoints for roster and learning data population
* Populate roster and learning data into a Ed-Fi ODS API
* View learning data and job status within ODS browser

## Installation Requirements

* Visual Studio 2015 with .NET 4.5.2 (used to remain consistent with the Ed-Fi ODS API 2.x)
* Microsoft Azure with App Services -or-
* Windows Server 2012+ with IIS 8+ and MS SQL 2012+ either within a Virtual Machine or physical server
* An operating Ed-Fi ODS API 2.x

## Installation (Microsoft Azure)

For education entities that are using Ed-Fi Cloud ODS (see: (https://techdocs.ed-fi.org/display/CLOUD/Cloud+ODS+and+API)), the Microsoft Azure installation path is recommended.

### Installation Steps

#### 1.) Setup a new App Service on Microsoft Azure

Login to Microsoft Azure and create a new Web App + SQL web service.  For SQL Server, a Standard S0 instance (with 10 DTU and 250 GB) is fine for initial deploy and can be scaled up as needed.  Input a SQL Admin Username and Password.

![Azure App Service Setup](https://dl.dropboxusercontent.com/s/6qt5wgxjj1iccza/DataFlow-01.png?dl=1)

![Azure App Service Setup](https://dl.dropboxusercontent.com/s/x980tqc6dcqdlxg/DataFlow-02.png?dl=1)

#### 2.) Setup a Storage Account for temporary files on Microsoft Azure

Data Flow in Azure mode uses a Storage Account to store incoming CSV and Excel files.  Click on Storage Accounts, + Add to create a new storage account.  Fill in the details and click Create.

![Azure Storage Account setup](https://dl.dropboxusercontent.com/s/6vm098rwr8jlel1/DataFlow-03.png?dl=1)

#### 3.) Set the configuration for the Data Flow app

Data Flow in Azure mode will use a common configuration as defined in Application Settings for the App Service.  Please define the following settings in App Settings and Connection Strings:

*App Settings*

| Key | Value |
| --- | --- |
| EncryptionKey | <random string of characters> |
| FileMode | Azure |
| ShareName | <Share name created in step #2> |
| AllowTestCertificates | <true or false depending on environment> |

*Connection Strings*

| Key | Value |
| --- | --- |
| defaultConnection | Data Source=<server>;Initial Catalog=<database>;Persist Security Info=True;User ID=<user>;Password=<password> |
| storageConnection | <string from Azure Storage Account created above> |

![Azure Application Settings](https://dl.dropboxusercontent.com/s/3euorb3je99o9uz/DataFlow-04.png?dl=1)

#### 4.) Download the Data Flow source code

Download the `release` folder/branch and place it into selected location on your machine with Visual Studio 2015.

#### 5.) Publish the Data Flow project to the Microsoft Azure App Service

Right click on the DataFlow.Web project.  Select the Publish option.  Select the instance of the newly created App Service.  Accept defaults for a Release configuration and Publish to Azure.

![Azure Publish](https://dl.dropboxusercontent.com/s/elbc2jcc08riosp/DataFlow-05.png?dl=1)

![Azure Publish](https://dl.dropboxusercontent.com/s/tmudikydwl4yh1d/DataFlow-06.png?dl=1)

Repeat these same steps and Publish as Azure WebJob the DataFlow.Server.FileTransport and DataFlow.Server.TransformLoad projects as well.  Accept defaults for a Release configuration and Publish.

![Azure Publish](https://dl.dropboxusercontent.com/s/dt3f86qov4v3cbi/DataFlow-07.png?dl=1)

#### 6.) Run the application and configure the first user

The first time the application will run will present the “Register” option below the Email and Password link.  Click on this to register your administrative user.

![Configure the first user](https://dl.dropboxusercontent.com/s/k7mu72swqend0wx/DataFlow-08.png?dl=1)

![Configure the first user](https://dl.dropboxusercontent.com/s/5hak7nv7j7cdtew/DataFlow-09.png?dl=1)

#### 7.) Configure the application

Next, configure the application by entering your Ed-Fi ODS API server credentials.  The base URL is the API endpoint in the format of https://<url>/api/v2.0/2017.  Enter the API server key and secret.  The Test API Connection will show if the information is correct.  Below that is customization information if you’d like to add your organization’s branding to the dataflow.

![Configure the application](https://dl.dropboxusercontent.com/s/toa58wdh8ljwj95/DataFlow-10.png?dl=1)

#### 8.) All set!

Once, configured Data Flow should show a browser with schools configured in the API:

![Data browser](https://dl.dropboxusercontent.com/s/vs0wpc448irfpt4/DataFlow-11.png?dl=1)

## Contributing

Community contributions to this application will keep it healthy and active.  We strongly welcome pull requests with new feature enhancements and bug fixes.

## License

This project is licensed under the Apache 2.0 license - see the [LICENSE.md](LICENSE.md) file for details.
