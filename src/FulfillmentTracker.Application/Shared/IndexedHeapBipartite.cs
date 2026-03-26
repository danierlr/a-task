namespace FulfillmentTracker.Application.Shared;

// Supporting null values, because standart library priority queue supports them as well. // TODO? Consider creating dictionary wrapper that accepts null key if needed

public class IndexedHeapBipartite<TValue, TPriority> : IndexedHeap<TValue, TPriority> {
    private readonly record struct HeapEntry(TValue Value, TPriority Priority);

    private readonly IComparer<TPriority> _comparer;

    private List<HeapEntry> _items = new();
    private int? _nullValueIndex = null; // null is not allowed as key in Dictionary
    private readonly Dictionary<TValue, int> _indexByValue = new();

    public IndexedHeapBipartite(IComparer<TPriority>? comparer) {
        _comparer = comparer ?? Comparer<TPriority>.Default;
    }

    private void FixUp(int index) {
        while (index > 0) {
            int parentIndex = (index - 1) / 2;

            bool parentDominates = _comparer.Compare(_items[parentIndex].Priority, _items[index].Priority) <= 0;

            if (parentDominates) {
                break;
            }

            Swap(index, parentIndex);

            index = parentIndex;
        }
    }

    private void FixDown(int index) {
        while(index < Count) {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            int dominantIndex = index;

            if(leftChildIndex < Count) {
                bool childDominates = _comparer.Compare(_items[leftChildIndex].Priority, _items[index].Priority) <= 0;

                if(childDominates) {
                    dominantIndex = leftChildIndex;
                }
            }

            if (rightChildIndex < Count) {
                bool childDominates = _comparer.Compare(_items[rightChildIndex].Priority, _items[index].Priority) <= 0;

                if (childDominates) {
                    dominantIndex = rightChildIndex;
                }
            }

            if(dominantIndex == index) {
                break;
            }

            Swap(index, dominantIndex);

            index = dominantIndex;
        }
    }

    private void Swap(int indexA, int indexB) {
        HeapEntry tmpEntry = _items[indexA];
        _items[indexA] = _items[indexB];
        _items[indexB] = tmpEntry;

        var valueA = _items[indexA].Value;
        var valueB = _items[indexB].Value;

        //RemoveIndex(valueA);
        //RemoveIndex(valueB);

        SetIndex(valueA, indexA);
        SetIndex(valueB, indexB);
    }

    private void SetIndex(TValue value, int index) {
        if (value is null) {
            _nullValueIndex = index;
        } else {
            _indexByValue[value] = index;
        }
    }

    private void RemoveIndex(TValue value) {
        if (value is null) {
            _nullValueIndex = null;
        } else {
            _indexByValue.Remove(value);
        }
    }

    public override int Count => _items.Count;

    public TValue Peek() {
        throw new NotImplementedException();
    }

    public override bool Contains(TValue value) {
        throw new NotImplementedException();
    }

    public override TValue Dequeue() {
        throw new NotImplementedException();
    }

    public override void Enqueue(TValue value, TPriority priority) {
        throw new NotImplementedException();
    }

    public override void Remove(TValue value) {
        throw new NotImplementedException();
    }
}
