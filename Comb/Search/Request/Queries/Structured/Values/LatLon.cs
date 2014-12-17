namespace Comb
{
    public class LatLon : IOperand
    {
        readonly double _latitude;
        readonly double _longitude;

        public LatLon(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public double Latitude { get { return _latitude; } }
        public double Longitude { get { return _longitude; } }

        public string Definition
        {
            get { return Utilities.WrapValue(ToString()); }
        }

        public override string ToString()
        {
            return Utilities.LatLonString(_latitude, _longitude); // Not wrapped in single quotes
        }
    }
}