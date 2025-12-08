using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ApplicationTests")]
namespace Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}