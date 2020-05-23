using SmartFormat;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vs.Core.Diagnostics;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Helpers;
using Vs.Rules.Core.Model;
using Vs.Rules.Core.Properties;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace Vs.Rules.Core
{
    /// <summary>
    /// Parses YAML's to the internal model for further interpretation.
    /// For the debug info, the original line/col indexes to the Yaml file are preserved so a debugger script editor 
    /// can be implemented on the parsed model during interpretation.
    /// </summary>
    public class YamlRuleParser
    {
        private static ConcurrentDictionary<string, YamlMappingNode> Maps = new ConcurrentDictionary<string, YamlMappingNode>();
        private readonly Dictionary<string, Parameter> _parameters;
        private readonly string _yaml;
        private readonly YamlMappingNode map;

        public YamlRuleParser(string yaml, Dictionary<string, Parameter> parameters)
        {
            _parameters = parameters;
            _yaml = yaml;
            try
            {
                map = Map(_yaml);
            }
            catch (Exception ex)
            {
                throw new FormattingExceptionCollection("Invalid Yaml File", new List<FormattingException>() { new RootFormattingException("Invalid Yaml File",
                        DebugInfo.Default)});
            }

            var rootElements = new List<string>()
            {
                keywords.header, keywords.formulas, keywords.tables, keywords.flow
            };
            // FormattingExceptionCollection formattingExceptions = new FormattingExceptionCollection();
            List<FormattingException> exceptions = new List<FormattingException>();
            foreach (var child in map.Children)
            {
                if (!rootElements.Contains(child.Key.ToString()))
                {
                    exceptions.Add(new RootFormattingException($"Unexpected root property '{child.Key}:'",
                        new DebugInfo().MapDebugInfo(child.Key.Start, child.Key.End)));
                }
            }
            if (exceptions.Any())
            {
                throw new FormattingExceptionCollection("Your Yaml contains multiple exceptions.", exceptions);
            }
        }

        public StuurInformatie Header()
        {
            var stuurinformatie = new StuurInformatie();
            var node = map.Children.Where(p => p.Key.ToString() == keywords.header);
            var notSet = new List<string>()
            {
                keywords.header_subject,keywords.header_organization,keywords.header_type,keywords.header_domain,keywords.header_version,keywords.header_status,keywords.header_period,keywords.header_source
            };
            var fields = string.Join(',', notSet);
            if (node.Count() == 0)
            {
                throw new HeaderFormattingException(
                    Smart.Format(ex_format.header_section_undefined, new {keywords.header}), 
                    new DebugInfo().MapDebugInfo(new Mark(), new Mark()));
            }
            var debugInfo = new DebugInfo().MapDebugInfo(node.ElementAt(0).Key.Start, node.ElementAt(0).Key.End);
            YamlMappingNode seq;
            try
            {
                seq = ((YamlMappingNode)map.Children[new YamlScalarNode(keywords.header)]);
            }
            catch (Exception)
            {
                throw new HeaderFormattingException(
                    Smart.Format(ex_format.header_section_empty, new {keywords.header, fields}), debugInfo);
            }
            foreach (var item in seq.Children)
            {
                var named = typeof(keywords).GetProperties(BindingFlags.NonPublic | BindingFlags.Static)
                    .Where(p => p.Name.StartsWith("header_") && keywords.ResourceManager.GetString(p.Name,keywords.Culture) == item.Key.ToString())
                    .Select(p => p.Name).SingleOrDefault();
                switch (named)
                {
                    case nameof(keywords.header_subject):
                        stuurinformatie.Onderwerp = item.Value.ToString();
                        notSet.Remove(keywords.header_subject);
                        break;
                    case nameof(keywords.header_organization):
                        stuurinformatie.Organisatie = item.Value.ToString();
                        notSet.Remove(keywords.header_organization);
                        break;
                    case nameof(keywords.header_type):
                        stuurinformatie.Type = item.Value.ToString();
                        notSet.Remove(keywords.header_type);
                        break;
                    case nameof(keywords.header_domain):
                        stuurinformatie.Domein = item.Value.ToString();
                        notSet.Remove(keywords.header_domain);
                        break;
                    case nameof(keywords.header_version):
                        stuurinformatie.Versie = item.Value.ToString();
                        notSet.Remove(keywords.header_version);
                        break;
                    case nameof(keywords.header_status):
                        stuurinformatie.Status = item.Value.ToString();
                        notSet.Remove(keywords.header_status);
                        break;
                    case nameof(keywords.header_period):
                        stuurinformatie.Jaar = item.Value.ToString();
                        notSet.Remove(keywords.header_period);
                        break;
                    case nameof(keywords.header_source):
                        stuurinformatie.Bron = item.Value.ToString();
                        notSet.Remove(keywords.header_source);
                        break;
                    default:
                        var field = item.Key.ToString();
                        throw new HeaderFormattingException(
                            Smart.Format(ex_format.header_field_unknown, new {keywords.header, field, fields}), 
                            new DebugInfo().MapDebugInfo(item.Key.Start, item.Key.End));
                }
            }
            // check if all items are set.
            /*
            var check = stuurinformatie.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string) &&
                string.IsNullOrEmpty((string)pi.GetValue(stuurinformatie)))
                .Select(p => p.Name)
            */
            if (notSet.Any())
            {
                fields = string.Join(", ", notSet);
                throw new HeaderFormattingException(
                    Smart.Format(ex_format.header_fields_missing,new {keywords.header, fields}),
                    new DebugInfo().MapDebugInfo(node.ElementAt(0).Key.Start,node.ElementAt(0).Key.End));
            }

            return stuurinformatie;
        }

        public IEnumerable<Step> Flow()
        {
            var steps = new List<Step>();
            int key = 0;
            var node = map.Children.Where(p => p.Key.ToString() == keywords.flow);
            if (node.Count() == 0)
            {
                throw new FlowFormattingException($"'{keywords.flow}:' section is undefined.", new DebugInfo().MapDebugInfo(new Mark(), new Mark()));
            }
            var debugInfo = new DebugInfo().MapDebugInfo(node.ElementAt(0).Key.Start, node.ElementAt(0).Key.End);
            YamlSequenceNode seq;
            try
            {
                seq = (YamlSequenceNode)map.Children[new YamlScalarNode(keywords.flow)];
            }
            catch (Exception)
            {
                throw new FlowFormattingException($"'{keywords.flow}:' section expects at least 1 ' - {keywords.step_}:' sequence", debugInfo);
            }
            foreach (var step in (YamlSequenceNode)map.Children[new YamlScalarNode(keywords.flow)])
            {
                var debugInfoStep = new DebugInfo().MapDebugInfo(step.Start, step.End);
                string stepid = "", description = "", formula = "", value = "", situation = "";
                var @break = null as IBreak;
                var evaluateTables = new List<IEvaluateTable>();
                IEnumerable<IChoice> choices = null;
                foreach (var stepInfo in ((YamlMappingNode)step).Children)
                {
                    var named = typeof(keywords).GetProperties(BindingFlags.NonPublic | BindingFlags.Static)
                        .Where(p => p.Name.StartsWith("step_") && keywords.ResourceManager.GetString(p.Name,keywords.Culture) == stepInfo.Key.ToString())
                        .Select(p => p.Name).SingleOrDefault();
                    switch (named)
                    {
                        case nameof(keywords.step_):
                            stepid = stepInfo.Value.ToString();
                            break;
                        case nameof(keywords.step_description):
                            description = stepInfo.Value.ToString();
                            break;
                        case nameof(keywords.step_formula):
                            formula = stepInfo.Value.ToString();
                            break;
                        case nameof(keywords.step_value):
                            value = stepInfo.Value.ToString();
                            break;
                        case nameof(keywords.step_situation):
                            situation = stepInfo.Value.ToString();
                            break;
                        case nameof(keywords.step_break):
                            @break = new Break() { Expression = stepInfo.Value.ToString() };
                            break;
                        case nameof(keywords.step_choice):
                            choices = GetSituations(stepInfo.Value);
                            break;
                        case nameof(keywords.step_evaluate):
                            evaluateTables.Add(new EvaluateTable() { Name = stepInfo.Value.ToString() } );
                            break;
                        default:
                            throw new StepFormattingException($"unknown property in step definition: '{stepInfo.Key.ToString()}:'",new DebugInfo().MapDebugInfo(stepInfo.Key.Start, stepInfo.Key.End));
                    }
                }
                /* not mandatory 
                if (string.IsNullOrEmpty(description))
                    throw new FlowFormattingException($"'- {Step}: {stepid}' should define a '{StepDescription}:' property", debugInfo);
                */
                if (!string.IsNullOrEmpty(value) && choices != null)
                    throw new StepFormattingException($"Within section '{keywords.flow}:', '- {keywords.step_}: {stepid}' section specifies '{keywords.step_value}:' and '{keywords.step_choice}:' but only 1 can be defined at a time.", debugInfo);
                steps.Add(new Step(debugInfoStep, key++, stepid, description, formula, value, situation, @break, choices, evaluateTables));
            }
            return steps;
        }

        private IEnumerable<IChoice> GetSituations(YamlNode node)
        {
            var result = new List<IChoice>();
            foreach (var choiceInfo in ((YamlSequenceNode)node).Children)
            {
                var choiceInfoItems = ((YamlMappingNode)choiceInfo).Children;
                if (choiceInfoItems.Count != 1)
                {
                    throw new StepFormattingException($"multiple step choice identifiders found; {choiceInfoItems.Count}", new DebugInfo().MapDebugInfo(node.Start,node.End));
                }
                var choiceInfoItem = choiceInfoItems.First();
                var named = typeof(keywords).GetProperties(BindingFlags.NonPublic | BindingFlags.Static)
    .Where(p => p.Name.StartsWith("choice_") && keywords.ResourceManager.GetString(p.Name, keywords.Culture) == choiceInfoItem.Key.ToString())
    .Select(p => p.Name).SingleOrDefault();
                switch (named)
                {
                    case nameof(keywords.choice_situation):
                        result.Add(new Choice() { Situation = choiceInfoItem.Value.ToString() });
                        break;
                    default:
                        throw new StepFormattingException($"unknown step choice identifider {choiceInfoItem.Key.ToString()}",new DebugInfo().MapDebugInfo(choiceInfoItem.Key.Start, choiceInfoItem.Key.End));
                }
            }
            return result;
        }

        public IEnumerable<Table> Tabellen()
        {
            var tables = new List<Table>();
            if (!map.Children.ContainsKey(new YamlScalarNode(keywords.tables)))
                return tables;
            if (((YamlNode)map.Children[new YamlScalarNode(keywords.tables)]).ToString() == string.Empty)
                return tables;
            foreach (var tabel in (YamlSequenceNode)map.Children[new YamlScalarNode(keywords.tables)])
            {
                var debugInfoTable = new DebugInfo().MapDebugInfo(tabel.Start, tabel.End);
                var debugInfo = new DebugInfo(
                    start: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index),
                    end: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index)
                );
                var situations = new List<ISituation>();
                var tableName = ((YamlMappingNode)tabel).ElementAt(0).Value.ToString();
                int j = 1;
                if (((YamlMappingNode)tabel).ElementAt(1).Key.ToString() == keywords.situation)
                {
                    foreach (var situation in ((YamlMappingNode)tabel).ElementAt(1).Value.ToString().Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray())
                    {
                        situations.Add(new Situation(situation));
                    }
                    j = 2;
                }
                var columns1 = ((YamlMappingNode)tabel).ElementAt(j).Key.ToString().Split(',').Select(sValue => sValue.Trim()).ToArray();
                var rows = new List<Row>();
                var columnsDebugInfo = new DebugInfo().MapDebugInfo(((YamlMappingNode)tabel).ElementAt(j).Key.Start, ((YamlMappingNode)tabel).ElementAt(j).Key.End);
                foreach (var row in (YamlSequenceNode)(((YamlMappingNode)tabel).ElementAt(j).Value))
                {
                    var rowdebugInfo = new DebugInfo().MapDebugInfo(row.Start, row.End);
                    var columns = new List<Column>();
                    foreach (var column in (YamlSequenceNode)row)
                    {
                        var info = new DebugInfo().MapDebugInfo(column.Start, column.End);
                        columns.Add(new Column(info, ((YamlScalarNode)column).Value));
                    }
                    rows.Add(new Row(rowdebugInfo, columns));
                }
                var columnTypes = new List<ColumnType>();
                for (int i = 0; i < columns1.Length; i++)
                {
                    columnTypes.Add(new ColumnType(columnsDebugInfo, columns1[i]));
                }
                tables.Add(new Table(debugInfoTable, tableName, columnTypes, rows, situations));
            }
            return tables;
        }

        public static YamlMappingNode Map(string body)
        {
            if (Maps.ContainsKey(body))
                return Maps[body];
            using (var input = new StringReader(body))
            {
                // Load the stream
                var yaml = new YamlStream();
                yaml.Load(input);
                // Examine the stream
                Maps.TryAdd(body, (YamlMappingNode)yaml.Documents[0].RootNode);
                return (YamlMappingNode)yaml.Documents[0].RootNode;
            }
        }

        public IEnumerable<Formula> Formulas()
        {
            var formulas = new List<Formula>();
            YamlSequenceNode formulasNode = null;
            try
            {
                formulasNode = (YamlSequenceNode)map.Children[new YamlScalarNode(keywords.formulas)];
            }
            catch (KeyNotFoundException ex)
            {
                // no formulas specified in yaml.
                return formulas;
            }
            foreach (var functionNode in formulasNode)
            {
                foreach (var row in ((YamlMappingNode)functionNode).Children)
                {
                    if (string.IsNullOrEmpty(row.Value.ToString()))
                    {
                        throw new FlowFormattingException($"'{keywords.formula}:' '- {row.Key}:' section is undefined.", new DebugInfo().MapDebugInfo(row.Key.Start, row.Key.End));
                    }
                    var variableName = row.Key;
                    var functions = new List<Function>();
                    if (row.Value.GetType() == typeof(YamlSequenceNode))
                    {
                        foreach (var situation in ((YamlSequenceNode)row.Value).Children)
                        {
                            var f = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == keywords.formula).Value;
                            var s = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == keywords.situation).Value;
                            var function = new Function(new DebugInfo().MapDebugInfo(f.Start, f.End), s.ToString(), f.ToString().Replace("'", "\""));
                            functions.Add(function);
                        }
                    }
                    else
                    {
                        var f = ((YamlMappingNode)row.Value).Children.FirstOrDefault(p => p.Key.ToString() == keywords.formula).Value;
                        var function = new Function(new DebugInfo().MapDebugInfo(f.Start, f.End), f.ToString().Replace("'", "\""));
                        functions.Add(function);
                    }
                    formulas.Add(new Formula(new DebugInfo().MapDebugInfo(variableName.Start, variableName.End), variableName.ToString(), functions));
                }
            }
            return formulas;
        }
    }
}
