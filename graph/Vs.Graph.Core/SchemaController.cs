using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Vs.Core.Extensions;
using Vs.Graph.Core.Data;
using Vs.Graph.Core.Data.Exceptions;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core
{
    public class SchemaController
    {
        public IGraphSchemaService Service { get; }

        public SchemaController(IGraphSchemaService service)
        {
            Service = service;
        }

        public Assembly Compile(string code)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Itenso.TimePeriod.TimeRange).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IGraphController).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);
                    StringBuilder s1 = new StringBuilder();
                    foreach (Diagnostic diagnostic in failures)
                    {
                        s1.AppendLine($"{ diagnostic.Id}: { diagnostic.GetMessage()}");
                    }
                    throw (new Exception(s1.ToString()));
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(ms.ToArray());
                }
            }
        }
    

        public string CreateCode<T>(NodeSchema schema, string nameSpace) where T : IGraphEntity
        {
            if (typeof(T) == typeof(IGraphEntity))
                throw new Exception("Please specify an interface that inherits from IGraphEntity");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"// -----------------------------------------------------------------
// Automatically generated with Virtual Society Graph Code Generator
// Please do not modify this file
// Generation date: {DateTime.Now}
// -----------------------------------------------------------------");
            sb.AppendLine("using System;");
            sb.AppendLine("using Itenso.TimePeriod;");
            sb.AppendLine("using Vs.Graph.Core.Data;");
            sb.AppendLine($"namespace {nameSpace}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpartial class {schema.Name.ToPascalCase()} : {typeof(T).Name}");
            sb.AppendLine("\t{");
            foreach (var attribute in schema.Attributes)
            {
                // Resolve AttributeType from types that inherit from IAttributeType
                switch (((AttributeTypeAttribute)attribute.Type.GetType().GetCustomAttributes(typeof(AttributeTypeAttribute), true)[0]).Name)
                {
                    case "int":
                        sb.AppendLine($"\t\tpublic int {attribute.Name.ToPascalCase()} {{get;set;}}");
                        break;
                    case "datum":
                        sb.AppendLine($"\t\tpublic DateTime {attribute.Name.ToPascalCase()} {{get;set;}}");
                        break;
                    case "elfproef":
                        sb.AppendLine($"\t\tpublic string {attribute.Name.ToPascalCase()} {{get;set;}}");
                        break;
                    case "euro":
                        sb.AppendLine($"\t\tpublic double {attribute.Name.ToPascalCase()} {{get;set;}}");
                        break;
                    case "periode":
                        sb.AppendLine($"\t\tpublic TimeRange {attribute.Name.ToPascalCase()} {{get;set;}}");
                        break;
                    case "text":
                        sb.AppendLine($"\t\tpublic string {attribute.Name.ToPascalCase()} {{get;set;}}");
                        break;
                    default:
                        throw new AttributeNotSupportedException();
                }
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string Serialize(NodeSchema schema)
        {
            var serializer = new SerializerBuilder().Build();
            var sw = new StringWriter();
            serializer.Serialize(sw, schema);
            return sw.ToString();
        }

        public SchemaSequence SchemaSequence(string yaml)
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            return deserializer.Deserialize<SchemaSequence>(yaml);
        }

        public NodeSchema Deserialize(string yaml)
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            return deserializer.Deserialize<NodeSchema>(yaml);
        }
    }
}
