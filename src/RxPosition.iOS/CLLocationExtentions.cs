using System;
using CoreLocation;
using RxPosition.Core;

namespace RxPosition
{
    static class CLLocationExtentions
    {
        public static Position ToRxPosition(this CLLocation location)
        {
            return new Position(
                coordinate: location.Coordinate.ToRxCoordinate(),
                accuracy:   Accuracy.FromMeters(location.HorizontalAccuracy, location.VerticalAccuracy),
                altitide:   new Distance(location.Altitude),
                timestamp:  (DateTime)location.Timestamp
            );
        }

        public static Coordinate ToRxCoordinate(this CLLocationCoordinate2D coordinate)
        {
            return new Coordinate(coordinate.Latitude, coordinate.Longitude);
        }

        public static bool IsDenied(this CLAuthorizationStatus status)
        {
            return status == CLAuthorizationStatus.Denied || status == CLAuthorizationStatus.Restricted;
        }
    }
}