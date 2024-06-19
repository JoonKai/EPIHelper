using System.Windows;
using System.Windows.Controls;

namespace EPIControls.Controls.BaseCustomControl.Units
{
    public class ChevronButton : Button
    {
        static ChevronButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChevronButton), new FrameworkPropertyMetadata(typeof(ChevronButton)));
        }
    }
}
