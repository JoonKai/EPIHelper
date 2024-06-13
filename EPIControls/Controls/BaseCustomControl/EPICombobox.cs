using System.Windows;
using System.Windows.Controls;

namespace EPIControls.Controls.BaseCustomControl
{
    public class EPICombobox : ComboBox
    {
        static EPICombobox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EPICombobox), new FrameworkPropertyMetadata(typeof(EPICombobox)));
        }
    }
}
