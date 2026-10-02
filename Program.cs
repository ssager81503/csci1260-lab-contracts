using System;
using System.Collections.Generic;
using System.IO;

// Ties everything together I think idk it works so I assume that is what it does
public class Program
{

    // Generic interface stuff I guess?? Idk if that is what it is called
    public static void Main()
    {
        Console.WriteLine("=== RIVER CITY SUPPLY ===");
        Console.WriteLine();

        List<ShelfCount> records = new List<ShelfCount>
        {
            new ShelfCount("DAIRY", 3, 21.50),
            new ShelfCount("DRY", 1, 19.00),
            new ShelfCount("DAIRY", 1, 15.00),
            new ShelfCount("DRY", 1, 19.00),
            new ShelfCount("DAIRY", 3, 18.75),
            new ShelfCount("FROZEN", 2, 30.00),
            new ShelfCount("DRY", 4, 12.50)
        };

        Console.WriteLine("Seven records created, in this order:");
        for (int i = 0; i < records.Count; i++)
        {
            Console.WriteLine(
                string.Format(" {0,2} {1}", i + 1, records[i]));
        }

        Console.WriteLine();
        Console.WriteLine("Contract 1: Equals and GetHashCode");

        HashSet<ShelfCount> set = new HashSet<ShelfCount>();
        foreach (ShelfCount record in records)
        {
            set.Add(record);
        }

        // The Shelfstein list
        List<ShelfCount> distinct = new List<ShelfCount>(set);

        PrintResult("Record 1 equals record 5 (same key, new value)?",
            records[0].Equals(records[4]));
        PrintResult("Record 2 equals record 4 (identical)?",
            records[1].Equals(records[3]));
        PrintResult("Record 1 equals record 3?",
            records[0].Equals(records[2]));
        PrintResult("Record 2 and record 4 are the same object?",
            ReferenceEquals(records[1], records[3]));
        PrintResult("Equal records report equal hash codes?",
            records[0].GetHashCode() == records[4].GetHashCode());
        PrintResult("Records created:", records.Count);
        PrintResult("Distinct records in the set:", distinct.Count);

        Console.WriteLine();
        Console.WriteLine("Contract 2: CompareTo, the natural order");
        distinct.Sort();

        foreach (ShelfCount record in distinct)
        {
            Console.WriteLine("  " + record);
        }

        Console.WriteLine();
        Console.WriteLine("Contract 3: a comparer, chosen at the call site");

        Console.WriteLine(" HighestValueFirst");
        distinct.Sort(new HighestValueFirst());

        foreach (ShelfCount record in distinct)
        {
            Console.WriteLine("    " + record);
        }

        Console.WriteLine(" GroupedByKey");
        distinct.Sort(new GroupedByKey());

        foreach (ShelfCount record in distinct)
        {
            Console.WriteLine("    " + record);
        }

        Console.WriteLine();
        Console.WriteLine(
            "Contract 4: cleanup that runs even when the code throws");

        distinct.Sort(new HighestValueFirst());

        CountLog log = null;


        // AI said to do something like this I have never seen a try catch throw thing but I mean it worked
        // Ima try to use this perchance in my project seems interesting I need to learn more about it
        // The 4 hour code bro video on this only lightly touched it
        try
        {
            using (log = new CountLog("count-log.txt"))
            {
                for (int i = 0; i < 3; i++)
                {
                    log.Write(distinct[i]);
                }

                throw new InvalidOperationException(
                    "scanner fault after 3 writes");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(" Caught: " + ex.Message);
        }

        bool safeTwice = true;

        try
        {
            log.Dispose();
        }
        catch
        {
            safeTwice = false;
        }

        PrintResult("The log closed itself?", log.IsClosed);
        PrintResult("Closing it a second time was safe?", safeTwice);
        PrintResult("Lines the log wrote before the fault:", log.Count);

        Console.WriteLine("count-log.txt now says:");

        foreach (string line in File.ReadAllLines("count-log.txt"))
        {
            Console.WriteLine("    " + line);
        }
    }

    private static void PrintResult(string label, object value)
    {
        Console.WriteLine(
            string.Format(" {0,-50}{1,6}", label, value));
    }
}