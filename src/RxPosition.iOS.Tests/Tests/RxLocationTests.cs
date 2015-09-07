using System;
using System.Threading.Tasks;
using Xunit;
using System.Reactive.Linq;
using RxPosition.Core;

namespace RxPosition.iOS.Tests
{
    public class RxPositionTests
    {
        [Fact]
        public void Updates_Location_On_Subscription()
        {
            var lm = TestableLocationManager.Create();
            var sut = new iOSRxPosition(() => lm);

            Assert.False(lm.IsUpdating);
            var subscription = sut.Position.Subscribe(x => { });
            Assert.True(lm.IsUpdating);
            subscription.Dispose();
            Assert.False(lm.IsUpdating);
        }

        [Fact]
        public async Task Can_Get_Single_Location()
        {
            var lm = TestableLocationManager.Create();
            var sut = new iOSRxPosition(() => lm);

            var location = await sut.Position.FirstAsync();
            Console.WriteLine(location.Coordinate);
            Assert.False(lm.IsUpdating);
        }

        [Fact]
        public async Task Subscription_Toggles_Updating()
        {
            var lm = TestableLocationManager.Create();
            var sut = new iOSRxPosition(() => lm);

            await GetLocationAsync(lm, sut);
            await GetLocationAsync(lm, sut);
        }

        async Task GetLocationAsync(TestableLocationManager lm, iOSRxPosition sut)
        {
            var tcs = new TaskCompletionSource<Position>();
            var sub = sut.Position.Subscribe(x => tcs.TrySetResult(x));
            Assert.True(lm.IsUpdating, "Not updating after subscribe");

            await tcs.Task;

            // Unsubscribe
            sub.Dispose();
            Assert.False(lm.IsUpdating, "Still updating after unsubscribe");
        }
    }
}
