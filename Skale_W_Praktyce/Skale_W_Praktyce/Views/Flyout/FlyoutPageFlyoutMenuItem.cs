using System;

namespace Skale_W_Praktyce.Views.Flyout
{
    public class FlyoutPageFlyoutMenuItem
    {
        public FlyoutPageFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}