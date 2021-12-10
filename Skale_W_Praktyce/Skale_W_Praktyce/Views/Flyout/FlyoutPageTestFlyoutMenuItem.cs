using System;

namespace Skale_W_Praktyce.Views.Flyout
{
    public class FlyoutPageTestFlyoutMenuItem
    {
        public FlyoutPageTestFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutPageTestFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}