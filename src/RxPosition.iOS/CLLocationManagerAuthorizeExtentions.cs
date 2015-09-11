using System;
using CoreLocation;
using System.Linq;
using System.Reactive.Linq;
using Foundation;
using UIKit;

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