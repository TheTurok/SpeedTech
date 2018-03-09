using System;

public class CRDEventArgs : EventArgs
{
    public int CardIndex { get; private set; }

    public CRDEventArgs(int cardIndex)
    {
        CardIndex = cardIndex;
    }
}