using System.Data;

namespace FulfillmentTracker.Harness;

internal class Program {
    /// <summary>
    /// Challenge harness
    /// </summary>
    /// <param name="auth">Authentication token (required)</param>
    /// <param name="endpoint">Problem server endpoint</param>
    /// <param name="name">Problem name. Leave blank (optional)</param>
    /// <param name="seed">Problem seed (random if zero)</param>
    /// <param name="rate">Inverse order rate (in milliseconds)</param>
    /// <param name="min">Minimum pickup time (in seconds)</param>
    /// <param name="max">Maximum pickup time (in seconds)</param>
    static async Task Main(string[] args) {
        // Custom argument read, because DragonFruit is deprecated, not using other library for simplicity
        try {
            Dictionary<string, string> argByKey = new();

            for (int i = 0; i + 1 < args.Length;) {
                if (args[i].StartsWith("--") && !args[i + 1].StartsWith("--")) {
                    string key = args[i].TrimStart('-');
                    string value = args[i + 1];

                    argByKey.Add(key, value);
                    i += 2;
                }
            }

            string ReadArg(string key, string defaultValue) {
                string value = default;
                var hasValue = argByKey.TryGetValue(key, out value);

                if (hasValue) {
                    return value;
                } else {
                    return defaultValue;
                }
            }

            string authToken = ReadArg("auth", ""); // TODO - read from config that is in .gitignore
            string endpoint = ReadArg("endpoint", ""); // TODO - read from config that is in .gitignore
            string name = ReadArg("name", "");
            long seed = long.Parse(ReadArg("seed", "0"));
            int rate = int.Parse(ReadArg("rate", "500")); ;
            int minPickDelaySeconds = int.Parse(ReadArg("min", "4"));
            int maxPickDelaySeconds = int.Parse(ReadArg("max", "8"));

            HarnessArgs harnessArgs = new(
                authToken,
                endpoint,
                name,
                seed,
                rate,
                minPickDelaySeconds,
                maxPickDelaySeconds
            );

            Console.WriteLine($"harnessArgs:{harnessArgs}");
        } catch (Exception exception) {
            Console.WriteLine($"Execution failed: {exception}");
        }

        Console.WriteLine("Start");
    }
}
