using System.Reflection;

namespace CleanArchitecture.Application;

public static class AssemblyRefence
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
