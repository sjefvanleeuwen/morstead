using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    /// <summary>
    /// Parses YAML's to the internal model for further interpretation.
    /// For the debug info, the original line/col indexes to the Yaml file are preserved so a debugger script editor 
    /// can be implemented on the parsed model during interpretation.
    /// </summary>
    public class YamlParser
    {
        private const string FormulasAttribute = "formules";
        private const string FormulaAttribute = "formule";
        private const string SituationAttribute = "situatie";
        private const string TablesAttribute = "tabellen";

        private static Dictionary<string, YamlMappingNode> Maps = new Dictionary<string, YamlMappingNode>();
        private static Dictionary<string, Dictionary<string, DataTable>> Tables = new Dictionary<string, Dictionary<string, DataTable>>();
        private readonly Dictionary<string, Parameter> _parameters;
        private readonly string _yaml;
        private readonly YamlMappingNode map;

        public YamlParser(string yaml, Dictionary<string,Parameter> parameters)
        {
            _parameters = parameters;
            _yaml = yaml;
            map = Map(_yaml);
        }

        public static DebugInfo mapDebugInfo(Mark start, Mark end)
        {
            return new DebugInfo(
                start: new LineInfo(line: start.Line, col: start.Column, index: start.Index),
                end: new LineInfo(line: end.Line, col: end.Column, index: end.Index)
            );
        }

        public List<Table> Tabellen()
        {
            var tables = new List<Table>();
            foreach (var tabel in (YamlSequenceNode)map.Children[new YamlScalarNode(TablesAttribute)])
            {
                var debugInfoTable = mapDebugInfo(tabel.Start, tabel.End);
                var debugInfo = new DebugInfo(
                    start: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index),
                    end: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index)
                );
                var tableName = ((YamlMappingNode)tabel).ElementAt(0).Value.ToString();
                var columns1 = ((YamlMappingNode)tabel).ElementAt(1).Key.ToString().Split(',').Select(sValue => sValue.Trim()).ToArray();
                var rows = new List<Row>();
                var columnsDebugInfo = mapDebugInfo(((YamlMappingNode)tabel).ElementAt(1).Key.Start, ((YamlMappingNode)tabel).ElementAt(1).Key.End);
                foreach (var row in (YamlSequenceNode)(((YamlMappingNode)tabel).ElementAt(1).Value))
                {
                    var rowdebugInfo = mapDebugInfo(row.Start, row.End);
                    var columns = new List<Column>();
                    foreach (var column in (YamlSequenceNode)row)
                    {
                        var info = mapDebugInfo(column.Start, column.End);
                        columns.Add(new Column(info, ((YamlScalarNode)column).Value));
                    }
                    rows.Add(new Row(rowdebugInfo, columns));
                }
                var columnTypes = new List<ColumnType>();
                for (int i = 0; i < columns1.Length; i++)
                {
                    columnTypes.Add(new ColumnType(columnsDebugInfo, columns1[i], ColumnType.ColumnTypeEnum.@string));
                }
                tables.Add(new Table(debugInfoTable, tableName, columnTypes, rows));
            }
            return tables;
        }

        private YamlMappingNode Map(string body)
        {
            if (Maps.ContainsKey(body))
                return Maps[body];
            var input = new StringReader(body);
            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);
            // Examine the stream
            Maps.Add(body, (YamlMappingNode)yaml.Documents[0].RootNode);
            return (YamlMappingNode)yaml.Documents[0].RootNode;
        }

        public Dictionary<string, DataTable> Table(string key)
        {
            if (Tables.ContainsKey(key))
                return Tables[key];
            var tables = new Dictionary<string, DataTable>();
            var entries = map.Children;
            foreach (var norm in (YamlSequenceNode)entries[key])
            {
                DataTable dt = new DataTable();
                dt.TableName = key + "_" + ((YamlMappingNode)norm).Children.First().Key.ToString();
                if (((YamlMappingNode)norm).Children.First().Value.GetType() == typeof(YamlMappingNode))
                {
                    //var n = (YamlMappingNode)norm).Children.First().Value;
                    foreach (var situatieColumn in (YamlMappingNode)((YamlMappingNode)norm).Children.First().Value)
                    {
                        DataRow row = dt.NewRow();
                        if (!dt.Columns.Contains(situatieColumn.Key.ToString()))
                            dt.Columns.Add(situatieColumn.Key.ToString());
                        row[situatieColumn.Key.ToString()] = situatieColumn.Value;
                        //row.SetModified();
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                }
                else
                {
                    if (((YamlMappingNode)norm).Children.First().Value.GetType() == typeof(YamlScalarNode))
                    {
                        var node = ((YamlMappingNode)norm).Children.First();
                        DataRow row = dt.NewRow();
                        dt.Columns.Add(node.Key.ToString());
                        row[node.Key.ToString()] = node.Value;
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                    else
                    {
                        foreach (var situatieRow in (YamlSequenceNode)((YamlMappingNode)norm).Children.First().Value)
                        {
                            DataRow row = dt.NewRow();
                            foreach (var situatieColumn in (YamlMappingNode)situatieRow)
                            {
                                if (!dt.Columns.Contains(situatieColumn.Key.ToString()))
                                    dt.Columns.Add(situatieColumn.Key.ToString());
                                row[situatieColumn.Key.ToString()] = situatieColumn.Value;
                            }
                            //row.SetModified();
                            dt.Rows.Add(row);
                            dt.AcceptChanges();
                        }

                    }
                }
                tables.Add(dt.TableName, dt);
            }
            Tables.Add(key, tables);
            return Tables[key];
        }

        public List<Formula> Formulas()
        {
            var funcs = new List<Formula>();
            foreach (var tabel in (YamlSequenceNode)map.Children[new YamlScalarNode(FormulasAttribute)])
            {
                foreach (var row in ((YamlMappingNode)tabel).Children)
                {
                    var variableName = row.Key;
                    List<Function> functions = new List<Function>();
                    if (row.Value.GetType() == typeof(YamlSequenceNode))
                    {
                        foreach (var situation in ((YamlSequenceNode)row.Value).Children)
                        {
                            var f = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                            var s = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == SituationAttribute).Value;
                            var function = new Function(mapDebugInfo(f.Start,f.End), s.ToString(), f.ToString().Replace("'","\""));
                            functions.Add(function);
                        }
                    }
                    else
                    {
                        var f = ((YamlMappingNode)row.Value).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                        var function = new Function(mapDebugInfo(f.Start, f.End), f.ToString().Replace("'", "\""));
                        functions.Add(function);
                    }
                    funcs.Add(new Formula(mapDebugInfo(variableName.Start, variableName.End), variableName.ToString(), functions));
                }
            }
            return funcs;
        }
    }
}
