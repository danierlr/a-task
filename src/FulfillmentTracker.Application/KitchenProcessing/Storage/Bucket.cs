using FulfillmentTracker.Application.Dsa;

namespace FulfillmentTracker.Application.KitchenProcessing.Storage;

// When priority value is the same, the priority is decided by waiting time: longer waiting time => bigger priority (as a tie breaker)

public class Bucket {
    public readonly double StartPriority;
    public readonly double EndPriority;

    /// <summary>
    /// Represents time (base time units) since order was tracked, after which the order should end up being in this bucket. End time is MinAge of the next bucket.
    /// </summary>
    public readonly TimeSpan MinAge;

    /// <summary>
    /// Orders stored in the current bucket
    /// </summary>
    private IndexedLinkedList<TrackedOrder> _orders = new(); // newest <=> first; oldest <=> last

    public Bucket(double startPriority, double endPriority, TimeSpan minAge) {
        StartPriority = startPriority;
        EndPriority = endPriority;
        MinAge = minAge;
    }

    public TrackedOrder? PeekLowestPriority() {
        if (Count == 0) {
            return null;
        }

        if (StartPriority <= EndPriority) {
            return _orders.PeekFirst();
        } else {
            return _orders.PeekLast();
        }
    }

    public TrackedOrder? PeekOldest() {
        if (Count == 0) {
            return null;
        }

        return _orders.PeekLast();
    }

    public void Remove(TrackedOrder order) {
        _orders.Remove(order);
    }

    public void AddNewest(TrackedOrder order) {
        _orders.AddFirst(order);
    }

    public int Count => _orders.Count;
}
