using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
// using System.Text.Json;

namespace LibCulqi.util
{
    public class Helper
    {
        public static bool IsValidCardNumber(string number)
        {
            return Regex.IsMatch(number, "^\\d{13,19}$");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, "^\\S+@\\S+\\.\\S+$");
        }

        public static void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new CustomException("Currency code is empty.");
            }

            List<string> allowedValues = new List<string> { "PEN", "USD" };
            if (!allowedValues.Contains(currencyCode))
            {
                throw new CustomException("Currency code must be either \"PEN\" or \"USD\".");
            }
        }

        public static void ValidateStringStart(string str, string start)
        {
            if (!(str.StartsWith(start + "_test_") || str.StartsWith(start + "_live_")))
            {
                throw new CustomException($"Incorrect format. The format must start with {start}_test_ or {start}_live_");
            }
        }

        public static void ValidateValue(string value, List<string> allowedValues)
        {
            if (!allowedValues.Contains(value))
            {
                throw new CustomException($"Invalid value. It must be one of {string.Join(", ", allowedValues)}");
            }
        }

        public static bool IsFutureDate(long expirationDate)
        {
            DateTimeOffset expDate = DateTimeOffset.FromUnixTimeSeconds(expirationDate);
            return expDate > DateTimeOffset.Now;
        }

        public static void ValidateDateFilter(string dateFrom, string dateTo)
        {
            if (!int.TryParse(dateFrom, out int parsedDateFrom) ||
                !int.TryParse(dateTo, out int parsedDateTo))
            {
                throw new CustomException("Invalid value. Date_from and Date_to must be integers.");
            }

            if (parsedDateTo < parsedDateFrom)
            {
                throw new CustomException("Invalid value. Date_from must be less than Date_to.");
            }
        }
        public static void ValidateAmountValue(object amountObj)
        {
            if (amountObj is int)
            {
                // Amount is already an integer, no further validation needed.
            }
            else if (amountObj is string amountStr && int.TryParse(amountStr, out int _))
            {
                // Successfully parsed string to integer
            }
            else
            {
                throw new CustomException("Invalid 'amount'. It should be an integer or a string representing an integer.");
            }
        }
        public static void ValidateMinValue(int value, int minimumValue, string errorMessage)
        {
            if (value < minimumValue)
            {
                throw new CustomException(errorMessage);
            }
            return;

        }

        public static void ValidateMaxValue(int value, int maximumValue, string errorMessage)
        {
            if (value > maximumValue)
            {
                throw new CustomException(errorMessage);
            }
            return;

        }

        public static void ValidateLengthSizeValue(string input, int size, string errorMessage)
        {
            string trimmedInput = input.Trim();

            if (trimmedInput.Length != size)
            {
                throw new CustomException(errorMessage);
            }
            return;
        }

        public static void ValidateEnumsIntValue(object value, int[] values, string errorMessage)
        {
            if (!(value is int))
            {
                throw new ArgumentException("El valor debe ser de tipo int.", nameof(value));
            }

            int intValue = (int)value;

            if (!values.Contains(intValue))
            {
                throw new CustomException(errorMessage);
            }
        }
        public static void ValidateEnumsStringValue(string value, string[] validValues, string errorMessage)
        {
            string trimmedValue = value.Trim();

            if (!validValues.Contains(trimmedValue))
            {
                throw new CustomException(errorMessage);
            }
            return;
        }

        public static void ValidateSizeCreationDate(int date, string errorMessage)
        {
            if (date != 10 && date != 13)
            {
                throw new CustomException(errorMessage);
            }
        }
        public static void ValidateAmountValueWithError(object amountObj, string errorMessage)
        {
            if (amountObj is int)
            {
                return;
            }
            else if (amountObj is string amountStr && int.TryParse(amountStr, out int _))
            {
                return;
            }
            else
            {
                throw new CustomException(errorMessage);
            }
        }
        public static void ValidateNotNull(object element, string errorMessage)
        {
            if (element is null)
            {
                throw new CustomException(errorMessage);
            }
        }
        public static void ValidateTypeInt(object element, string errorMessage)
        {
            if (!(element is int))
            {
                throw new CustomException(errorMessage);
            }
            return;
        }
        public static void ValidateTypeString(object element, string errorMessage)
        {
            if (!(element is string))
            {
                throw new CustomException(errorMessage);
            }
            return;
        }

        public static void ValidateTypeBool(object element, string errorMessage)
        {
            if (!(element is bool))
            {
                throw new CustomException(errorMessage);
            }
            return;
        }
        public static void ValidateMin(object element, int minSize, string errorMessage)
        {
            if (element is string stringValue)
            {
                if (stringValue.Length < minSize)
                {
                    throw new CustomException(errorMessage);
                }
            }
            if (element is int intValue)
            {
                if (intValue < minSize)
                {
                    throw new CustomException(errorMessage);
                }
            }
        }
        public static void ValidateMax(object element, int maxSize, string errorMessage)
        {
            if (element is string stringValue)
            {
                if (stringValue.Length > maxSize)
                {
                    throw new CustomException(errorMessage);
                }
            }
            if (element is int intValue)
            {
                if (intValue > maxSize)
                {
                    throw new CustomException(errorMessage);
                }
            }
        }
        public static void ValidateRegex(string value, string regx, string errorMessage)
        {
            string trimmedValue = value.Trim();

            if (!Regex.IsMatch(trimmedValue, regx))
            {
                throw new CustomException(errorMessage);
            }
        }
        public static string ValidateMetadataSchema(object objMetadata)
        {   
            IDictionary<string, object> metadata = objMetadata as IDictionary<string, object>;
            if (!(objMetadata is IDictionary<string, object>) && !(objMetadata is IEnumerable<KeyValuePair<string, object>>))
            {
                throw new CustomException(ConstantsResponse.METADATA_INVALID);
            }

            if (metadata.Count() > 20)
            {
                throw new CustomException(ConstantsResponse.METADATA_LIMIT_20);
            }

            var transformedMetadata = new Dictionary<string, string>();

            foreach (var entry in metadata)
            {
                var paramKey = entry.Key.Length;
                var paramValue = entry.Value.ToString().Length;

                if (paramKey < 1 || paramKey > 30 || paramValue < 1 || paramValue > 2000)
                {
                    throw new CustomException(ConstantsResponse.METADATA_LIMIT_KEY_30_CHARACTERS_RF);
                }

                transformedMetadata[entry.Key] = entry.Value.ToString();
            }

            return JsonConvert.SerializeObject(transformedMetadata);
        }
        public static void ValidateEnumCurrency(string currency)
        {
            if (!(ConstantsRequest.CURRENCY_ENUM.Contains(currency)))
            {
                throw new CustomException(ConstantsResponse.PLAN_INVALID_CURRENCY_ENUM);
            }
        }
        public static void ValidateCurrency(string currency, int amount)
        {
            ValidateEnumCurrency(currency);

            int minAmountPublicApi = ConstantsRequest.MIN_AMOUNT_PEN * 100;
            int maxAmountPublicApi = ConstantsRequest.MAX_AMOUNT_PEN * 100;

            if (currency == "USD")
            {
                minAmountPublicApi = ConstantsRequest.MIN_AMOUNT_USD * 100;
                maxAmountPublicApi = ConstantsRequest.MAX_AMOUNT_USD * 100;
            }

            bool validAmount = amount >= minAmountPublicApi && amount <= maxAmountPublicApi;

            if (!validAmount)
            {
                if (currency == "USD")
                {
                    throw new CustomException(ConstantsResponse.PLAN_INVALID_AMOUNT_RANGE_USD);
                }

                throw new CustomException(ConstantsResponse.PLAN_INVALID_AMOUNT_RANGE_PEN);
            }
        }
        public static void ValidateInitialCycles(Dictionary<string, object> initialCycles, string currency, int amount)
        {
            bool hasInitialCharge = Convert.ToBoolean(initialCycles["has_initial_charge"]);
            int payAmount = Convert.ToInt32(initialCycles["amount"]);
            int count = Convert.ToInt32(initialCycles["count"]);

            if (!(payAmount is int))
            {
                throw new CustomException(ConstantsResponse.PLAN_INVALID_INITIAL_CYCLES_AMOUNT_IS_NOT_NUMBER);
            }

            if (hasInitialCharge)
            {
                ValidateCurrency(currency, amount);
                if (amount == payAmount)
                {
                    throw new CustomException(ConstantsResponse.PLAN_AMOUNT_PAY_AMOUNT_EQUAL);
                }

                if (count < ConstantsRequest.PUBLIC_COUNT_MIN || count > ConstantsRequest.PUBLIC_INTERVAL_COUNT_MAX)
                {
                    throw new CustomException(ConstantsResponse.PLAN_INVALID_INITIAL_CYCLES_RANGE);
                }

                if (payAmount < ConstantsRequest.PUBLIC_PLAN_INITIAL_CYCLE_MIN_AMOUNT || payAmount > ConstantsRequest.PUBLIC_PLAN_INITIAL_CYCLE_MAX_AMOUNT)
                {
                    throw new CustomException(ConstantsResponse.PLAN_INVALID_INITIAL_CYCLE_AMOUNT_RANGE);
                }
            }
            else
            {
                if (count < 0 || count > ConstantsRequest.PUBLIC_INTERVAL_COUNT_MAX)
                {
                    throw new CustomException(ConstantsResponse.PLAN_INITIAL_CYCLES_COUNT_NON_ZERO);
                }

                if (payAmount != 0)
                {
                    throw new CustomException(ConstantsResponse.PLAN_INITIAL_CYCLES_AMOUNT_NON_ZERO);
                }
            }
        }

        public static Exception ValidateMetadata(Dictionary<string, object> metadata)
        {
            // Permitir un diccionario vacío para el campo metadata
            if (metadata == null)
            {
                throw new CustomException("Enviaste el campo metadata con un formato incorrecto.");
            }

            if (!metadata.Any())
            {
                return null;
            }

            // Verificar límites de longitud de claves y valores
            Exception errorLength = ValidateKeyAndValueLength(metadata);
            if (errorLength != null)
            {
                throw new CustomException(errorLength.ToString());
            }

            // Convertir el diccionario transformado a JSON
            // try
            // {
            //     // JsonSerializer.Serialize(metadata);
            // }
            // catch (System.Text.Json.JsonException e)
            // {
            //     string errorMessage = $"Error al serializar el diccionario a JSON. Mensaje de error: {e.Message}";
            //     throw new CustomException(errorMessage);
            // }

            return null;
        }
        public static Exception ValidateKeyAndValueLength(Dictionary<string, object> objMetadata)
        {
            int maxKeyLength = 30;
            int maxValueLength = 200;

            foreach (var kvp in objMetadata)
            {
                string keyStr = kvp.Key.ToString();
                string valueStr = kvp.Value?.ToString();

                // Verificar límites de longitud de claves
                if (!(1 <= keyStr.Length && keyStr.Length <= maxKeyLength) ||
                    !(1 <= (valueStr?.Length ?? 0) && (valueStr?.Length ?? 0) <= maxValueLength))
                {
                    string errorMessage = $"El objeto 'metadata' es inválido, límite key (1 - {maxKeyLength}), value (1 - {maxValueLength}).";
                    throw new CustomException(errorMessage);
                }
            }

            return null;
        }

        public static void ValidateId(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 25)
            {
                throw new CustomException($"El campo 'id' es inválido. La longitud debe ser de 25 caracteres.");
            }
        }
    }
}
