using System;
using System.Text.RegularExpressions;

namespace Server
{
    public class ApiEndpointUrl
    {
        private readonly string rawUrl;
        private static readonly Regex rawUrlTemplatePattern = new Regex(@"(?<rawUrl>/([\w-]+/)*)(<(?<argType>int|string)>)?$");
        private readonly Regex urlValidationPattern;
        public readonly string[] supportedHttpMethods;
        public string absolutePrefix = "";      // e.g. "http://localhost:8080"
        private static Regex intArgTemplate = new Regex(@"(?<arg>\d{1,9})$");
        private static Regex strArgTemplate = new Regex(@"(?<arg>\w+)$");

        public ApiEndpointUrl(string urlTemplate, params string[] httpMethods)
        {
            Match match = rawUrlTemplatePattern.Match(urlTemplate);
            if (!match.Success)
                throw new ArgumentException("Wrong URL template format");

            supportedHttpMethods = httpMethods;

            rawUrl      = match.Groups["rawUrl"].Value;
            var argType = match.Groups["argType"].Value;

            if (argType.Length == 0)
            {
                urlValidationPattern = new Regex(rawUrl + "$");
                return;
            }

            switch (argType)
            {
                case "int":
                    urlValidationPattern = new Regex(rawUrl + @"\d{1,9}$");
                    break;
                case "string":
                    urlValidationPattern = new Regex(rawUrl + @"\w+$");
                    break;
                default:
                    throw new ArgumentException($"Url argument type {argType} is not supported");
            }
        }

        public string GetAbsoluteUrl() => absolutePrefix + rawUrl;
        public bool ValidateUrl(string rawUrl) => urlValidationPattern.IsMatch(rawUrl);

        public static int? GetIntArgument(string rawUrl)
        {
            int argument;
            if (int.TryParse(intArgTemplate.Match(rawUrl).Groups["arg"].Value, out argument))
                return argument;
            return null;
        }

        public static string GetStringArgument(string rawUrl)
        {
            return strArgTemplate.Match(rawUrl).Groups["arg"].Value;
        }
    }
}
