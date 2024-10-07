using Pxic.DynamicUI.DTO;
using Pxic.DynamicUI.Helper;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Pxic.DynamicUI.ViewModel
{
    public class OfflineParamTemplateViewModel : ViewModelBase
    {
        public OfflineParamTemplateViewModel()
        {
            DeviceId = 1;

            List<ProcedureParameter> parameters = [new ProcedureParameter("DeviceId", DeviceId)];

            List<DeviceInfo> deviceDetail = DatabaseHelper.Instance.GetData<DeviceInfo>("SSP_GetDeviceInfo", parameters);

            if (deviceDetail != null && deviceDetail[0] != null)
            {
                DeviceImage = HelperFunctions.Instance.ByteArrayToImage(deviceDetail[0].Image);
                DeviceName = deviceDetail[0].Name;
                DeviceVendor = deviceDetail[0].Vendor;
            }

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
            SelectedTreeItem = TreeListItems.FirstOrDefault(ti => ti.IsSelected);
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

        #region Binding Properties

        private ObservableCollection<TreeItemDetail> treeListItems = [];
        public ObservableCollection<TreeItemDetail> TreeListItems
        {
            get { return treeListItems; }
            set { treeListItems = value; NotifyPropertyChanged(); }
        }

        private TreeItemDetail selectedTreeItem;
        public TreeItemDetail SelectedTreeItem
        {
            get { return selectedTreeItem; }
            set { selectedTreeItem = value; }
        }

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

        #endregion
    }
}
