// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// <AppliancePartMapSnippet>
using CsvHelper.Configuration;

namespace PartsInventoryConnector.Data;

public class AppliancePartMap : ClassMap<AppliancePart>
{
    public AppliancePartMap()
    {
        Map(m => m.PartNumber);
        Map(m => m.Name);
        Map(m => m.Description);
        Map(m => m.Price);
        Map(m => m.Inventory);
        Map(m => m.Appliances).TypeConverter<ApplianceListConverter>();
    }
}
// </AppliancePartMapSnippet>
