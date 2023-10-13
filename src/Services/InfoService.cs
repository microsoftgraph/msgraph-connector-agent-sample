// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Grpc.Core;
using Microsoft.Graph.Connectors.Contracts.Grpc;

namespace PartsInventoryConnector.Services;

public class InfoService : ConnectorInfoService.ConnectorInfoServiceBase
{
    private readonly string connectorUniqueId;
    private readonly ILogger logger;

    public InfoService(
        IConfiguration config,
        ILogger<InfoService> logger)
    {
        this.logger = logger;
        connectorUniqueId = config.GetSection("Connector").GetValue<string>("ConnectorUniqueId") ??
            throw new ArgumentException("ConnectorUniqueId not set in appsettings.json");
    }

    public override Task<GetBasicConnectorInfoResponse> GetBasicConnectorInfo(
        GetBasicConnectorInfoRequest request, ServerCallContext context)
    {
        logger.LogInformation("GetBasicConnectorInfo");

        // Return the connector ID
        return Task.FromResult(new GetBasicConnectorInfoResponse
        {
            ConnectorId = connectorUniqueId,
        });
    }

    public override Task<HealthCheckResponse> HealthCheck(
        HealthCheckRequest request, ServerCallContext context)
    {
        logger.LogInformation("HealthCheck");

        // Return a response to signal that service
        // is "healthy" (running and accepting requests)
        return Task.FromResult(new HealthCheckResponse());
    }
}
