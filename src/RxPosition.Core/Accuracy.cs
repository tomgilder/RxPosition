namespace RxPosition.Core
{
    public struct Accuracy
    {
        public Accuracy(Distance horitzontal, Distance vertical)
        {
            Horizontal = horitzontal;
            Vertical = vertical;
        }

        public static Accuracy FromMeters(double horitzontal, double vertical)
        {
            return new Accuracy(new Distance(horitzontal), new Distance(vertical));
        }

        public Distance Horizontal { get; }
        public Distance Vertical { get; }
    }
}