using System;
using System.IO;

// So I watched a couple of videos on this stuff I hope I was able to do the IO correctly.
// Ill be real and asked AI for assistance in this part 
public class CountLog : IDisposable
{
    private StreamWriter _writer;
    private int _count;
    private bool _isClosed;

    public int Count { get; private set; }

    public CountLog(string path)
    {
        _writer = new StreamWriter(path);
        _writer.WriteLine("LOG OPENED");
        Count = 0;
        _isClosed = false;
    }

    public bool IsClosed
    {
        get { return _isClosed; }
    } 

    public void Write(ShelfCount r)
    {
        if (_isClosed)
        {
            return;
        }

        Count++;
        _writer.WriteLine(string.Format("{0,3} {1}", Count, r));
    }
    public void Dispose()
    {
        if (_isClosed)
        {
            return;
        }

        _writer.WriteLine("LOG CLOSED, " + Count + " lines written");

        _writer.Close();

         _isClosed = true;
    }

}