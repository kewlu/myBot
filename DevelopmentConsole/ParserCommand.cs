using System;
using System.Text.RegularExpressions;

namespace DevelopmentConsole
{
    class ParserCommand
    {
        static public string[] Parse(string lineToParse, int maxArguments)
        {
            if (String.IsNullOrWhiteSpace(lineToParse))
                return new string[1];

            Regex regex = new Regex("(\"[^\"]+\"|[^\\s\"]+)");
            MatchCollection args = regex.Matches(lineToParse);

            var arguments = new string[maxArguments];

            for (int i = 0; i < maxArguments && i < args.Count; i++)
                arguments[i] = args[i].Value.Replace("\"", String.Empty);
            return arguments;
        }

    }
}

