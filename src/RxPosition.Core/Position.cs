using System;

namespace RxPosition
{
    public class Position
    {
        public Position(Coordinate coordinate, Accuracy accuracy, Distance altitide, DateTimeOffset timestamp)
        {
            Coordinate = coordinate;
            Accuracy = accuracy;
            Altitude = altitide;
            Timestamp = timestamp;
        }
            
        public Coordinate Coordinate { get; }
        public Accuracy Accuracy { get; }
        public Distance Altitude { get; }
        public DateTimeOffset Timestamp { get; }
    }
}