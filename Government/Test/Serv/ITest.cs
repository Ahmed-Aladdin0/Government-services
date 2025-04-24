
using Government.Test.Dtos;

namespace Government.Test.Serv
{
    public interface ITest
    {
        Task<Result> UpdateFieldsAsync(int ServcieId, FieldsTest fieldsTest, CancellationToken cancellationToken = default!);
        Task<Result> UpdateFilesAsync(int serviceId, FilesTest filesTest, CancellationToken cancellationToken = default!);
        Task<Result> UpdateUserFieldsAsync(int RequestId , UserFields userFields, CancellationToken cancellationToken = default!);
        Task<Result> UpdateUserFilesAsync(int RequestId, UserFiles userFiles, CancellationToken cancellationToken = default!);
        Task<Result<DownLoadResponse>> DownloadServiceFileAsync(int FileId , CancellationToken cancellationToken = default!);
        Task<Result<IEnumerable<FileDetails>>> DownloadAttachedFilesAsync(int RequestId, CancellationToken cancellationToken = default!);


    }
}
