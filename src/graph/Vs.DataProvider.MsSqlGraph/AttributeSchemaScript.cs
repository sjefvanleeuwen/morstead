using System.Linq;
using System.Reflection;
using System.Text;
using Vs.Graph.Core.Data;
using Vs.Graph.Core.Data.Exceptions;

namespace Vs.DataProvider.MsSqlGraph
{
    public class AttributeSchemaScript : IScriptable<Attributes>
    {
        public string CreateScript(Attributes @object)
        {
            if (@object == null) return string.Empty;
            var sb = new StringBuilder();
            var typesWithMyAttribute =
                from t in Assembly.GetAssembly(typeof(AttributeTypeAttribute)).GetTypes()
                let attributes = t.GetCustomAttributes(typeof(AttributeTypeAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<AttributeTypeAttribute>() };

            foreach (var attribute in @object)
            {
                if (attribute.Type == null)
                {
                    // this is an edge without type definitions.
                    break;
                }

                // Resolve AttributeType from types that inherit from IAttributeType
                switch (((AttributeTypeAttribute)attribute.Type.GetType().GetCustomAttributes(typeof(AttributeTypeAttribute), true)[0]).Name)
                {
                    case "int":
                        sb.Append($"{attribute.Name} INTEGER,");
                        break;
                    case "datum":
                        sb.Append($"{attribute.Name} DATETIME,");
                        break;
                    case "elfproef":
                        sb.Append($"{attribute.Name} VARCHAR(10),");
                        break;
                    case "euro":
                        sb.Append($"{attribute.Name} DECIMAL,");
                        break;
                    case "periode":
                        sb.Append($"{attribute.Name}_begin DATETIME,");
                        sb.Append($"{attribute.Name}_eind  DATETIME,");
                        break;
                    case "text":
                        sb.Append($"{attribute.Name}  NTEXT,");
                        break;
                    default:
                        throw new AttributeNotSupportedException();
                }
            }

            return sb.ToString();
        }
    }
}
