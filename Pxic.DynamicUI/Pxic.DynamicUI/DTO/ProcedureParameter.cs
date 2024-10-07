namespace Pxic.DynamicUI.DTO
{
    public class ProcedureParameter
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public ProcedureParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
