using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.Api.Models
{
    public interface IJsonAttribute
    {
        object TryConvert(string modelValue, Type targertType, out bool success);
    }
}
