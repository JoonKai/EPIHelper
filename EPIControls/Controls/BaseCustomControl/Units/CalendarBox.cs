using System.Windows;
using System.Windows.Controls;

namespace EPIControls.Controls.BaseCustomControl.Units
{
    public class CalendarBox : ListBox
    {
        static CalendarBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarBox), new FrameworkPropertyMetadata(typeof(CalendarBox)));
        }
    }
}
