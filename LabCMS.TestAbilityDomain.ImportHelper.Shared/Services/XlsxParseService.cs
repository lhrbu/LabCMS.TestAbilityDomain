using Syncfusion.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabCMS.TestAbilityDomain.Shared.Models;
using System.IO;
using Syncfusion.XlsIO;
using System.Text.Json;

namespace LabCMS.TestAbilityDomain.ImportHelper.Shared.Services
{
    public class XlsxParseService
    {
        static XlsxParseService()=> SyncfusionLicenseProvider
            .RegisterLicense( "MzQ4Njg5QDMxMzgyZTMzMmUzMFVKUkRocFV2eTcrMmxtcDg1UlRtclFibFNSMWJLei85VWpUenBvSVV0SFE9");
   
        private TestAbility[] CreateTestAbilityNode(IEnumerable<string[]> rowsValue) =>
            rowsValue.GroupBy(row => row[0]).Select(group =>
            {
                TestAbility testAbility = new()
                {
                    Key = Guid.NewGuid().ToString(),
                    Label = group.Key
                };
                IEnumerable<string[]> subRowsValue = group.Where(row => row.Count() > 1).Select(row => row.Skip(1).ToArray());
                if (subRowsValue.Count() > 0) { testAbility.Children = CreateTestAbilityNode(subRowsValue); }
                return testAbility;
            }).ToArray();

        public async ValueTask ParseImportXlsxAsync(string xlsxFile, string outputJson)
        {
            using Stream stream = File.OpenRead(xlsxFile);
            using ExcelEngine excelEngine = new();
            IApplication app = excelEngine.Excel;

            


            IWorkbook workbook = app.Workbooks.Open(stream);

            IEnumerable<TestField> testFields = workbook.Worksheets.Select(worksheet =>
            {
                IRange[] rows = worksheet.Rows;
                IEnumerable<string[]> rowsValue = rows.Select(row => row.Cells.Where(cell => !string.IsNullOrEmpty(cell.Value)
                ).Select(cell => cell.Value).ToArray());
                TestAbility[] testAbilities = CreateTestAbilityNode(rowsValue);
                return new TestField()
                {
                    Name = worksheet.Name,
                    TestAbilities = testAbilities
                };
            });

            if (File.Exists(outputJson)){File.Delete(outputJson);}
            using Stream outputStream = File.OpenWrite(outputJson);
            await JsonSerializer.SerializeAsync(outputStream, testFields,
                new JsonSerializerOptions() { IgnoreNullValues = true });
        }
    }
}
