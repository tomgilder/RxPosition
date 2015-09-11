# RxPosition

![](https://raw.githubusercontent.com/tomgilder/RxPosition/master/logo.png)

**Observable location information for Xamarin.iOS**

(More platforms to follow)

## General Features

* Stongly-typed position types, not just endless `double` values
* Waits for first subscription to start location updates
* Disables updates when there are no subscribers
* Handles prompting the user for permission

## iOS Features

* Requests background location when declared in Info.plist
* Alerts you if you're missing usage descriptions from Info.plist

## Usage

```#
IRxPosition provider = new iOSRxPosition();
provider.Position.Subscribe(
    onNext:  pos => Console.WriteLine(pos.Coordinate.Latitude),
    onError: ex => Console.WriteLine("LocationServicesAccessDenied thrown, no access to location")
);
```

The sequence never completes.