using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("InfrastructureTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}