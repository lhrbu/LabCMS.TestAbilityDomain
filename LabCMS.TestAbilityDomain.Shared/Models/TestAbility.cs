using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LabCMS.TestAbilityDomain.Shared.Models
{
    public class TestAbility
    {
        public string? Key { get; set; }

        public string? Label { get; set; }

        public TestAbility[]? Children { get; set; }
    }
}
