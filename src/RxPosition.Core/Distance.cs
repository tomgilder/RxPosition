using System;

namespace RxPosition
{
    public struct Distance : IEquatable<Distance>, IComparable<Distance>
    {
        static readonly double MetersPerKilometer = 1000.0;
        static readonly double CentimetersPerMeter = 100.0;
        static readonly double CentimetersPerInch = 2.54;
        static readonly double InchesPerFoot = 12.0;
        static readonly double FeetPerYard = 3.0;
        static readonly double FeetPerMeter = CentimetersPerMeter / (CentimetersPerInch * InchesPerFoot);
        static readonly double InchesPerMeter = CentimetersPerMeter / CentimetersPerInch;

        public Distance(double meters)
        {
            TotalMeters = meters;
        }

        public double TotalMeters { get; }
        public double TotalKilometers => TotalMeters / MetersPerKilometer;
        public double TotalCentimeters => TotalMeters * CentimetersPerMeter;
        public double TotalYards => TotalMeters * FeetPerMeter / FeetPerYard;
        public double TotalFeet => TotalMeters * FeetPerMeter;
        public double TotalInches => TotalMeters * InchesPerMeter;

        public static Distance operator +(Distance a, Distance b)
        {
            return new Distance(a.TotalMeters + b.TotalMeters);
        }

        public static Distance operator -(Distance a, Distance b)
        {
            return new Distance(a.TotalMeters - b.TotalMeters);
        }

        public static Distance operator -(Distance a)
        {
            return new Distance(-a.TotalMeters);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Distance))
            {
                return false;
            }

            return Equals((Distance)obj);
        }

        public bool Equals(Distance other)
        {
            return TotalMeters == other.TotalMeters;
        }

        public int CompareTo(Distance other)
        {
            return TotalMeters.CompareTo(other.TotalMeters);
        }

        public override int GetHashCode()
        {
            return TotalMeters.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} Meters", TotalMeters);
        }
    }
}
