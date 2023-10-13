// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace PartsInventoryConnector.Data;

public class ApplianceListConverter : DefaultTypeConverter
{
    public override object ConvertFromString(
        string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var appliances = text?.Split(';') ?? new string[0];
        return new List<string>(appliances);
    }
}
