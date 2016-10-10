using System;

namespace RxPosition
{
    public interface IRxPosition
    {
        IObservable<Position> Position { get; } 
    }
}