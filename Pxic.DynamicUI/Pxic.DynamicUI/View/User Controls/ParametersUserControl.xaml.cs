using Pxic.DynamicUI.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pxic.DynamicUI.View.User_Controls
{
    /// <summary>
    /// Interaction logic for ParametersUserControl.xaml
    /// </summary>
    public partial class ParametersUserControl : UserControl
    {
        public ParametersUserControl()
        {
            InitializeComponent();
        }


        #region Dependency Properties

        public string GroupboxHeader
        {
            get { return (string)GetValue(GroupboxHeaderProperty); }
            set { SetValue(GroupboxHeaderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for GroupboxHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupboxHeaderProperty =
            DependencyProperty.Register("GroupboxHeader", typeof(string), typeof(ParametersUserControl), new PropertyMetadata(string.Empty));


        public ObservableCollection<ConfigParam> ConfigParameters
        {
            get { return (ObservableCollection<ConfigParam>)GetValue(ConfigParametersProperty); }
            set { SetValue(ConfigParametersProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ConfigParameters.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfigParametersProperty =
            DependencyProperty.Register("ConfigParameters", typeof(ObservableCollection<ConfigParam>), typeof(ParametersUserControl), new PropertyMetadata(new ObservableCollection<ConfigParam>()));


        #endregion

    }
}
