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


       
    }
}
