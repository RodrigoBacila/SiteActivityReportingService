namespace Crosscutting.Shared
{
    public class OperationResult
    {
        public bool OK => !Errors.HasErrors;
        public bool IsFailure => Errors.HasErrors;
        public ErrorDictionary Errors { get; }

        public OperationResult() : this(new ErrorDictionary())
        {
        }

        public OperationResult(string key, string error) : this(new ErrorDictionary(key, error))
        {
        }

        public OperationResult(string key, params string[] errors) : this(new ErrorDictionary(key, errors))
        {
        }

        public OperationResult(ErrorDictionary errors)
        {
            Errors = errors;
        }

        public void AddError(string key, string error) => Errors.Add(key, error);
        public void AddErrors(string key, params string[] errors) => Errors.Add(key, errors);
        public void AddErrors(ErrorDictionary errors) => Errors.Merge(errors);

        public static OperationResult Success => new OperationResult();
        public static OperationResult Fail(string message) => new OperationResult("", message);
        public static OperationResult Fail(string key, string message) => new OperationResult(key, message);

        public static implicit operator OperationResult(ErrorDictionary errors) => new OperationResult(errors);
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; }

        public OperationResult() : this(default(T))
        {
        }

        public OperationResult(T result) : this(result, new ErrorDictionary())
        {
        }

        public OperationResult(string key, params string[] errors) : this(new ErrorDictionary(key, errors))
        {
        }

        public OperationResult(OperationResult validation) : base(validation?.Errors)
        {
        }

        public OperationResult(ErrorDictionary errors) : this(default(T), errors)
        {
        }

        public OperationResult(T result, ErrorDictionary errors) : base(errors)
        {
            Result = result;
        }

        public static new OperationResult<T> Fail(string key, string message) => new OperationResult<T>(key, message);
        public static OperationResult<TResult> Fail<TResult>(ErrorDictionary errors) => new OperationResult<TResult>(errors);

        public static implicit operator OperationResult<T>(T entity) => new OperationResult<T>(entity);

        public static implicit operator OperationResult<T>(ErrorDictionary errors) => new OperationResult<T>(errors);
    }
}