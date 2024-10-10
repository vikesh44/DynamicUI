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

namespace Pxic.DynamicUI.View.User_Controls
{
    /// <summary>
    /// Interaction logic for NumericUserControl.xaml
    /// </summary>
    public partial class NumericUserControl : UserControl
    {
        public NumericUserControl()
        {
            InitializeComponent();
        }

        // Dependency Property for MinValue
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericUserControl),
                new PropertyMetadata(0));

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Dependency Property for MaxValue
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUserControl),
                new PropertyMetadata(100));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Dependency Property for Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericUserControl),
                new PropertyMetadata(0, OnValueChanged));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Event handler for when Value is changed
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericUserControl;
            if (control != null)
            {
                int newValue = (int)e.NewValue;
                if (newValue < control.MinValue)
                    control.Value = control.MinValue;
                else if (newValue > control.MaxValue)
                    control.Value = control.MaxValue;
            }
        }

        // Handler for the Up Button
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value < MaxValue)
            {
                Value++;
            }
        }

        // Handler for the Down Button
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > MinValue)
            {
                Value--;
            }
        }
    }
}
