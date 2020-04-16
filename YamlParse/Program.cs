using CommandLine;
using System;

namespace YamlParse
{
    class OptionsMutuallyExclusive
    {
        //define commands in set(group) named web
        [Option('c',"content",Required=true, SetName = "mode",HelpText="For parsing content yaml files")]
        public bool UseContentParser { get; set; }
        [Option('r', "rule", Required = true, SetName = "mode", HelpText = "For parsing rule yaml files")]
        public bool UseRuleParser { get; set; }

        //Common: the next option can be used with any set because it's not included in a set
        [Option('f', "path", Required = true, HelpText = "Specify Yaml File")]
        public string File { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Verbose)
                       {
                           Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                           Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                       }
                       else
                       {
                           Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                           Console.WriteLine("Quick Start Example!");
                       }
                   });

        }
    }
}
