using EPI.Commons;
using System;
using System.Windows;
using WinInterop = System.Windows.Interop;

namespace EPIHelper.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.SourceInitialized += MainView_SourceInitialized;
        }

        private void MainView_SourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(Maximizer.WindowProc));
        }
    }
}
