namespace Hyperbliss
{
    public class ApiValue
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ApiValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public ApiValue()
        { }
    }
}
