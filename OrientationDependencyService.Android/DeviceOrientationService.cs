using System;
using System.Runtime.CompilerServices;
using Android.Content;
using Android.Runtime;
using Android.Views;
using OrientationDependencyService.Droid;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceOrientationService))]
namespace OrientationDependencyService.Droid
{
    public class DeviceOrientationService : IDeviceOrientationService
    {
        public DeviceOrientation GetPhoneOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            SurfaceOrientation orientation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = orientation == SurfaceOrientation.Rotation90 ||
                orientation == SurfaceOrientation.Rotation270;
            return isLandscape ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;
        }
    }
}
