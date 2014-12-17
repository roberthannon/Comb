//using System;

//namespace Comb
//{
//    /// <summary>
//    /// TODO This is a function that can be used in expressions (for sorting, or returning). It essentially calculates the distance of the indexed field to the specified location.
//    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/searching-locations.html
//    /// </summary>
//    public class Haversin
//    {
//        public Haversin(LatLon location, IField field)
//        {
//            if (location == null) throw new ArgumentNullException("location");
//            if (field == null) throw new ArgumentNullException("field");

//            var definition = string.Format("haversin({0},{1},{2}.latitude,{2}.longitude)", location.Latitude, location.Longitude, field.Name);
//        }
//    }
//}