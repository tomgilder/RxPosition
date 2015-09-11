using System;
using Xunit;
using Foundation;
using CoreLocation;
using System.Reactive;
using System.Reactive.Linq;

namespace RxPosition.iOS.Tests
{
    public class AuthorizeTests
    {
        [Fact]
        public void Background_With_No_Description_Throws()
        {
            var bundle = new TestBundle
            {
                RequriesBackgroundLocation = true
            };

            var manager = new VerfyAuthorizeLocationManager();
            Assert.Throws<InvalidOperationException>(() =>
                manager.RequestAuthorization(bundle)
            );
            Assert.Equal(CLAuthorizationStatus.NotDetermined, manager.RequestedStatus);
        }

        [Fact]
        public void In_Use_With_No_Description_Throws()
        {
            var bundle = new TestBundle
                {
                    RequriesBackgroundLocation = false
                };

            var manager = new VerfyAuthorizeLocationManager();
            Assert.Throws<InvalidOperationException>(() =>
                manager.RequestAuthorization(bundle)
            );
            Assert.Equal(CLAuthorizationStatus.NotDetermined, manager.RequestedStatus);
        }

        [Fact]
        public void Background_With_Description_Does_Not_Throw()
        {
            var bundle = new TestBundle
                {
                    RequriesBackgroundLocation = true,
                    BackgroundLocationReason = "reason"
                };

            var manager = new VerfyAuthorizeLocationManager();
            manager.RequestAuthorization(bundle);
            Assert.Equal(CLAuthorizationStatus.AuthorizedAlways, manager.RequestedStatus);
        }

        [Fact]
        public void In_Use_With_Description_Does_Request()
        {
            var bundle = new TestBundle
                {
                    RequriesBackgroundLocation = false,
                    InUseLocationReason = "reason"
                };
                        
            var manager = new VerfyAuthorizeLocationManager();
            manager.RequestAuthorization(bundle);
            Assert.Equal(CLAuthorizationStatus.AuthorizedWhenInUse, manager.RequestedStatus);
        }

        class VerfyAuthorizeLocationManager : CLLocationManager
        {
            public VerfyAuthorizeLocationManager()
            {
                RequestedStatus = CLAuthorizationStatus.NotDetermined;
            }

            public CLAuthorizationStatus RequestedStatus { get; set; }
            
            public override void RequestAlwaysAuthorization()
            {
                RequestedStatus = CLAuthorizationStatus.AuthorizedAlways;
            }

            public override void RequestWhenInUseAuthorization()
            {
                RequestedStatus = CLAuthorizationStatus.AuthorizedWhenInUse;
            }
        }

        class TestBundle : NSBundle 
        {
            // This class replicates values from Info.plist

            public bool RequriesBackgroundLocation { get; set; }
            public string BackgroundLocationReason { get; set; }
            public string InUseLocationReason { get; set; }

            public override NSObject ObjectForInfoDictionary(string key)
            {
                switch (key)
                {
                    case "UIBackgroundModes":

                        return RequriesBackgroundLocation
                            ? NSArray.FromNSObjects(new NSString("location"))
                                : null;

                    case "NSLocationAlwaysUsageDescription":

                        return BackgroundLocationReason == null ? null : new NSString(BackgroundLocationReason);

                    case "NSLocationWhenInUseUsageDescription":

                        return InUseLocationReason == null ? null : new NSString(InUseLocationReason);
                }

                return null;
            }
        }
    }
}
