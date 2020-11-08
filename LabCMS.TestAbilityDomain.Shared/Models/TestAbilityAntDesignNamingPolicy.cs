using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LabCMS.TestAbilityDomain.Shared.Models
{
    public class TestAbilityAntDesignNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)=>

            name switch
            {
                "Key"=>"key",
                "Label"=>"title",
                "Children"=>"children",
                _ => name,
            };
        
    }
}
