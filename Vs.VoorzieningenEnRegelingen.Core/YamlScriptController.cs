using System;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{

    public class YamlScriptController
    {
        private Model.Model _model;

        public YamlScriptController()
        {
        }

        public Function GetSituation(string formula, string situation)
        {
            return _model.Formulas.First(p=>p.Name== formula).Functions.First(p => p.Situation == situation);
        }

        public Table GetTable(string name)
        {
            return _model.Tables.First(p => p.Name == name);
        }

        public ParseResult Parse(string yaml)
        {
            try
            {
                YamlParser parser = new YamlParser(yaml, null);
                _model = new Model.Model(parser.Formulas(), parser.Tabellen());
            }
            catch (Exception ex)
            {
                var result = new ParseResult();
                result.Message = ex.Message;
                return result;
            }
            return new ParseResult()
            {
                Message = "OK",
                ExpressionTree = new YamlDotNet.Serialization.Serializer().Serialize(_model),
                Model = _model
            };
        }
    }

    public class ParseResult
    {
        public bool IsError = false;
        public string Message;
        public string ExpressionTree;
        public Model.Model Model;
    }
}
