using System;

namespace RxPosition.Core
{
    public interface IRxPosition
    {
        IObservable<Position> Position { get; } 
    }
}