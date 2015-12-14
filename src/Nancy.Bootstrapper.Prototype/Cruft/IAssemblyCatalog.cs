using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Bootstrapper.Prototype.Cruft
{
    public interface IAssemblyCatalog
    {
        IEnumerable<Assembly> Assemblies { get; }
    }
}