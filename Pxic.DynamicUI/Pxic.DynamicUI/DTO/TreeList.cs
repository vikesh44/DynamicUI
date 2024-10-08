using Pxic.DynamicUI.ViewModel;
using System.Collections.ObjectModel;

namespace Pxic.DynamicUI.DTO
{
    public class TreeItemDetail : ViewModelBase
    {
        public TreeItemDetail()
        {

        }

        public TreeItemDetail(string tpageId, string tname, bool tisSelected)
        {
            PageId = tpageId;
            ItemText = tname;
            IsSelected = tisSelected;
        }

        private string? pageId;
        public string? PageId
        {
            get { return pageId; }
            set { pageId = value; NotifyPropertyChanged(); }
        }

        private string? itemText;
        public string? ItemText
        {
            get { return itemText; }
            set { itemText = value; NotifyPropertyChanged(); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<TreeItemDetail> itemChild = [];
        public ObservableCollection<TreeItemDetail> ItemChild
        {
            get { return itemChild; }
            set { itemChild = value; NotifyPropertyChanged(); }
        }
    }

    public class DevicePageInfo
    {
        public string PageId { get; set; } = string.Empty;
        public string ItemText { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
        public string ParentPageId { get; set; } = string.Empty;
    }
}
