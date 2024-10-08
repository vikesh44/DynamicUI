using Pxic.DynamicUI.Helper;
using Pxic.DynamicUI.Model;

namespace Pxic.DynamicUI.DTO
{
    public class ConfigParam : NotifyPropertyChangedBase
    {
        public ConfigParam()
        {
            
        }

        public ConfigParam(string parameterId)
        {
            this.parameterId = parameterId;
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
            this.parameterId = parameterId;
            this.name = name;
            this.description = description;
            this.controlType = controlType;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.defaultValue = defaultValue;
            this.validation = validation;
            this.isReadonly = isReadonly;
            this.unit = unit;
        }

        private string parameterId;
        public string ParameterId
        {
            get { return parameterId; }
            set { parameterId = value; NotifyPropertyChanged(); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged(); }
        }

        private FieldType controlType;
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

        private string validation;
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

        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; NotifyPropertyChanged(); }
        }
    }
}
