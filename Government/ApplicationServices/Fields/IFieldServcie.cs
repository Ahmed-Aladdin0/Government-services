using Government.Contracts.FilesAndFileds;

namespace Government.ApplicationServices.Fields
{
    public interface IFieldServcie
    {
        Task<Result<IEnumerable<UpdateFields>>> GetUserRequestFileds(int RequestId, CancellationToken cancellationToken);
        Task<Result> UpdateUserFieldsAsync(int RequestId, UserFields userFields, CancellationToken cancellationToken = default!);
        Task<Result<IEnumerable<FieldsResponse>>> GetServiceFieldAsync(int serviceId, CancellationToken cancellationToken);
        Task<Result> UpdateFieldsAsync(int ServcieId, FieldsTest fieldsTest, CancellationToken cancellationToken = default!);

   

    }
}
