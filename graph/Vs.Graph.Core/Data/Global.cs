using System.Reflection;

namespace Vs.Graph.Core.Data
{
    public static class Global
    {
        public readonly static string Version = Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
