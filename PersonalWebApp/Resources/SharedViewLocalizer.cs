using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PersonalWebApp.Resources
{
    public class SharedViewLocalizer
    {
        private readonly IStringLocalizer localizer;

        public SharedViewLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString this[string key] => localizer[key];

        public LocalizedString GetLocalizedString(string key)
        {
            return localizer[key];
        }
    }
}
