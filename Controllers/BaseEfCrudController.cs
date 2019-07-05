using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models;
using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Infrastructure.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BaseEfCrudController<TApiModel, TDbModel> : ControllerBase where TDbModel : DbIdEntity
                                                             
    {
        private readonly IEfRepository<TDbModel> _repository;
        protected readonly IMapper _mapper;

        public BaseEfCrudController(IEfRepository<TDbModel> repository,
                                    IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TApiModel>>> Get()
        {
            var dbModels = await _repository.GetAll();

            var apiModels = dbModels.Select(u => _mapper.Map<TApiModel>(u));

            return Ok(apiModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TApiModel>> Get(int id)
        {
            var dbModel = await _repository.GetById(id);

            var apiModel = _mapper.Map<TApiModel>(dbModel);

            return Ok(apiModel);
        }

        [HttpPost]
        public async Task<ActionResult<TApiModel>> Post([FromBody] TApiModel apiModel)
        {
            var dbEntity = _mapper.Map<TDbModel>(apiModel);

            dbEntity = await _repository.Add(dbEntity);

            apiModel = _mapper.Map<TApiModel>(dbEntity);

            return Ok(apiModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TApiModel apiModel)
        {
            var dbEntity = _mapper.Map<TDbModel>(apiModel);

            await _repository.Update(dbEntity);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return Ok();
        }
    }
}
