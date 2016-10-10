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
    public class LocationServicesAccessDenied : AccessViolationException { }
    
}
