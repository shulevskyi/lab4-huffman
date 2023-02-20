namespace lab4_huffman;

// @ref: https://egorikas.com/max-and-min-heap-implementation-with-csharp/
public class MinHeap<T>
{
    private readonly IHeapable[] _elements;
    private int _size;

    public MinHeap(int size)
    {
        _elements = new IHeapable[size];
    }
    
    public int Size => _size;

    private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
    private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
    private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

    private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
    private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
    private bool IsRoot(int elementIndex) => elementIndex == 0;

    private IHeapable GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
    private IHeapable GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
    private IHeapable GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

    private void Swap(int firstIndex, int secondIndex)
    {
        (_elements[firstIndex], _elements[secondIndex]) = (_elements[secondIndex], _elements[firstIndex]);
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public IHeapable Peek()
    {
        if (_size == 0)
            throw new IndexOutOfRangeException();

        return _elements[0];
    }

    public IHeapable Pop()
    {
        if (_size == 0)
            throw new IndexOutOfRangeException();

        var result = _elements[0];
        _elements[0] = _elements[_size - 1];
        _size--;

        ReCalculateDown();

        return result;
    }

    public void Add(IHeapable element)
    {
        if (_size == _elements.Length)
            throw new IndexOutOfRangeException();

        _elements[_size] = element;
        _size++;

        ReCalculateUp();
    }

    private void ReCalculateDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            var smallerIndex = GetLeftChildIndex(index);
            if (HasRightChild(index) && GetRightChild(index).GetNumber < GetLeftChild(index).GetNumber)
            {
                smallerIndex = GetRightChildIndex(index);
            }

            if (_elements[smallerIndex].GetNumber >= _elements[index].GetNumber)
            {
                break;
            }

            Swap(smallerIndex, index);
            index = smallerIndex;
        }
    }

    private void ReCalculateUp()
    {
        var index = _size - 1;
        while (!IsRoot(index) && _elements[index].GetNumber < GetParent(index).GetNumber)
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }
}