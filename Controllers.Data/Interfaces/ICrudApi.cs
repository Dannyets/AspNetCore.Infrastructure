using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Infrastructure.Controllers.Interfaces
{
    public interface ICrudApi<TApiModel>
    {
        Task<ActionResult<IEnumerable<TApiModel>>> Get();

        Task<ActionResult<TApiModel>> Get(int id);

        Task<IActionResult> Post([FromBody] TApiModel value);

        Task<IActionResult> Put(int id, [FromBody] TApiModel value);

        Task<IActionResult> Delete(int id);

    }
}
