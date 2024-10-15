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
    /// Interaction logic for SelectUserControl.xaml
    /// </summary>
    public partial class SelectUserControl : UserControl
    {
        public SelectUserControl()
        {
            InitializeComponent();
        }

        public Dictionary<int, string> ItemCollection
        {
            get { return (Dictionary<int, string>)GetValue(ItemCollectionProperty); }
            set { SetValue(ItemCollectionProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ItemCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCollectionProperty =
            DependencyProperty.Register("ItemCollection", typeof(Dictionary<int, string>), typeof(SelectUserControl), new PropertyMetadata(new Dictionary<int, string>()));


        public int SelectedMember
        {
            get { return (int)GetValue(SelectedMemberProperty); }
            set { SetValue(SelectedMemberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMember.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMemberProperty =
            DependencyProperty.Register("SelectedMember", typeof(int), typeof(SelectUserControl), new PropertyMetadata(0));


    }
}
