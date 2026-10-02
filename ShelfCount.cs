using System;


// Essentially this class just tells amount of items per shelf per aisle
public class ShelfCount : IEquatable<ShelfCount>, IComparable<ShelfCount>
{
    private string _aisle;
    private int _slot;
    private double _valueOnHand;

    // The Getters
    public string Aisle { get { return _aisle; } }
    public int Slot { get { return _slot; } }
    public double ValueOnHand { get { return _valueOnHand; } }

    // Bob the Constructor(??)
    public ShelfCount(string aisle, int slot, double valueOnHand)
    {
        _aisle = aisle;
        _slot = slot;
        _valueOnHand = valueOnHand;
    }

    // BOO-leon like Halloween

    public bool Equals(ShelfCount other)
    {
        if (other == null)
        {
            return false;
        }

        return Aisle == other.Aisle && Slot == other.Slot;
    }

    // Overrides
    public override bool Equals(object obj)
    {
        return Equals(obj as ShelfCount);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Aisle, Slot);
    }

    public override string ToString()
    {
        return string.Format("{0,-6} #{1} {2,8:N2}", Aisle, Slot, ValueOnHand);
    }
    public int CompareTo(ShelfCount other)
    {
        if (other == null)
        {
            return 1;
        }

        int byAisle = string.Compare(Aisle, other.Aisle, StringComparison.Ordinal);

        if (byAisle != 0)
        {
            return byAisle;
         }

        return Slot.CompareTo(other.Slot);
    }
}