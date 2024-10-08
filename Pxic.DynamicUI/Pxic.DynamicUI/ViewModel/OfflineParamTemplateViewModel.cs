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
        /// <summary>
        /// Initializes a new instance of the <see cref="OfflineParamTemplateViewModel"/> class.
        /// </summary>
        public OfflineParamTemplateViewModel()
        {
            TreeViewSelectionChanged = new RelayCommand<TreeItemDetail>(TreeView_SelectionChanged);

            DeviceId = 1;

            GetHeaderDetails();

            GetTreeViewItems();

            TreeItemDetail? selectedcItem = TreeListItems.FirstOrDefault(ti => ti.IsSelected);

            GetConfigParameterDetails(selectedcItem);
        }

        /// <summary>
        /// Retrieves configuration parameter details for the selected tree item.
        /// </summary>
        /// <param name="selectedItem">The currently selected tree item detail.</param>

        private void GetConfigParameterDetails(TreeItemDetail? selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }
            TreeViewSelectedItem = selectedItem;

            List<ProcedureParameter> parameters = [new ProcedureParameter("PageId", TreeViewSelectedItem.PageId ?? string.Empty)];
            List<ConfigParam> deviceParams = DatabaseHelper.Instance.GetData<ConfigParam>("SSP_GetPageParams", parameters);

            ConfigurationParameters = new ObservableCollection<ConfigParam>(deviceParams);
        }

        /// <summary>
        /// Fetches and organizes tree view items based on device pages from the database.
        /// </summary>
        private void GetTreeViewItems()
        {
            List<ProcedureParameter> parameters = [new ProcedureParameter("DeviceId", DeviceId)];
            List<DevicePageInfo> devicePages = DatabaseHelper.Instance.GetData<DevicePageInfo>("SSP_GetDevicePageInfo", parameters);

            foreach (DevicePageInfo pageInfo in devicePages)
            {
                if (string.IsNullOrEmpty(pageInfo.ParentPageId))
                {
                    TreeListItems.Add(new TreeItemDetail(pageInfo.PageId, pageInfo.ItemText, pageInfo.IsSelected));
                }
                else
                {
                    TreeItemDetail? parentItem = GetParentItem(pageInfo.ParentPageId, TreeListItems);
                    parentItem?.ItemChild.Add(new TreeItemDetail(pageInfo.PageId, pageInfo.ItemText, pageInfo.IsSelected));
                }
            }
        }

        /// <summary>
        /// Retrieves device header details such as image, name, and vendor information based on the current device ID.
        /// </summary>
        private void GetHeaderDetails()
        {
            List<ProcedureParameter> parameters = [new ProcedureParameter("DeviceId", DeviceId)];

            List<DeviceInfo> deviceDetail = DatabaseHelper.Instance.GetData<DeviceInfo>("SSP_GetDeviceInfo", parameters);

            if (deviceDetail != null && deviceDetail[0] != null)
            {
                DeviceImage = HelperFunctions.Instance.ByteArrayToImage(deviceDetail[0].Image) ??
                              new(new Uri("pack://application:,,,/Images/No Image.png", UriKind.Absolute));
                DeviceName = deviceDetail[0].Name;
                DeviceVendor = deviceDetail[0].Vendor;
            }
        }

        /// <summary>
        /// Command handler that is invoked when the selected item in the tree view changes.
        /// Updates the configuration parameters based on the newly selected tree item.
        /// </summary>
        /// <param name="param">The newly selected tree item detail.</param>
        private void TreeView_SelectionChanged(TreeItemDetail param)
        {
            GetConfigParameterDetails(param);
        }

        /// <summary>
        /// Recursively searches for a parent tree item based on the provided parent page ID.
        /// </summary>
        /// <param name="parentPageId">The ID of the parent page to find.</param>
        /// <param name="treeItemDetails">The collection of tree item details to search within.</param>
        /// <returns>The found parent tree item detail or <c>null</c> if not found.</returns>
        private TreeItemDetail? GetParentItem(string parentPageId, ObservableCollection<TreeItemDetail> treeItemDetails)
        {
            TreeItemDetail? parentItem = null;
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

        /// <summary>
        /// Command that gets invoked when the selection in the tree view changes.
        /// </summary>
        public ICommand TreeViewSelectionChanged { get; }

        #endregion

        #region Binding Properties

        private int deviceId;
        public int DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; NotifyPropertyChanged(); }
        }

        private BitmapImage deviceImage = new(new Uri("pack://application:,,,/Images/No Image.png", UriKind.Absolute));
        public BitmapImage DeviceImage
        {
            get { return deviceImage; }
            set { deviceImage = value; NotifyPropertyChanged(); }
        }

        private string deviceName = string.Empty;
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; NotifyPropertyChanged(); }
        }

        private string deviceVendor = string.Empty;
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

        private TreeItemDetail treeViewSelectedItem = new();
        public TreeItemDetail TreeViewSelectedItem
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
