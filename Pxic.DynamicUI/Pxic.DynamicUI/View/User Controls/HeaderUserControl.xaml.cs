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
    /// Interaction logic for HeaderUserControl.xaml
    /// </summary>
    public partial class HeaderUserControl : UserControl
    {
        public HeaderUserControl()
        {
            InitializeComponent();
        }



        public BitmapImage DeviceImage
        {
            get { return (BitmapImage)GetValue(DeviceImageProperty); }
            set { SetValue(DeviceImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeviceImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceImageProperty =
            DependencyProperty.Register("DeviceImage", typeof(BitmapImage), typeof(HeaderUserControl), new PropertyMetadata(null));



        public string DeviceName
        {
            get { return (string)GetValue(DeviceNameProperty); }
            set { SetValue(DeviceNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeviceName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceNameProperty =
            DependencyProperty.Register("DeviceName", typeof(string), typeof(HeaderUserControl), new PropertyMetadata(string.Empty));




        public string DeviceVendor
        {
            get { return (string)GetValue(DeviceVendorProperty); }
            set { SetValue(DeviceVendorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeviceVendor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceVendorProperty =
            DependencyProperty.Register("DeviceVendor", typeof(string), typeof(HeaderUserControl), new PropertyMetadata(string.Empty));


    }
}
