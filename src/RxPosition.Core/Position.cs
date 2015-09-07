using System;

namespace RxPosition.Core
{
    public class Position
    {
        public Position(Coordinate coordinate, Accuracy accuracy, Distance altitide, DateTimeOffset timestamp)
        {
            this.Coordinate = coordinate;
            this.Accuracy = accuracy;
            this.Altitude = altitide;
            this.Timestamp = timestamp;
        }
            
        public Coordinate Coordinate { get; }
        public Accuracy Accuracy { get; }
        public Distance Altitude { get; }
        public DateTimeOffset Timestamp { get; }
    }
}