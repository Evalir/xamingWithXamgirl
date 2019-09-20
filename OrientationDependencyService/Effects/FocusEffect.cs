using System;
using Xamarin.Forms;

namespace OrientationDependencyService.Effects
{
    public class FocusEffect : RoutingEffect
    {
        public FocusEffect() : base ($"Azurite.{nameof(FocusEffect)}")
        {
        }
    }
}
