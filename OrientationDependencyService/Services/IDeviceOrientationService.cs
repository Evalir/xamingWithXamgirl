using System;
using Xamarin.Forms.Internals;

namespace OrientationDependencyService
{

    public interface IDeviceOrientationService
    {
        DeviceOrientation GetPhoneOrientation();
    }
}
