using EPIControls.Controls.BaseCustomControl.Units;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace EPIControls.Controls.BaseCustomControl
{
    public class EPIDatePicker : Control
    {
        private Popup _popup;
        private CalendarSwitch _switch;
        private CalendarBox _listbox;

        public bool KeepPopupOpen
        {
            get { return (bool)GetValue(KeepPopupOpenProperty); }
            set { SetValue(KeepPopupOpenProperty, value); }
        }

        public static readonly DependencyProperty KeepPopupOpenProperty =
            DependencyProperty.Register("KeepPopupOpen", typeof(bool), typeof(EPIDatePicker), new PropertyMetadata(true));



        public DateTime CurrentMonth
        {
            get { return (DateTime)GetValue(CurrentMonthProperty); }
            set { SetValue(CurrentMonthProperty, value); }
        }

        public static readonly DependencyProperty CurrentMonthProperty =
            DependencyProperty.Register("CurrentMonth", typeof(DateTime), typeof(EPIDatePicker), new PropertyMetadata(null));



        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(EPIDatePicker), new PropertyMetadata(null));



        static EPIDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EPIDatePicker), new FrameworkPropertyMetadata(typeof(EPIDatePicker)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _popup = (Popup)GetTemplateChild("PART_Popup");
            _switch = (CalendarSwitch)GetTemplateChild("PART_Switch");
            _listbox = (CalendarBox)GetTemplateChild("PART_ListBox");
            ChevronButton leftButton = (ChevronButton)GetTemplateChild("PART_Left");
            ChevronButton rightButton = (ChevronButton)GetTemplateChild("PART_Right");

            _popup.Closed += _popup_Closed;
            _switch.Click += _switch_Click;
            _listbox.MouseLeftButtonUp += _listbox_MouseLeftButtonUp;

            leftButton.Click += (s, e) => MoveMonthClick(-1);
            rightButton.Click += (s, e) => MoveMonthClick(1);

        }

        private void MoveMonthClick(int month)
        {
            GenerateCalendar(CurrentMonth.AddMonths(month));
        }

        private void _popup_Closed(object sender, EventArgs e)
        {
            _switch.IsChecked = IsMouseOver;
        }

        private void _switch_Click(object sender, RoutedEventArgs e)
        {
            if (_switch.IsChecked == true)
            {
                _popup.IsOpen = true;

                GenerateCalendar(SelectedDate ?? DateTime.Now);
            }
        }

        private void _listbox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_listbox.SelectedItem is CalendarBoxItem selected)
            {
                SelectedDate = selected.Date;
                GenerateCalendar(selected.Date);

                _popup.IsOpen = KeepPopupOpen;
            }
        }
        private void GenerateCalendar(DateTime current)
        {
            if (current.ToString("yyyyMM") == CurrentMonth.ToString("yyyyMM")) return;

            CurrentMonth = current;
            _listbox.Items.Clear();
            DateTime fDayOfMonth = new DateTime(current.Year, current.Month, 1);
            DateTime lDayOfMonth = fDayOfMonth.AddMonths(1).AddDays(-1);

            int fOffset = (int)fDayOfMonth.DayOfWeek;
            int lOffset = 6 - (int)lDayOfMonth.DayOfWeek;

            DateTime fDay = fDayOfMonth.AddDays(-fOffset);
            DateTime lDay = lDayOfMonth.AddDays(lOffset);

            for (DateTime day = fDay; day <= lDay; day = day.AddDays(1))
            {
                CalendarBoxItem boxItem = new CalendarBoxItem();
                boxItem.Date = day;
                boxItem.DateFormat = day.ToString("yyyyMMdd");
                boxItem.Content = day.Day;
                boxItem.IsCurrentMonth = day.Month == current.Month;

                _listbox.Items.Add(boxItem);
            }
            if (SelectedDate != null)
            {
                _listbox.SelectedValue = SelectedDate.Value.ToString("yyyyMMdd");
            }
        }
    }
}
