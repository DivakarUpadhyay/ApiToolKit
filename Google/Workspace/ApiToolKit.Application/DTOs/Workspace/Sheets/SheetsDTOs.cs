using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Sheets;

public class SpreadsheetPropertiesDto
{
    public string? Title { get; set; }
    public string? Locale { get; set; }
    public string? AutoRecalc { get; set; }
    public string? TimeZone { get; set; }
    public object? DefaultFormat { get; set; }
}

public class GridProperties
{
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }
    public int FrozenRowCount { get; set; }
    public int FrozenColumnCount { get; set; }
}

public class SheetPropertiesDto
{
    public int SheetId { get; set; }
    public string? Title { get; set; }
    public int Index { get; set; }
    public string? SheetType { get; set; }
    public GridProperties? GridProperties { get; set; }
    public bool Hidden { get; set; }
    public object? TabColor { get; set; }
}

public class SpreadsheetResponse
{
    public string? SpreadsheetId { get; set; }
    public SpreadsheetPropertiesDto? Properties { get; set; }
    public List<SheetPropertiesDto>? Sheets { get; set; }
    public string? SpreadsheetUrl { get; set; }
    public object? NamedRanges { get; set; }
}

public class SheetDefinition
{
    public string? Title { get; set; }
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }
}

public class CreateSpreadsheetRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public List<SheetDefinition>? Sheets { get; set; }
}

public class GetSpreadsheetRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;
    public bool IncludeGridData { get; set; } = false;
    public List<string>? Ranges { get; set; }
}

public class ValuesResponse
{
    public string? Range { get; set; }
    public string? MajorDimension { get; set; }
    public List<List<object>>? Values { get; set; }
}

public class GetValuesRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public string Range { get; set; } = string.Empty;
    public string MajorDimension { get; set; } = "ROWS";
    public string ValueRenderOption { get; set; } = "FORMATTED_VALUE";
    public string DateTimeRenderOption { get; set; } = "SERIAL_NUMBER";
}

public class UpdateValuesRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public string Range { get; set; } = string.Empty;

    [Required]
    public List<List<object>> Values { get; set; } = new();
    public string ValueInputOption { get; set; } = "USER_ENTERED";
    public string MajorDimension { get; set; } = "ROWS";
}

public class UpdateValuesResponse
{
    public string? SpreadsheetId { get; set; }
    public string? UpdatedRange { get; set; }
    public int UpdatedRows { get; set; }
    public int UpdatedColumns { get; set; }
    public int UpdatedCells { get; set; }
}

public class AppendValuesRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public string Range { get; set; } = string.Empty;

    [Required]
    public List<List<object>> Values { get; set; } = new();
    public string ValueInputOption { get; set; } = "USER_ENTERED";
    public string InsertDataOption { get; set; } = "INSERT_ROWS";
}

public class ClearValuesRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public string Range { get; set; } = string.Empty;
}

public class BatchGetValuesRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public List<string> Ranges { get; set; } = new();
    public string MajorDimension { get; set; } = "ROWS";
}

public class BatchGetValuesResponse
{
    public string? SpreadsheetId { get; set; }
    public List<ValuesResponse>? ValueRanges { get; set; }
}

public class ValueRangeData
{
    public string? Range { get; set; }
    public List<List<object>>? Values { get; set; }
}

public class BatchUpdateValuesRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public List<ValueRangeData> Data { get; set; } = new();
    public string ValueInputOption { get; set; } = "USER_ENTERED";
}

public class AddSheetRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;
}

public class DeleteSheetRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public int SheetId { get; set; }
}

public class RenameSheetRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public int SheetId { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
}

public class CopyToRequest
{
    [Required]
    public string SpreadsheetId { get; set; } = string.Empty;

    [Required]
    public int SheetId { get; set; }

    [Required]
    public string DestinationSpreadsheetId { get; set; } = string.Empty;
}
