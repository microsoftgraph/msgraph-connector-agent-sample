// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Grpc.Core;
using Microsoft.Graph.Connectors.Contracts.Grpc;
using PartsInventoryConnector.Data;

namespace PartsInventoryConnector.Services;

public class CrawlerService : ConnectorCrawlerService.ConnectorCrawlerServiceBase
{
    private readonly ILogger logger;

    public CrawlerService(
        ILogger<CrawlerService> logger)
    {
        this.logger = logger;
    }

    public override async Task GetCrawlStream(
        GetCrawlStreamRequest request,
        IServerStreamWriter<CrawlStreamBit> responseStream,
        ServerCallContext context)
    {
        logger.LogInformation("GetCrawlStream");
        try
        {
            var crawlItems = CsvDataLoader
                .GetCrawlItemsFromCsvAsync(request.AuthenticationData.DatasourceUrl);

            foreach (var crawlItem in crawlItems)
            {
                var crawlStreamBit = GetCrawlStreamBit(crawlItem);
                await responseStream.WriteAsync(crawlStreamBit);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error loading records from datasource");
            var crawlStreamBit = new CrawlStreamBit
            {
                Status = new()
                {
                    Result = OperationResult.DatasourceError,
                    StatusMessage = "Fetching items from datasource failed",
                    RetryInfo = new()
                    {
                        Type = RetryDetails.Types.RetryType.Standard,
                    },
                },
            };

            await responseStream.WriteAsync(crawlStreamBit);
        }
    }

    public override async Task GetIncrementalCrawlStream(
        GetIncrementalCrawlStreamRequest request,
        IServerStreamWriter<IncrementalCrawlStreamBit> responseStream,
        ServerCallContext context)
    {
        logger.LogInformation("GetIncrementalCrawlStream");
        logger.LogInformation($"Requesting items updated since {request.PreviousCrawlStartTimeInUtc}");

        try
        {
            var crawlItems = CsvDataLoader
                .GetCrawlItemsFromCsvAsync(request.AuthenticationData.DatasourceUrl);

            foreach (var crawlItem in crawlItems)
            {
                var crawlStreamBit = GetIncrementalCrawlStreamBit(crawlItem);
                await responseStream.WriteAsync(crawlStreamBit);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error loading records from datasource");
            var crawlStreamBit = new IncrementalCrawlStreamBit
            {
                Status = new()
                {
                    Result = OperationResult.DatasourceError,
                    StatusMessage = "Fetching items from datasource failed",
                    RetryInfo = new()
                    {
                        Type = RetryDetails.Types.RetryType.Standard,
                    },
                },
            };

            await responseStream.WriteAsync(crawlStreamBit);
        }
    }

    private CrawlStreamBit GetCrawlStreamBit(CrawlItem crawlItem)
    {
        return new()
        {
            Status = new()
            {
                Result = OperationResult.Success,
            },
            CrawlItem = crawlItem,
            CrawlProgressMarker = new()
            {
                CustomMarkerData = crawlItem.ItemId,
            },
        };
    }

    private IncrementalCrawlStreamBit GetIncrementalCrawlStreamBit(CrawlItem crawlItem)
    {
        // Create an incremental crawl item
        var incrementalCrawlItem = new IncrementalCrawlItem
        {
            ItemType = IncrementalCrawlItem.Types.ItemType.ContentItem,
            ItemId = crawlItem.ItemId,
            ContentItem = crawlItem.ContentItem,
        };

        return new()
        {
            Status = new()
            {
                Result = OperationResult.Success,
            },
            CrawlItem = incrementalCrawlItem,
            CrawlProgressMarker = new()
            {
                CustomMarkerData = crawlItem.ItemId,
            },
        };
    }
}
