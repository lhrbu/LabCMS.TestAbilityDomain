using System;
using Syncfusion.Licensing;
using Syncfusion.XlsIO;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LabCMS.TestAbilityDomain.Shared.Models;
using System.Text.Json;

namespace LabCMS.TestAbilityDomain.ImportHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Register();
            using ExcelEngine excelEngine = new();
            IApplication app = excelEngine.Excel;
            using Stream stream  = File.OpenRead("TestFields/Environment.xlsx");
            IWorkbook workbook = app.Workbooks.Open(stream);
            IWorksheet sheet = workbook.Worksheets[0];
            IRange[] rows = sheet.Rows;
            List<string[]> rowValues = new();
            foreach(var row in rows)
            {
                string[] values = row.Cells.Where(cell=>!string.IsNullOrEmpty(cell.Value))
                    .Select(cell=>cell.Value).ToArray();
                rowValues.Add(values);
            }
            var t = CreateTestAbilityNode(rowValues.ToArray());
            // rows.GroupBy(row=>row.Cells[0].Value);
            // foreach(IRange row in rows)
            // {
            //     TestAbility testAbility = new(){Key=Guid.NewGuid().ToString()};
            //     var cellItr = row.Cells.AsEnumerable().GetEnumerator();
            //     while(cellItr.MoveNext() && !string.IsNullOrEmpty(cellItr.Current.Value))
            //     {
            //         testAbility.Label  = cellItr.Current.Value;
            //     }

            // }
            TestField field = new(){Name="Environment",TestAbilities = t};
            using Stream stream2 = File.OpenWrite("Seed1.json");
            JsonSerializer.SerializeAsync<TestField[]>(stream2,new TestField[] {field},
                new JsonSerializerOptions(){WriteIndented=true,IgnoreNullValues=true}).Wait();
            Console.WriteLine("Hello World!");
        }

        static TestAbility[] CreateTestAbilityNode(string[][] rows)
        {
            var groups = rows.GroupBy(row=>row[0]);
            List<TestAbility> result = new();
            int count = groups.Count();
            
            foreach(var group in groups)
            {
                TestAbility testAbility = new(){
                    Key=Guid.NewGuid().ToString(),
                    Label = group.Key
                    };
                var subRows = group.Where(row=>row.Count()>1).Select(row=>row.Skip(1).ToArray()).ToArray();
                if(subRows.Count()>1){
                    testAbility.Children = CreateTestAbilityNode(subRows);
                }
                result.Add(testAbility);
            }
            return result.ToArray();
            
        }
        static void Register()
        {
            SyncfusionLicenseProvider.RegisterLicense(
                "MzQ4Njg5QDMxMzgyZTMzMmUzMFVKUkRocFV2eTcrMmxtcDg1UlRtclFibFNSMWJLei85VWpUenBvSVV0SFE9"
            );
        }
    }
}
