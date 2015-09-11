using System;
using System.Linq;
using Foundation;
using UIKit;

namespace RxPosition.iOS
{
    static class NSBundleExtentions
    {
        public static bool RequiresBackgroundLocation(this NSBundle bundle)
        {
            return GetBackgroundModes(bundle).Any(m => m == "location");
        }

        static string[] GetBackgroundModes(NSBundle bundle)
        {
            var backgroundModes = bundle.ObjectForInfoDictionary("UIBackgroundModes");

            if (backgroundModes == null)
            {
                return new string[0];
            }

            return NSArray.StringArrayFromHandle(backgroundModes.Handle);
        }

        public static void ThrowIfNoBackgroundDescription(this NSBundle bundle)
        {
            ThrowIfNoDescription(bundle, "NSLocationAlwaysUsageDescription");
        }

        public static void ThrowIfNoInUseDescription(this NSBundle bundle)
        {
            ThrowIfNoDescription(bundle, "NSLocationWhenInUseUsageDescription");
        }

        static void ThrowIfNoDescription(NSBundle bundle, string key)
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                return;
            }

            var backgroundModes = bundle.ObjectForInfoDictionary(key);
            if (backgroundModes == null)
            {
                throw new InvalidOperationException(string.Format("You must provide a value for {0} in Info.plist to use location services", key));
            }
        }
    }
}