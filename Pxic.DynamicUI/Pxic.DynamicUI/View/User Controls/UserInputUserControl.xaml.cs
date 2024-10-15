using Pxic.DynamicUI.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Pxic.DynamicUI.View.User_Controls
{
    /// <summary>
    /// Interaction logic for UserInputUserControl.xaml
    /// </summary>
    public partial class UserInputUserControl : UserControl, INotifyPropertyChanged
    {
        public UserInputUserControl()
        {
            InitializeComponent();
        }

        // Dependency Property for MinValue
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(decimal), typeof(UserInputUserControl),
                new PropertyMetadata(0.0m));

        public decimal MinValue
        {
            get { return (decimal)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Dependency Property for MaxValue
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(decimal), typeof(UserInputUserControl),
                new PropertyMetadata(100.0m));

        public decimal MaxValue
        {
            get { return (decimal)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Dependency Property for DecimalPlaces
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(UserInputUserControl),
                new PropertyMetadata(2));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        // Dependency Property for formatted display in the TextBox
        public static readonly DependencyProperty FormattedValueProperty =
            DependencyProperty.Register("FormattedValue", typeof(string), typeof(UserInputUserControl),
                new PropertyMetadata("0.00"));

        public string FormattedValue
        {
            get { return (string)GetValue(FormattedValueProperty); }
            set { SetValue(FormattedValueProperty, value); }
        }

        // Dependency Property for Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(UserInputUserControl),
                new PropertyMetadata(0.0m));

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        public FieldType ControlType
        {
            get { return (FieldType)GetValue(ControlTypeProperty); }
            set { SetValue(ControlTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlTypeProperty =
            DependencyProperty.Register("ControlType", typeof(FieldType), typeof(UserInputUserControl),
                new PropertyMetadata(FieldType.None, ControlType_SelectionChanged));

        private UserControl selectedControl;
        public UserControl SelectedControl
        {
            get { return selectedControl; }
            set { selectedControl = value; NotifyPropertyChanged(); }
        }

        private static void ControlType_SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UserInputUserControl;
            if (control != null)
            {
                FieldType newValue = (FieldType)e.NewValue;
                switch (newValue)
                {
                    case FieldType.Numeric:
                        control.SelectedControl = new NumericUserControl()
                        {
                            MinValue = (int)control.MinValue,
                            MaxValue = (int)control.MaxValue,
                            Value = int.Parse(control.FormattedValue),
                            DataContext = control.DataContext,
                        };
                        break;
                    case FieldType.Decimal:
                        control.SelectedControl = new DecimalUserControl()
                        {
                            MinValue = control.MinValue,
                            MaxValue = control.MaxValue,
                            Value = decimal.Parse(control.FormattedValue),
                            DataContext = control.DataContext,
                        };
                        break;
                    case FieldType.Select:
                        break;
                    default:
                        break;
                }
                //control.UpdateFormattedValue();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify subscribers that a property value has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed. If not provided, 
        /// it defaults to the name of the calling property.</param>
        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
