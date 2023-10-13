// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Globalization;
using CsvHelper;
using Microsoft.Graph.Connectors.Contracts.Grpc;

namespace PartsInventoryConnector.Data;

public static class CsvDataLoader
{
    public static async Task ReadRecordsFromCsvAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<AppliancePartMap>();
        await csv.ReadAsync();
    }

    public static IEnumerable<CrawlItem> GetCrawlItemsFromCsvAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<AppliancePartMap>();

        var parts = csv.GetRecords<AppliancePart>();
        foreach (var record in parts)
        {
            yield return record.ToCrawlItem();
        }
    }
}
