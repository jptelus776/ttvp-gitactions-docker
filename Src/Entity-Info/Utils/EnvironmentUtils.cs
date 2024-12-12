using Serilog;

namespace EntityInfoService.Utils
{
    public class EnvironmentUtils
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(EnvironmentUtils));

        /// <summary>
        /// Returns the value of an environment variable with an optional default value.
        /// If the environment variable is not found and no default value is provided,
        /// an exception will be raised.
        /// </summary>
        /// <param name="variableName">The name of the environment variable to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the environment variable is not found (optional).</param>
        /// <returns>The value of the environment variable, or the default value if provided, or an exception if not found.</returns>
        public static string? GetEnvironmentVariable(string variableName, string? defaultValue = null, bool isRequired = true)
        {
            string? value = Environment.GetEnvironmentVariable(variableName);

            if (value == null)
            {

                if (string.IsNullOrWhiteSpace(defaultValue) && isRequired)
                {
                    var msg = $"Environment variable {variableName} is required but is not set.";
                    _logger.Error(msg);
                    throw new ArgumentNullException(msg);
                }
                else
                {
                    return defaultValue;
                }
            }

            return value;
        }

        public static T? GetEnvironmentVariableAs<T>(string variableName, T? defaultValue = null) where T : struct, IComparable, IConvertible, IEquatable<T>
        {
            string? value = Environment.GetEnvironmentVariable(variableName);

            if (value == null)
                return defaultValue;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (FormatException)
            {
                var msg = $"Environment variable {variableName} is set to an invalid value: {value}";
                _logger.Error(msg);
                throw new FormatException(msg);
            }
        }

        public static T GetRequiredEnvironmentVariableAs<T>(string variableName, T? defaultValue = null) where T : struct, IComparable, IConvertible, IEquatable<T>
        {
            string? value = Environment.GetEnvironmentVariable(variableName);

            if (value == null)
            {
                if (!defaultValue.HasValue)
                {
                    var msg = $"Environment variable {variableName} is required but is not set.";
                    _logger.Error(msg);
                    throw new ArgumentNullException(msg);
                }
                else
                {
                    return (T)defaultValue;
                }
            }

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (FormatException)
            {
                var msg = $"Environment variable {variableName} is set to an invalid value: {value}";
                _logger.Error(msg);
                throw new FormatException(msg);
            }
        }
    }
}
