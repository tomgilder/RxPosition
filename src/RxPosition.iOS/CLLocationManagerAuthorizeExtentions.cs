using CoreLocation;
using Foundation;

namespace RxPosition.iOS
{
    public static class CLLocationManagerAuthorizeExtentions
    {
        public static void RequestAuthorization(this CLLocationManager manager, NSBundle bundle)
        {
            if (bundle.RequiresBackgroundLocation())
            {
                bundle.ThrowIfNoBackgroundDescription();
                manager.RequestAlwaysAuthorization();
            }
            else
            {
                bundle.ThrowIfNoInUseDescription();
                manager.RequestWhenInUseAuthorization();
            }
        }
    }
}