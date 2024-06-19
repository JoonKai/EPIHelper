using System.Windows;
using System.Windows.Controls;

namespace EPIControls.Controls.BaseCustomControl.Units
{
    public class DayOfWeek : Label
    {
        static DayOfWeek()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DayOfWeek), new FrameworkPropertyMetadata(typeof(DayOfWeek)));
        }
    }
}
