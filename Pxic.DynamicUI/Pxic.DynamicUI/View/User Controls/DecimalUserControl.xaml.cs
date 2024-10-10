using System.Globalization;
using System.Windows;
using System.Windows.Controls;
 
namespace Pxic.DynamicUI.View.User_Controls
{
    /// <summary>
    /// Interaction logic for DecimalUserControl.xaml
    /// </summary>
    public partial class DecimalUserControl : UserControl
    {
        public DecimalUserControl()
        {
            InitializeComponent();
        }

        // Dependency Property for MinValue
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(decimal), typeof(DecimalUserControl),
                new PropertyMetadata(0.0m));

        public decimal MinValue
        {
            get { return (decimal)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Dependency Property for MaxValue
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(decimal), typeof(DecimalUserControl),
                new PropertyMetadata(100.0m));

        public decimal MaxValue
        {
            get { return (decimal)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Dependency Property for DecimalPlaces
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(DecimalUserControl),
                new PropertyMetadata(2, OnDecimalPlacesChanged));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        // Dependency Property for formatted display in the TextBox
        public static readonly DependencyProperty FormattedValueProperty =
            DependencyProperty.Register("FormattedValue", typeof(string), typeof(DecimalUserControl),
                new PropertyMetadata("0.00", OnFormattedValueChanged));

        public string FormattedValue
        {
            get { return (string)GetValue(FormattedValueProperty); }
            set { SetValue(FormattedValueProperty, value); }
        }

        // Dependency Property for Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(DecimalUserControl),
                new PropertyMetadata(0.0m, OnValueChanged));

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Event handler for when Value is changed
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DecimalUserControl;
            if (control != null)
            {
                decimal newValue = (decimal)e.NewValue;
                if (newValue < control.MinValue)
                    control.Value = control.MinValue;
                else if (newValue > control.MaxValue)
                    control.Value = control.MaxValue;

                control.Value = Math.Round(control.Value, control.DecimalPlaces);
                control.UpdateFormattedValue();
            }
        }

        // Event handler for when DecimalPlaces is changed
        private static void OnDecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DecimalUserControl;
            if (control != null)
            {
                control.Value = Math.Round(control.Value, control.DecimalPlaces);
                control.UpdateFormattedValue();
            }
        }

        // Event handler for when the formatted value is manually edited
        private static void OnFormattedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DecimalUserControl;
            if (control != null)
            {
                if (decimal.TryParse((string)e.NewValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedValue))
                {
                    control.Value = parsedValue;
                }
            }
        }

        // Updates the formatted value displayed in the TextBox
        private void UpdateFormattedValue()
        {
            FormattedValue = Value.ToString($"F{DecimalPlaces}", CultureInfo.InvariantCulture);
        }

        // Handler for the Up Button
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            decimal increment = (decimal)Math.Pow(10, -DecimalPlaces);
            if (Value + increment <= MaxValue)
            {
                Value = Math.Round(Value + increment, DecimalPlaces);
            }
        }

        // Handler for the Down Button
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            decimal decrement = (decimal)Math.Pow(10, -DecimalPlaces);
            if (Value - decrement >= MinValue)
            {
                Value = Math.Round(Value - decrement, DecimalPlaces);
            }
        }
    }
}
