using System.Reflection;

namespace CleanArchitecture.Presentation
{
    public static class AssemblyReferance
    {
        // 'Assembly' türünü kullanarak mevcut assembly'nin referansını alır
        public static readonly Assembly  assembly = typeof(Assembly).Assembly;
    }
}
