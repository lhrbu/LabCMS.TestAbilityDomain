using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.TestAbilityDomain.Shared.Models
{
    public class TestField
    {
        public string? Name { get; set; }
        public TestAbility[] TestAbilities { get; set; } = Array.Empty<TestAbility>();
    }
}
