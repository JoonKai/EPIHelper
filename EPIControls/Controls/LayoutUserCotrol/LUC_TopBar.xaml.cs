using System.Windows;
using System.Windows.Controls;
using EPI.Extensions;

namespace EPIControls.Controls.LayoutUserCotrol
{
    public partial class LUC_TopBar : UserControl
    {
        #region /////Dependency Property
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(LUC_TopBar), new PropertyMetadata("EPI"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion
        #region /////Property
        private Window _parentWindow;
        public Window ParentWindow
        {
            get
            {
                if (_parentWindow == null)
                {
                    _parentWindow = this.FindParent<Window>();
                }
                return _parentWindow;
            }
            set { _parentWindow = value; }
        }
        #endregion
        public LUC_TopBar()
        {
            InitializeComponent();
            MinimizeButton.Click += MinimizeButton_Click;
            MaximizeButton.Click += MaximizeButton_Click;
            ExitButton.Click += ExitButton_Click;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = ParentWindow.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = WindowState.Minimized;
            ParentWindow.ResizeMode = ResizeMode.CanResizeWithGrip;
        }
    }
}
