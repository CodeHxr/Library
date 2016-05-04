using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jImaging
{
    public class ExifGpsLocation
    {
        private readonly ExifFraction[] _latitude;
        private readonly string _latitudeRef;
        private readonly ExifFraction[] _longitude;
        private readonly string _longitudeRef;
        private readonly double _latitudeHour, _latitudeMinute, _latitudeSecond;
        private readonly double _longitudeHour, _longitudeMinute, _longitudeSecond;

        public ExifGpsLocation(ExifReader reader)
        {
            _latitude = (ExifFraction[])reader[ExifTagName.GpsLatitude];
            _latitudeRef = (string) reader[ExifTagName.GpsLatitudeRef];
            _longitude = (ExifFraction[]) reader[ExifTagName.GpsLongitude];
            _longitudeRef = (string) reader[ExifTagName.GpsLongitudeRef];

            if(_latitude == null || _longitude == null || _latitudeRef == null || _longitudeRef == null)
                throw new Exception("Full GPS data not found in image");

            _latitudeHour = _latitude[0].Value();
            _latitudeMinute = _latitude[1].Value();
            _latitudeSecond = _latitude[2].Value();

            _longitudeHour = _longitude[0].Value();
            _longitudeMinute = _longitude[1].Value();
            _longitudeSecond = _longitude[2].Value();
        }

        public double LatitudeDouble => (_latitudeHour + _latitudeMinute/60 + _latitudeSecond/3600) * (_latitudeRef == "S" ? -1 : 1);

        public double LongitudeDouble => (_longitudeHour + _longitudeMinute/60 + _longitudeSecond/3600)*(_longitudeRef == "W" ? -1 : 1);

        public override string ToString()
        {
            var latitude = $"{_latitudeRef} {_latitude[0].Value()}° {_latitude[1].Value()}' {_latitude[2].Value()}\"";
            var longitude = $"{_longitudeRef} {_longitude[0].Value()}° {_longitude[1].Value()}' {_longitude[2].Value()}\"";

            return $"{latitude} \n{longitude}";
        }
    }
}
