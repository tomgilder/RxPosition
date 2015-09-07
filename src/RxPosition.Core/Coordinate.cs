using System;

namespace RxPosition.Core
{
    public struct Coordinate
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Coordinate(double latitude, double longitude)
            : this()
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override int GetHashCode()
        {
            unchecked // integer overflows are accepted here
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }

        public static bool operator ==(Coordinate x, Coordinate y)
        {
            return x.Latitude == y.Latitude && x.Longitude == y.Longitude;
        }

        public static bool operator !=(Coordinate x, Coordinate y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            var coord = (Coordinate)obj;
            return Latitude == coord.Latitude && Longitude == coord.Longitude;
        }
    }
}
