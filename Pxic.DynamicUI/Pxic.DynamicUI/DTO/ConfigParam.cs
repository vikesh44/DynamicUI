using Pxic.DynamicUI.Helper;

namespace Pxic.DynamicUI.DTO
{
    public class ConfigParam : NotifyPropertyChangedBase
    {
        public ConfigParam(string paramId)
        {
            parameterId = paramId;
        }

        private string parameterId;
		public string ParameterId
		{
			get { return parameterId; }
			set { parameterId = value; NotifyPropertyChanged(); }
		}
	}
}
