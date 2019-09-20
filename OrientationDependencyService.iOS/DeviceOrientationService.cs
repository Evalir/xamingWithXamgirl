using System;
using System.Runtime.CompilerServices;
using OrientationDependencyService.iOS;
using UIKit;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceOrientationService))]

namespace OrientationDependencyService.iOS
{
    
    public class DeviceOrientationService : IDeviceOrientationService
    {
        public DeviceOrientation GetPhoneOrientation()
        {
            UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;

            bool isPortrait = orientation == UIInterfaceOrientation.Portrait ||
                orientation == UIInterfaceOrientation.PortraitUpsideDown;
            return isPortrait ? DeviceOrientation.Portrait : DeviceOrientation.Landscape;
        }

    }
}
