namespace FulfillmentTracker.Harness;

internal record HarnessArgs(
    string authToken,
    string endpoint,
    string name,
    long seed,
    int rate,
    int minPickDelaySeconds,
    int maxDelaySeconds
);
