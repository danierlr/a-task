namespace FulfillmentTracker.Application.Dsa;

public class IndexedLinkedList<TValue> {
    private readonly LinkedList<TValue> _list = new();

    // TODO create nullable dictionary maybe
    private readonly Dictionary<TValue, LinkedListNode<TValue>> _dict = new();

    public IndexedLinkedList() {}

    public void AddFirst(TValue entry) {
        if (_dict.ContainsKey(entry)) {
            throw new InvalidOperationException("Can add entry that is present in the collection already");
        }

        var node = _list.AddFirst(entry);

        _dict.Add(entry, node);
    }

    public void AddLast(TValue entry) {
        if (_dict.ContainsKey(entry)) {
            throw new InvalidOperationException("Can add entry that is present in the collection already");
        }

        var node = _list.AddLast(entry);

        _dict.Add(entry, node);
    }

    public TValue RemoveFirst() {
        if(_list.Count == 0) {
            throw new InvalidOperationException("Can not remove entry from empty collention");
        }

        TValue firstValue = _list.First.Value;

        _list.RemoveFirst();
        _dict.Remove(firstValue);

        return firstValue;
    }

    public TValue RemoveLast() {
        if (_list.Count == 0) {
            throw new InvalidOperationException("Can not remove entry from empty collention");
        }

        TValue lastValue = _list.Last.Value;

        _list.RemoveLast();
        _dict.Remove(lastValue);

        return lastValue;
    }

    public TValue PeekFirst() {
        if (_list.Count == 0) {
            throw new InvalidOperationException("Can not peek entry from empty collention");
        }

        return _list.First.Value;
    }

    public TValue PeekLast() {
        if (_list.Count == 0) {
            throw new InvalidOperationException("Can not peek entry from empty collention");
        }

        return _list.Last.Value;
    }

    public bool Remove(TValue entry) {
        LinkedListNode<TValue>? linkedNode = null;

        bool exists = _dict.TryGetValue(entry, out linkedNode);

        if(!exists) {
            return false;
        }

        _dict.Remove(entry);

        _list.Remove(linkedNode); // O(1)

        return true;
    }

    public int Count => _list.Count;
}

