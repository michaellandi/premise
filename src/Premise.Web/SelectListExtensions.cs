using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Premise.Web
{
    /// <summary>
    /// Extension methods for Select Lists
    /// </summary>
    public static class SelectListExtensions
    {
        /// <summary>
        /// Builds a Select List from an enumerable
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> FromEnum<T>() 
        {
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new SelectListItem() { Text = enu.ToString(), Value = enu.ToString() })).ToList();
        }
    }
}
