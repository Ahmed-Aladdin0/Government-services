namespace Government.Contracts.FilesAndFileds
{
    public record UpdateRequest
    (
        int FieldId,
        int FieldDataId,
        string? FieldValueString,
        int? FieldValueInt,
        float? FieldValueFloat,
        DateTime? FieldValueDate

        );
}