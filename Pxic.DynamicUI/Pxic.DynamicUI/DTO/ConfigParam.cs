using Pxic.DynamicUI.Helper;
using Pxic.DynamicUI.Model;

namespace Pxic.DynamicUI.DTO
{
    public class ConfigParam : NotifyPropertyChangedBase
    {
        public ConfigParam()
        {
            
        }

        public ConfigParam(string parameterId,
                             string name,
                             string description,
                             FieldType controlType,
                             decimal minValue,
                             decimal maxValue,
                             decimal defaultValue,
                             string validation,
                             bool isReadonly,
                             string unit)
        {
            this.ParameterId = parameterId;
            this.Name = name;
            this.Description = description;
            this.ControlType = controlType;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.DefaultValue = defaultValue;
            this.Validation = validation;
            this.IsReadonly = isReadonly;
            this.Unit = unit;
        }

        private string parameterId = string.Empty;
        public string ParameterId
        {
            get { return parameterId; }
            set { parameterId = value; NotifyPropertyChanged(); }
        }

        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

        private string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged(); }
        }

        private FieldType controlType = FieldType.Number;
        public FieldType ControlType
        {
            get { return controlType; }
            set { controlType = value; NotifyPropertyChanged(); }
        }

        private decimal minValue;
        public decimal MinValue
        {
            get { return minValue; }
            set { minValue = value; NotifyPropertyChanged(); }
        }

        private decimal maxValue;
        public decimal MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; NotifyPropertyChanged(); }
        }

        private decimal defaultValue;
        public decimal DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; NotifyPropertyChanged(); }
        }

        private string validation = string.Empty;
        public string Validation
        {
            get { return validation; }
            set { validation = value; NotifyPropertyChanged(); }
        }

        private bool isReadonly;
        public bool IsReadonly
        {
            get { return isReadonly; }
            set { isReadonly = value; NotifyPropertyChanged(); }
        }

        private string unit = string.Empty;
        public string Unit
        {
            get { return unit; }
            set { unit = value; NotifyPropertyChanged(); }
        }
    }
}
