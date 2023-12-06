using System.Security.Cryptography;
using System.Text;
using CommandLine;

Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(RunWithOptions)
               .WithNotParsed<Options>(HandleParseError);

static string GenerateSecret(string input)
{
    var bytes = Encoding.UTF8.GetBytes(input);
    var hash = SHA256.HashData(bytes); // Use SHA256.HashData
    return Convert.ToBase64String(hash); // Use standard base64 encoding
}

static void RunWithOptions(Options opts)
{
    string secret = GenerateSecret(opts.Input);

    var txt = File.ReadAllText(opts.TransformTarget);

    var transformedText = txt.Replace(opts.TransformMask, secret);

    File.WriteAllText(opts.TransformTarget, transformedText);
}

static void HandleParseError(IEnumerable<Error> errs)
{
    Console.WriteLine("Error parsing arguments");
}

class Options
{
    [Option('i', "input", Required = true, HelpText = "Input string.")]
    public string Input { get; set; } = string.Empty;

    [Option('t', "transformTarget", Required = true, HelpText = "Path to the transformation target.")]
    public string TransformTarget { get; set; } = string.Empty;

    [Option('m', "transformMask", Required = true, HelpText = "The mask that should be transformed")]
    public string TransformMask {get;set;} = string.Empty;
}