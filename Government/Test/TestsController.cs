using Government.Contracts.Request.Submiting;
using Government.Test.Dtos;
using Government.Test.Serv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Government.Test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController(ITest test ) : ControllerBase
    {
        private readonly ITest test = test;

        [HttpPut("Update Fields/{id}")]
        public async Task<IActionResult> UpdateServiceFields([FromRoute]int id,[FromBody] FieldsTest fieldsTest, CancellationToken cancellationToken)
        {
            var result = await test.UpdateFieldsAsync(id, fieldsTest, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }

        [HttpPut("Update Files/{id}")]
        public async Task<IActionResult> UpdateServiceFiles([FromRoute] int id, [FromForm] FilesTest filesTest, CancellationToken cancellationToken)
        {
            var result = await test.UpdateFilesAsync(id, filesTest, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }


        [HttpPut("Update UserFields/{id}")]
        public async Task<IActionResult> UpdateUserFields([FromRoute] int id, [FromBody] UserFields userFields, CancellationToken cancellationToken)
        {
            var result = await test.UpdateUserFieldsAsync(id, userFields, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }

        [HttpPut("Update UserFiles/{id}")]
        public async Task<IActionResult> UpdateUserFiles([FromRoute] int id, [FromForm] UserFiles userFiles, CancellationToken cancellationToken)
        {
            var result = await test.UpdateUserFilesAsync(id, userFiles, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(statuscode: StatusCodes.Status404NotFound);

        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await test.DownloadServiceFileAsync(id, cancellationToken);

           // var value = result.Value();

            return !result.IsSuccess ?
                    result.ToProblem(statuscode:StatusCodes.Status404NotFound) :
                    File(result.Value()!.fileContent, result.Value()!.contentType, result.Value()!.fileName);

            
        }
    }
}
