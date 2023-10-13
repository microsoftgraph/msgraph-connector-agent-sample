// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// <AppliancePartSnippet>
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.Graph.Connectors.Contracts.Grpc;
using static Microsoft.Graph.Connectors.Contracts.Grpc.SourcePropertyDefinition.Types;

namespace PartsInventoryConnector.Data;

public class AppliancePart
{
    [Key]
    public int PartNumber { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public int Inventory { get; set; }

    public List<string>? Appliances { get; set; }

    public static DataSourceSchema GetSchema()
    {
        var schema = new DataSourceSchema();

        schema.PropertyList.Add(new SourcePropertyDefinition()
        {
            Name = nameof(PartNumber),
            Type = SourcePropertyType.Int64,
            DefaultSearchAnnotations = (uint)(SearchAnnotations.IsQueryable | SearchAnnotations.IsRetrievable),
            RequiredSearchAnnotations = (uint)(SearchAnnotations.IsQueryable | SearchAnnotations.IsRetrievable),
        });

        schema.PropertyList.Add(new SourcePropertyDefinition()
        {
            Name = nameof(Name),
            Type = SourcePropertyType.String,
            DefaultSearchAnnotations = (uint)(SearchAnnotations.IsSearchable | SearchAnnotations.IsRetrievable),
            RequiredSearchAnnotations = (uint)(SearchAnnotations.IsSearchable | SearchAnnotations.IsRetrievable),
        });

        schema.PropertyList.Add(new SourcePropertyDefinition()
        {
            Name = nameof(Price),
            Type = SourcePropertyType.Double,
            DefaultSearchAnnotations = (uint)SearchAnnotations.IsRetrievable,
            RequiredSearchAnnotations = (uint)SearchAnnotations.IsRetrievable,
        });

        schema.PropertyList.Add(new SourcePropertyDefinition()
        {
            Name = nameof(Inventory),
            Type = SourcePropertyType.Int64,
            DefaultSearchAnnotations = (uint)(SearchAnnotations.IsQueryable | SearchAnnotations.IsRetrievable),
            RequiredSearchAnnotations = (uint)(SearchAnnotations.IsQueryable | SearchAnnotations.IsRetrievable),
        });

        schema.PropertyList.Add(new SourcePropertyDefinition()
        {
            Name = nameof(Appliances),
            Type = SourcePropertyType.StringCollection,
            DefaultSearchAnnotations = (uint)(SearchAnnotations.IsSearchable | SearchAnnotations.IsRetrievable),
            RequiredSearchAnnotations = (uint)(SearchAnnotations.IsSearchable | SearchAnnotations.IsRetrievable),
        });

        schema.PropertyList.Add(new SourcePropertyDefinition()
        {
            Name = nameof(Description),
            Type = SourcePropertyType.String,
            DefaultSearchAnnotations = (uint)(SearchAnnotations.IsSearchable | SearchAnnotations.IsRetrievable),
            RequiredSearchAnnotations = (uint)(SearchAnnotations.IsSearchable | SearchAnnotations.IsRetrievable),
        });

        return schema;
    }

    public CrawlItem ToCrawlItem()
    {
        return new CrawlItem
        {
            ItemType = CrawlItem.Types.ItemType.ContentItem,
            ItemId = PartNumber.ToString(CultureInfo.InvariantCulture),
            ContentItem = new()
            {
                AccessList = GetAccessControlList(),
                PropertyValues = GetSourcePropertyValueMap(),
            },
        };
    }

    private AccessControlList GetAccessControlList()
    {
        var acl = new AccessControlList();
        acl.Entries.Add(new AccessControlEntry()
        {
            AccessType = AccessControlEntry.Types.AclAccessType.Grant,
            Principal = new()
            {
                Type = Principal.Types.PrincipalType.Everyone,
                IdentitySource = Principal.Types.IdentitySource.AzureActiveDirectory,
                IdentityType = Principal.Types.IdentityType.AadId,
                Value = "EVERYONE",
            },
        });

        return acl;
    }

    private SourcePropertyValueMap GetSourcePropertyValueMap()
    {
        var propertyValueMap = new SourcePropertyValueMap();

        propertyValueMap.Values.Add(
            nameof(PartNumber),
            new()
            {
                IntValue = PartNumber,
            });

        propertyValueMap.Values.Add(
            nameof(Name),
            new()
            {
                StringValue = Name,
            });

        propertyValueMap.Values.Add(
            nameof(Description),
            new()
            {
                StringValue = Description,
            });

        propertyValueMap.Values.Add(
            nameof(Price),
            new()
            {
                DoubleValue = Price,
            });

        propertyValueMap.Values.Add(
            nameof(Inventory),
            new()
            {
                IntValue = Inventory,
            });

        var appliances = new StringCollectionType();
        foreach (var appliance in Appliances ?? new())
        {
            appliances.Values.Add(appliance);
        }

        propertyValueMap.Values.Add(
            nameof(Appliances),
            new()
            {
                StringCollectionValue = appliances,
            });

        return propertyValueMap;
    }
}
// </AppliancePartSnippet>
