using System.Windows;
using System.Windows.Controls.Primitives;

namespace EPIControls.Controls.BaseCustomControl.Units
{
    public class CalendarSwitch : ToggleButton
    {
        static CalendarSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarSwitch), new FrameworkPropertyMetadata(typeof(CalendarSwitch)));
        }
    }
}
