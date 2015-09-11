using System;
using RxPosition.Core;
using CoreLocation;
using System.Reactive.Linq;
using UIKit;
using System.Reactive.Disposables;
using Foundation;
using RxPosition.iOS;

namespace RxPosition
{
    public class LocationServicesAccessDenied : AccessViolationException { }

    public class iOSRxPosition : IRxPosition
    {
        public IObservable<Position> Position { get; }

        public iOSRxPosition() : this(DefaultLocationManagerFactory)
        {            
        }

        public iOSRxPosition(Func<CLLocationManager> locationManagerFactory)
        {
            _locationManagerFactory = locationManagerFactory;

            this.Position = Observable.Create<Position>(Subscribe)
                .Publish()
                .RefCount();
        }

        readonly Func<CLLocationManager> _locationManagerFactory;

        IDisposable Subscribe(IObserver<Position> observer)
        {
            var manager = _locationManagerFactory();
            manager.StartUpdatingLocation();

            return new CompositeDisposable
            (
                manager.ObservableLocation()
                       .Select(l => l.ToRxPosition())
                       .Subscribe(observer),

                manager.ObservableAuthorizationStatus()
                       .Where(status => status.IsDenied())
                       .Select(_ => new LocationServicesAccessDenied())
                       .Subscribe(observer.OnError),

                manager.ObservableAuthorizationStatus()
                       .Where(status => status == CLAuthorizationStatus.NotDetermined)
                    .Subscribe(_ => manager.RequestAuthorization(NSBundle.MainBundle)),

                Disposable.Create(manager.StopUpdatingLocation)
            );
        }

        static CLLocationManager DefaultLocationManagerFactory()
        {
            // CLLocationManager must always be created on the main thread
            // otherwise it doesn't get callbacks from its delegate

            return UIApplication.SharedApplication.CreateOnMainThread<CLLocationManager>();
        }
    }
}
