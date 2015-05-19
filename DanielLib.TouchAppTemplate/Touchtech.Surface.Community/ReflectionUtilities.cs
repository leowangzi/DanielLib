using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using Microsoft.Surface;

namespace Touchtech.Surface
{
    internal class ReflectionUtilities
    {
        internal static string ConstructAssemblyQualifiedName(string assemblyName, string typeFullName)
        {
            AssemblyName name = typeof(SurfaceEnvironment).Assembly.GetName();
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, Version={2}, Culture=neutral, PublicKeyToken={3}", new object[] { typeFullName, assemblyName, name.Version, BitConverter.ToString(name.GetPublicKeyToken()).Replace("-", "").ToUpperInvariant() });
        }

    }
}
