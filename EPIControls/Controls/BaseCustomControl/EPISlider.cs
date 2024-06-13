using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EPIControls.Controls.BaseCustomControl
{
    public class EPISlider : Slider
    {
        static EPISlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EPISlider), new FrameworkPropertyMetadata(typeof(EPISlider)));
        }
    }
}
