using System.IO;
using System.Text;
using Vs.Rules.Core;
using Xunit;

namespace Vs.Morstead.Compiler.Tests
{
    public class CompileTests
    {
        [Fact]
        public void MinimalCompilationTest()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(File.OpenText(@"../../../minimal.yaml").ReadToEnd());
            //var p = parser.Flow();
            StringBuilder i = new StringBuilder(); // grain interface.
            StringBuilder p = new StringBuilder(); // grain persistence.

            var m = result.Model;
            i.AppendLine($"public interface {m.Header.Onderwerp} : IGrainWithStringKey {{");
            p.AppendLine("using System;");
            p.AppendLine($"namespace {m.Header.Organisatie.ToPascalCase()}.{m.Header.Domein.ToPascalCase()}.{m.Header.Type.ToPascalCase()}.{m.Header.Onderwerp.ToPascalCase()} {{");
            p.AppendLine($"[Serializable]public class {m.Header.Onderwerp}_state {{");
            p.AppendLine($"public const string hBron = \"{m.Header.Bron}\";");
            p.AppendLine($"public const string hDomein = \"{m.Header.Domein}\";");
            p.AppendLine($"public const string hPeriode = \"{m.Header.Jaar}\";");
            p.AppendLine($"public const string hOnderwerp = \"{m.Header.Onderwerp}\";");
            p.AppendLine($"public const string hOrganisatie = \"{m.Header.Organisatie}\";");
            p.AppendLine($"public const string hStatus = \"{m.Header.Status}\";");
            p.AppendLine($"public const string hType = \"{m.Header.Type}\";");
            p.AppendLine($"public const string hVersie = \"{m.Header.Versie}\";");
            //p.AppendLine($"public class {m.Header.Onderwerp}_state {{");
            foreach (var step in result.Model.Steps)
            {
                // i.AppendLine($"public interface {m.Header.Onderwerp} : IGrainWithIntegerKey {{");
                if (step.Choices != null)
                {
                    foreach (var choice in step.Choices)
                    {
                        p.AppendLine($"public bool {choice.Situation} {{get;set;}}");
                    }
                }
                if (step.Value != null)
                {
                    p.AppendLine($"public double {step.Value} {{get;set;}}");
                }
            }

            p.AppendLine($"}}"); // end class
            p.AppendLine($"}}"); // end namespace
            var state = p.ToString();
        }
    }
}
