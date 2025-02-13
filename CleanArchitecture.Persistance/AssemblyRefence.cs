using System.Reflection;

namespace CleanArchitecture.Persistance
{
    public static class AssemblyRefence
    {
        public static readonly Assembly assembly = typeof(Assembly).Assembly;
    }
}
