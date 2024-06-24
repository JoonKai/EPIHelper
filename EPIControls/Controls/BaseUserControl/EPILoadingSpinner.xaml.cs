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

namespace EPIControls.Controls.BaseUserControl
{
    /// <summary>
    /// EPILoadingSpinner.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EPILoadingSpinner : UserControl
    {


        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(EPILoadingSpinner), new PropertyMetadata(default(Duration)));


        public Brush SpinnerColor
        {
            get { return (Brush)GetValue(SpinnerColorProperty); }
            set { SetValue(SpinnerColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpinnerColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(EPILoadingSpinner), new PropertyMetadata(Brushes.DodgerBlue));



        public EPILoadingSpinner()
        {
            InitializeComponent();
        }
    }
}
