using LabCMS.TestAbilityDomain.Shared.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LabCMS.TestAbilityDomain.Server.Repositories
{
    public class TestFieldsRepository
    {
        private readonly IConfiguration _configuration;
        public TestFieldsRepository(IConfiguration configuration)
        { 
            _configuration = configuration;
            ReloadTestFields().Wait();
        }
        
        private string TestFieldsDirectory => Path.Combine(Environment.CurrentDirectory,
            _configuration[nameof(TestFieldsDirectory)]);

        private TestField[] _testFields { get; set; } = Array.Empty<TestField>();
        public IReadOnlyCollection<TestField> TestFields => _testFields;
        public async Task ReloadTestFields()
        {
            string[] configFiles = Directory.GetFiles(TestFieldsDirectory);
            IAsyncEnumerable<TestField> testFields = configFiles.ToAsyncEnumerable().SelectManyAwait(async file =>
            {
                using Stream stream = File.OpenRead(file);
                IEnumerable<TestField> testFieldsInFile = (await 
                    JsonSerializer.DeserializeAsync<IEnumerable<TestField>>(stream))!;
                return testFieldsInFile.ToAsyncEnumerable();
            });

            List<TestField> newTestFields = new(await testFields.CountAsync());
            await foreach(TestField testAbility in testFields)
            { newTestFields.Add(testAbility);}

            _testFields = newTestFields.ToArray();
        }
    }
}
