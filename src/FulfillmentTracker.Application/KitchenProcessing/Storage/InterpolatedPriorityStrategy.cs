using FulfillmentTracker.Application.Dsa;
using FulfillmentTracker.Application.Dsa.IndexedHeap;
using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Application.KitchenProcessing.Storage;

public record struct Breakpoint(TimeSpan Time, double Priority);

public record InterpolationConfig {
    public TimeSpan BaseTimeUnit { get; init; }

    public List<Breakpoint> Breakpoints { get; init; }
}

// Before and after first and last breakpoints function is assumed to have their respective priority values as constant (same value towards -infinity and + infinity)
// using decimal instead of double, because time advances to the infinity and we don't want to loose precision at some point in the future

public class InterpolatedPriorityStrategy : IOverflowAdmissionStrategy {
    //InterpolationConfig _config;

    double _initialPriority;
    DateTime _start;

    private List<Bucket> _buckets = new();

    private Dictionary<Order, TrackedOrder> _trackedOrders = new();
    private Dictionary<TrackedOrder, int> _bucketIndexByOrder = new();

    public InterpolatedPriorityStrategy(InterpolationConfig config, DateTime start) {

        _initialPriority = FindEffectiveInitialPriority(config.Breakpoints);
        _start = start;

        //for (int index = 0; index < )
    }

    private double FindEffectiveInitialPriority(List<Breakpoint> breakpoints) {
        int initialPriorityIndex = 0;

        if (breakpoints.Count == 0) {
            throw new ArgumentException("At least one priority breakpoint should be present");
        }

        for (int index = 1; index < breakpoints.Count; index += 1) {
            if (breakpoints[index].Time < breakpoints[index - 1].Time) {
                //? maybe just sort?
                throw new ArgumentException("X in the priority breakpoints list should not be decreasing");
            }
        }

        while (initialPriorityIndex + 2 < breakpoints.Count) {
            if (
                breakpoints[initialPriorityIndex].Time == breakpoints[initialPriorityIndex + 2].Time
                ) {
                initialPriorityIndex += 1;
            } else {
                break;
            }
        }

        return breakpoints[initialPriorityIndex].Priority;
    }

    private void AdvanceTime(DateTime now) {
        int bucketIndex = _buckets.Count - 2; // orders in the last bucket stay there until forgotten by the strategy

        while (bucketIndex >= 0) {
            var currentBucket = _buckets[bucketIndex];
            

            var order = currentBucket.PeekOldest();

            while(order != null) {
                TimeSpan age = now - order.Admitted;

                int nextBucketIndex = bucketIndex;

                // TODO can optimize slightly by going backwards from most forward bucket instead of checking from the current for every bucket
                while (nextBucketIndex < _buckets.Count - 1 && age > _buckets[nextBucketIndex + 1].MinAge) {
                    nextBucketIndex += 1;
                }

                if(bucketIndex == nextBucketIndex) {
                    break;
                }

                currentBucket.Remove(order);
                _buckets[nextBucketIndex].AddNewest(order);
                _bucketIndexByOrder[order] = nextBucketIndex;

                order = currentBucket.PeekOldest();
            }

            bucketIndex -= 1;
        }
    }

    /// <summary>
    /// Start tracking the order by this strategy
    /// </summary>
    public void TrackNew(Order order, DateTime now) {
        AdvanceTime(now);

        TrackedOrder tracked = new TrackedOrder(order, now);

        _trackedOrders.Add(order, tracked);
        _bucketIndexByOrder.Add(tracked, 0);
        _buckets[0].AddNewest(tracked);
    }

    /// <summary>
    /// Forget the tracked order by this strategy
    /// </summary>
    public void Forget(Order order, DateTime now) {
        TrackedOrder tracked = _trackedOrders[order];
        int bucketIndex = _bucketIndexByOrder[tracked];
        Bucket bucket = _buckets[bucketIndex];

        _trackedOrders.Remove(order);
        _bucketIndexByOrder.Remove(tracked);
        bucket.Remove(tracked);
    }

    /// <summary>
    /// Try find the lowest priority order, that could be evicted and has lower priority than newly admitted order.
    /// </summary>
    /// <returns>Lowest priority already existing order. If all existing orders are higher priority than newly admitted one, then returns null</returns>
    public Order? FindOrderToEvict(Order admitted) {
        throw new NotImplementedException();
    }
}
