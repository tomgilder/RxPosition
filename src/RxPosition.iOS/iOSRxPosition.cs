using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using CoreLocation;
using Foundation;
using RxPosition.Core;
using RxPosition.iOS;
using UIKit;

namespace RxPosition
{
    public class iOSRxPosition : IRxPosition
    {
        public IObservable<Position> Position { get; }
        public CLLocationManager LocationManager { get; }

        public iOSRxPosition() : this(CreateDefaultLocationManager)
        {            
        }

        public iOSRxPosition(Func<CLLocationManager> createLocationManager)
        {
            LocationManager = createLocationManager();

            Position = Observable.Create<Position>(Subscribe)
                .Publish()
                .RefCount();
        }

        IDisposable Subscribe(IObserver<Position> observer)
        {
            LocationManager.StartUpdatingLocation();

            return new CompositeDisposable
            (
                LocationManager.ObservableLocation()
                       .Select(l => l.ToRxPosition())
                       .Subscribe(observer),

                LocationManager.ObservableAuthorizationStatus()
                       .Where(status => status.IsDenied())
                       .Select(_ => new LocationServicesAccessDenied())
                       .Subscribe(observer.OnError),

                LocationManager.ObservableAuthorizationStatus()
                       .Where(status => status == CLAuthorizationStatus.NotDetermined)
                       .Subscribe(_ => LocationManager.RequestAuthorization(NSBundle.MainBundle)),

                Disposable.Create(LocationManager.StopUpdatingLocation)
            );
        }

        static CLLocationManager CreateDefaultLocationManager()
        {
            // CLLocationManager must always be created on the main thread
            // otherwise it doesn't get callbacks from its delegate

            return UIApplication.SharedApplication.CreateOnMainThread<CLLocationManager>();
        }
    }
}
