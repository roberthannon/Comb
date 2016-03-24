namespace Comb
{
    public class LatLon : IOperand
    {
        public LatLon(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }

        public string Definition => Utilities.WrapValue(ToString());

        public override string ToString()
        {
            return Utilities.LatLonString(Latitude, Longitude); // Not wrapped in single quotes
        }
    }
}