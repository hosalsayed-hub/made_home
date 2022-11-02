﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Homemade.Api.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FromJsonAttribute : Attribute, IJsonAttribute
    {
        public object TryConvert(string modelValue, Type targetType, out bool success)
        {
            var value = JsonConvert.DeserializeObject(modelValue, targetType);
            success = value != null;
            return value;
        }
    }
}
