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
            get { return ToString(); }
        }

        public override string ToString()
        {
            return string.Format("'{0},{1}'", _latitude, _longitude); // Always wrapped in single quotes?
        }
    }
}