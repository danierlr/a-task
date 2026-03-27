namespace FulfillmentTracker.Application.Shared;

public class IndexedHeapBinary<TValue, TPriority> : IIndexedHeap<TValue, TPriority> {
    private readonly record struct HeapEntry(TValue Value, TPriority Priority);

    private readonly IComparer<TPriority> _comparer;

    private readonly List<HeapEntry> _entries = new();

    // null index is separately, because null is not allowed as key in Dictionary
    // Supporting null values, because standart library priority queue supports them as well.
    // TODO? Consider creating dictionary wrapper that accepts null key if needed
    private int? _nullValueIndex = null;

    private readonly Dictionary<TValue, int> _indexByValue = new();

    public IndexedHeapBinary(IComparer<TPriority>? comparer = null) {
        _comparer = comparer ?? Comparer<TPriority>.Default;
    }

    private void FixUp(int index) {
        while (index > 0) {
            int parentIndex = (index - 1) / 2;

            bool parentDominates = _comparer.Compare(_entries[parentIndex].Priority, _entries[index].Priority) <= 0;

            if (parentDominates) {
                break;
            }

            Swap(index, parentIndex);

            index = parentIndex;
        }
    }

    private void FixDown(int index) {
        while (index < Count) {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            int dominantIndex = index;

            if (leftChildIndex < Count) {
                bool childDominates = _comparer.Compare(_entries[leftChildIndex].Priority, _entries[dominantIndex].Priority) <= 0;

                if (childDominates) {
                    dominantIndex = leftChildIndex;
                }
            }

            if (rightChildIndex < Count) {
                bool childDominates = _comparer.Compare(_entries[rightChildIndex].Priority, _entries[dominantIndex].Priority) <= 0;

                if (childDominates) {
                    dominantIndex = rightChildIndex;
                }
            }

            if (dominantIndex == index) {
                break;
            }

            Swap(index, dominantIndex);

            index = dominantIndex;
        }
    }

    private void Swap(int indexA, int indexB) {
        HeapEntry tmpEntry = _entries[indexA];
        _entries[indexA] = _entries[indexB];
        _entries[indexB] = tmpEntry;

        var valueA = _entries[indexA].Value;
        var valueB = _entries[indexB].Value;

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

    public int Count => _entries.Count;

    public TValue Peek() {
        if (Count == 0) {
            throw new InvalidOperationException("No values are present in the queue");
        }

        return _entries[0].Value;
    }

    public bool Contains(TValue value) {
        if (value is null) {
            return _nullValueIndex is not null;
        }

        return _indexByValue.ContainsKey(value);
    }

    public TValue Dequeue() {
        TValue value = Peek();

        Remove(value);

        return value;
    }

    public void Enqueue(TValue value, TPriority priority) {
        if (Contains(value)) {
            throw new InvalidOperationException($"Value {value} is present in the heap already");
        }

        HeapEntry entry = new HeapEntry(value, priority);

        _entries.Add(entry);

        int inserIndex = Count - 1;

        SetIndex(value, inserIndex);

        FixUp(inserIndex);
    }

    public bool Remove(TValue value) {
        if (!Contains(value)) {
            return false;
        }

        int indexRemoved = 0;

        if (value is null) {
            indexRemoved = (int)_nullValueIndex;
        } else {
            indexRemoved = _indexByValue[value];
        }

        int indexLast = Count - 1;

        Swap(indexRemoved, indexLast);

        RemoveIndex(value);
        _entries.RemoveAt(indexLast);

        if (Count > 0 && indexRemoved < Count) {
            FixUp(indexRemoved);
            FixDown(indexRemoved);
        }

        return true;
    }
}
