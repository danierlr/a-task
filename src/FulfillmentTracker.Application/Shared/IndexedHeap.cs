namespace FulfillmentTracker.Application.Shared;

// Custom implementation, because standart library PriorityQueue does not have a method to remove element in better than O(n) time complexity

public abstract class IndexedHeap<TValue, TPriority> {

    public abstract void Enqueue(TValue value, TPriority priority);

    public abstract TValue Dequeue();

    public abstract void Remove(TValue value);

    public abstract bool Contains(TValue value);

    public abstract int Count { get; } // TODO? support long
}
