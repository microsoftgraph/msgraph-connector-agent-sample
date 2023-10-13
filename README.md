# Microsoft Graph connector agent sample

![License.](https://img.shields.io/badge/license-MIT-green.svg)

Microsoft Graph connectors let you add your own data to the semantic search index and have it power various Microsoft 365 experiences. This .NET application shows you how to use the [Microsoft Graph connector agent](https://learn.microsoft.com/microsoftsearch/graph-connector-agent) and the [Microsoft Graph connectors SDK](https://learn.microsoft.com/graph/custom-connector-sdk-overview) to create a custom connector that indexes items from a sample appliance parts inventory. This connector sample powers experiences such as Microsoft Search, Copilot in Teams, the Microsoft 365 App, and more.

## Prerequisites

- The [.NET SDK](https://dotnet.microsoft.com/download) installed on your Windows development machine (Windows 10 or later, or Windows Server 2016 or later).
- You should have a Microsoft work or school account with the Global administrator role. If you don't have a Microsoft account, you can [sign up for the Microsoft 365 Developer Program](https://developer.microsoft.com/microsoft-365/dev-program) to get a free Microsoft 365 subscription.
- The [Microsoft Graph connector agent](https://learn.microsoft.com/microsoftsearch/graph-connector-agent) installed on your development machine.

## Configure the sample

1. Generate a new GUID to use as the connector ID for your connector. You can use PowerShell to generate a GUID with the following command.

    ```powershell
    [guid]::NewGuid()
    ```

1. Open **appsettings.json** and add the following, replacing `YOUR_CONNECTOR_ID_HERE` with the GUID you generated.

    ```json
    "Connector": {
      "ConnectorUniqueId": "YOUR_CONNECTOR_ID_HERE"
    }
    ```

## Configure the Microsoft Graph connector agent

See []() for steps to configure the agent and test the connector.

## Publish the connector

See []() for steps to publish the connector.

## Code of conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft trademarks or logos is subject to and must follow [Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/legal/intellectualproperty/trademarks/usage/general). Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship. Any use of third-party trademarks or logos are subject to those third-party's policies.
