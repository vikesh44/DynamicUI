using Pxic.DynamicUI.DTO;
using Pxic.DynamicUI.Helper;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Pxic.DynamicUI.ViewModel
{
    public class OfflineParamTemplateViewModel : ViewModelBase
    {
        public OfflineParamTemplateViewModel()
        {
            TreeViewSelectionChanged = new RelayCommand<TreeItemDetail>(TreeView_SelectionChanged);

            DeviceId = 1;

            GetHeaderDetails();

            GetTreeViewItems();

            GetConfigParameterDetails();
        }

        private void GetConfigParameterDetails()
        {
            TreeViewSelectedItem = TreeListItems.FirstOrDefault(ti => ti.IsSelected).ItemText;

            ConfigurationParameters.Add(new ConfigParam("P1.1"));
            ConfigurationParameters.Add(new ConfigParam("P1.2"));
            ConfigurationParameters.Add(new ConfigParam("P1.3"));
            ConfigurationParameters.Add(new ConfigParam("P1.4"));
            ConfigurationParameters.Add(new ConfigParam("P1.5"));
        }

        private void GetTreeViewItems()
        {
            List<ProcedureParameter> parameters = [new ProcedureParameter("DeviceId", DeviceId)];
            List<DevicePageInfo> devicePages = DatabaseHelper.Instance.GetData<DevicePageInfo>("SSP_GetDevicePageInfo", parameters);

            foreach (DevicePageInfo pageInfo in devicePages)
            {
                if (pageInfo.ParentPageId == null)
                {
                    TreeListItems.Add(new TreeItemDetail(pageInfo.PageId, pageInfo.ItemText, pageInfo.IsSelected));
                }
                else
                {
                    TreeItemDetail parentItem = GetParentItem(pageInfo.ParentPageId, TreeListItems);
                    parentItem.ItemChild.Add(new TreeItemDetail(pageInfo.PageId, pageInfo.ItemText, pageInfo.IsSelected));
                }
            }
        }

        private void GetHeaderDetails()
        {
            List<ProcedureParameter> parameters = [new ProcedureParameter("DeviceId", DeviceId)];

            List<DeviceInfo> deviceDetail = DatabaseHelper.Instance.GetData<DeviceInfo>("SSP_GetDeviceInfo", parameters);

            if (deviceDetail != null && deviceDetail[0] != null)
            {
                DeviceImage = HelperFunctions.Instance.ByteArrayToImage(deviceDetail[0].Image);
                DeviceName = deviceDetail[0].Name;
                DeviceVendor = deviceDetail[0].Vendor;
            }
        }

        private void TreeView_SelectionChanged(TreeItemDetail param)
        {
            TreeViewSelectedItem = param.ItemText;
        }

        private TreeItemDetail GetParentItem(string parentPageId, ObservableCollection<TreeItemDetail> treeItemDetails)
        {
            TreeItemDetail parentItem = null;
            foreach (TreeItemDetail item in treeItemDetails)
            {
                if (item.PageId == parentPageId)
                {
                    parentItem = item;
                    break;
                }

                if (item.ItemChild != null)
                {
                    parentItem = GetParentItem(parentPageId, item.ItemChild);
                }
            }
            return parentItem;
        }

        #region Commands Implementations

        public ICommand TreeViewSelectionChanged { get; }

        #endregion

        #region Binding Properties

        private int deviceId;
        public int DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; NotifyPropertyChanged(); }
        }

        private BitmapImage deviceImage;
        public BitmapImage DeviceImage
        {
            get { return deviceImage; }
            set { deviceImage = value; NotifyPropertyChanged(); }
        }

        private string deviceName;
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; NotifyPropertyChanged(); }
        }

        private string deviceVendor;
        public string DeviceVendor
        {
            get { return deviceVendor; }
            set { deviceVendor = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<TreeItemDetail> treeListItems = [];
        public ObservableCollection<TreeItemDetail> TreeListItems
        {
            get { return treeListItems; }
            set { treeListItems = value; NotifyPropertyChanged(); }
        }

        private string treeViewSelectedItem;
        public string TreeViewSelectedItem
        {
            get { return treeViewSelectedItem; }
            set { treeViewSelectedItem = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ConfigParam> configurationParameters = [];
        public ObservableCollection<ConfigParam> ConfigurationParameters
        {
            get { return configurationParameters; }
            set { configurationParameters = value; NotifyPropertyChanged(); }
        }

        #endregion
    }
}
