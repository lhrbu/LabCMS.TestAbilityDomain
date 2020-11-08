using LabCMS.TestAbilityDomain.Server.Repositories;
using LabCMS.TestAbilityDomain.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LabCMS.TestAbilityDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestFieldsController : ControllerBase
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        { PropertyNamingPolicy = new TestAbilityAntDesignNamingPolicy() };
        private readonly TestFieldsRepository _testFieldsRepository;
        public TestFieldsController(TestFieldsRepository testFieldsRepository)
        { _testFieldsRepository = testFieldsRepository;}

        [HttpGet]
        public IActionResult Get() =>new JsonResult(_testFieldsRepository.TestFields, _jsonSerializerOptions);
        
        
        [HttpPost]
        public async ValueTask PostReloadAsync() => await _testFieldsRepository.ReloadTestFields();
    }
}
