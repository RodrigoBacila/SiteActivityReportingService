namespace Crosscutting.Shared
{
    public class ErrorDictionary : List<KeyValuePair<string, string>>
    {
        public ErrorDictionary() { }
        public ErrorDictionary(string error) => Add("", error);
        public ErrorDictionary(IEnumerable<string> errors) => Add("", errors.ToArray());
        public ErrorDictionary(string key, string error) => Add(key, error);
        public ErrorDictionary(string key, params string[] errors) => Add(key, errors);
        public ErrorDictionary(IEnumerable<KeyValuePair<string, string>> errors) => Add(errors);

        public bool HasErrors => Count > 0;
        public bool IsEmpty => Count == 0;
        public static ErrorDictionary Empty => new ErrorDictionary();

        public static ErrorDictionary FromMany(IEnumerable<ErrorDictionary> errors)
        {
            var merged = new ErrorDictionary();

            if (errors != null)
                foreach (var error in errors)
                    merged.Merge(error);

            return merged;
        }

        public new ErrorDictionary Add(KeyValuePair<string, string> keyAndError)
        {
            base.Add(keyAndError);

            return this;
        }

        public ErrorDictionary Add(IEnumerable<KeyValuePair<string, string>> keyAndError)
        {
            foreach (var item in keyAndError)
                Add(item);

            return this;
        }

        public ErrorDictionary Add(string key, string error)
        {
            var element = new KeyValuePair<string, string>(key, error);

            Add(element);

            return this;
        }

        public ErrorDictionary Add(string key, params string[] errors)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            foreach (var error in errors)
                Add(key, error);

            return this;
        }

        public ErrorDictionary Merge(ErrorDictionary other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            other.ForEach(i => Add(i.Key, i.Value));

            return this;
        }

        public override string ToString()
        {
            var list = this.Select(error => error.Key + ": " + error.Value);

            return string.Join(Environment.NewLine, list);
        }

        public string ToSimpleString()
        {
            var list = this.Select(error => error.Value);

            return string.Join(Environment.NewLine, list);
        }
    }
}