using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contest.Web.Attributes
{
    public class EndDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime endDate = (DateTime) value;

            if (endDate.Year == DateTime.UtcNow.Year || endDate.Year == DateTime.UtcNow.Year + 1)
                return true;

            return false;
        }
    }
}
