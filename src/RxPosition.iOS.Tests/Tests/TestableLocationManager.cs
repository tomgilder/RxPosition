using CoreLocation;
using UIKit;

namespace RxPosition.iOS.Tests
{
    public class TestableLocationManager : CLLocationManager
    {
        public static TestableLocationManager Create()
        {
            TestableLocationManager manager = null;

            UIApplication.SharedApplication.InvokeOnMainThread(
                () => manager = new TestableLocationManager()
            );

            return manager;
        }

        TestableLocationManager() { }

        public bool IsUpdating { get; private set; }

        public override void StartUpdatingLocation()
        {
            IsUpdating = true;    
            base.StartUpdatingLocation();
        }

        public override void StopUpdatingLocation()
        {
            IsUpdating = false;
            base.StopUpdatingLocation();
        }
    }
}
