using System;
using System.Linq;
using System.Reactive.Linq;
using CoreLocation;

namespace RxPosition
{
    public static class CLLocationManagerExtentions
    {
        public static IObservable<CLLocation> ObservableLocation(this CLLocationManager manager)
        {
            return Observable.FromEventPattern<CLLocationsUpdatedEventArgs>(
                h => manager.LocationsUpdated += h,
                h => manager.LocationsUpdated -= h)
                    
                    .SelectMany(e => e.EventArgs.Locations.ToObservable());
        }

        public static IObservable<CLAuthorizationStatus> ObservableAuthorizationStatus(this CLLocationManager manager)
        {
            return Observable.FromEventPattern<CLAuthorizationChangedEventArgs>(
                h => manager.AuthorizationChanged += h,
                h => manager.AuthorizationChanged -= h)

                    .Select(e => e.EventArgs.Status)
                    .StartWith(CLLocationManager.Status);
        }
    }
}