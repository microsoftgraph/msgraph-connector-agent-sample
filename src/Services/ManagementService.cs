// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Grpc.Core;
using Microsoft.Graph.Connectors.Contracts.Grpc;
using PartsInventoryConnector.Data;

namespace PartsInventoryConnector.Services;

public class ManagementService : ConnectionManagementService.ConnectionManagementServiceBase
{
    private readonly ILogger logger;

    public ManagementService(
        ILogger<ManagementService> logger)
    {
        this.logger = logger;
    }

    // Validates credentials needed to access the datasource.
    public override async Task<ValidateAuthenticationResponse> ValidateAuthentication(
        ValidateAuthenticationRequest request, ServerCallContext context)
    {
        logger.LogInformation("Validating authentication");
        var response = new ValidateAuthenticationResponse();

        // If using authentication, credentials can be found in
        // request.AuthenticationData
        try
        {
            // Since connector is loading a local file as the data source,
            // there isn't any authentication involved. Instead, verify that
            // we can read the file.
            await CsvDataLoader.ReadRecordsFromCsvAsync(request.AuthenticationData.DatasourceUrl);
            response.Status = new() { Result = OperationResult.Success };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error loading CSV file");
            response.Status = new()
            {
                Result = OperationResult.AuthenticationIssue,
                StatusMessage = "Could not read the provided CSV file with the provided credentials",
            };
        }

        return response;
    }

    // Validates any additional parameters required by the connector.
    public override Task<ValidateCustomConfigurationResponse> ValidateCustomConfiguration(
        ValidateCustomConfigurationRequest request, ServerCallContext context)
    {
        logger.LogInformation("Validating custom configuration");
        var response = new ValidateCustomConfigurationResponse();

        // This sample doesn't require any additional parameters.
        // If any are sent, return failure.
        if (string.IsNullOrWhiteSpace(request.CustomConfiguration.Configuration))
        {
            // Valid
            response.Status = new()
            {
                Result = OperationResult.Success,
            };
        }
        else
        {
            // Invalid
            response.Status = new()
            {
                Result = OperationResult.ValidationFailure,
                StatusMessage = "No additional parameters are required for this connector",
            };
        }

        return Task.FromResult(response);
    }

    // Get the schema associated with the data source. The Graph
    // connector agent will use this to define the schema on the connector.
    public override Task<GetDataSourceSchemaResponse> GetDataSourceSchema(
        GetDataSourceSchemaRequest request, ServerCallContext context)
    {
        logger.LogInformation("Getting datasource schema");
        var response = new GetDataSourceSchemaResponse
        {
            DataSourceSchema = AppliancePart.GetSchema(),
            Status = new()
            {
                Result = OperationResult.Success,
            },
        };

        return Task.FromResult(response);
    }
}
