using Foundation;

namespace RxPosition.iOS
{
    public static class MainThreadReturnExtention
    {
        public static T CreateOnMainThread<T>(this NSObject nsObject) where T : new()
        {
            if (NSThread.Current.IsMainThread)
            {
                return new T();
            }

            T result = default(T);
            nsObject.InvokeOnMainThread(() => new T());
            return result;
        }
    }
}