using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{

    public class YamlScriptController
    {
        private Model.Model _model;
        private readonly CultureInfo _numberCulture = CultureInfo.InvariantCulture;
        private readonly CultureInfo _dateCulture = CultureInfo.InvariantCulture;

        public YamlScriptController()
        {
        }


        public double Lookup(string tableName, string lookupValue, string lookupColumn, string resultColumn)
        {
            var table = GetTable(tableName);
            var columnIndex = (from p in table.ColumnTypes where p.Name == lookupColumn select p.Index).First();
            var resultColumnIndex = (from p in table.ColumnTypes where p.Name == resultColumn select p.Index).First();
            var value = (from p in table.Rows where p.Columns[columnIndex].Value.ToString() == lookupValue select p.Columns[resultColumnIndex]).FirstOrDefault();
            return double.Parse(value.Value.ToString(),_numberCulture);
        }

        public Function GetFormula(string name)
        {
            return _model.Formulas.First(p => p.Name == name).Functions.First();
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

        public void Execute(List<Parameter> parameters)
        {

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
