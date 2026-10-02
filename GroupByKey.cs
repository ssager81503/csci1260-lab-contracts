using System.Collections.Generic;

// Sorts the aisles and puts values on stuff
public class GroupedByKey : IComparer<ShelfCount>
{

    // Simple change for each shelf
    public int Compare(ShelfCount a, ShelfCount b)
    {
        int byAisle = string.Compare(
            a.Aisle, b.Aisle, StringComparison.Ordinal);

        if (byAisle != 0)
        {
            return byAisle;
        }

        int byValue = b.ValueOnHand.CompareTo(a.ValueOnHand);

        if (byValue != 0)
        {
            return byValue;
        }

        return a.Slot.CompareTo(b.Slot);
    }
}