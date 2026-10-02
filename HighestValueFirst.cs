using System.Collections.Generic;

// Sorts shelf counts by price perferably highest first
public class HighestValueFirst : IComparer<ShelfCount>
{
    public int Compare(ShelfCount a, ShelfCount b)
    {
        int byValue = b.ValueOnHand.CompareTo(a.ValueOnHand);

        // If I fail this class I return to it in spring

        if (byValue != 0)
        {
            return byValue;
        }

        return a.CompareTo(b);
    }
}