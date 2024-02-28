public static class ConstantsResponse
{
    public const string PLAN_INTERVAL_UNIT_TIME_REQUIRED = "El campo 'interval_unit_time' es requerido.";
    public const string PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM = "El campo 'interval_unit_time' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos:1 ,2 ,3 ,4 ,5 ,6 .";
    public const string PLAN_INTERVAL_COUNT_REQUIRED = "El campo 'interval_count' es requerido.";
    public const string PLAN_INVALID_INTERVAL_COUNT = "El campo 'interval_count' es inválido o está vacío, debe tener un valor numérico.";
    public const string PLAN_AMOUNT_REQUIRED = "El campo 'amount' es requerido";
    public const string PLAN_INVALID_AMOUNT = "El campo 'amount' es inválido o está vacío, debe tener un valor numérico";
    public const string PLAN_INVALID_NAME_REQUIRED = "El campo 'name' es requerido.";
    public const string PLAN_INVALID_NAME = "El campo 'name' es inválido o está vacío, debe ser una cadena.";
    public const string PLAN_INVALID_NAME_RANGE = "El campo 'name' es inválido o está vacío. El valor debe tener un rango de 5 a 50. ";
    public const string PLAN_DESCRIPTION_REQUIRED = "El campo 'description' es requerido.";
    public const string PLAN_INVALID_DESCRIPTION = "El campo 'description' es inválido o está vacío, debe ser una cadena.";
    public const string PLAN_INVALID_DESCRIPTION_RANGE = "El campo 'description' es inválido o está vacío. El valor debe tener un rango de 5 a 250.";
    public const string PLAN_SHORT_NAME_REQUIRED = "El campo 'short_name' es requerido";
    public const string PLAN_INVALID_SHORT_NAME = "El campo 'short_name' es inválido o está vacío, debe ser una cadena.";
    public const string PLAN_INVALID_SHORT_NAME_RANGE = "El campo 'short_name' es inválido o está vacío. El valor debe tener un rango de 5 a 50 caracteres.";
    public const string PLAN_CURRENCY_REQUIRED = "El campo 'currency' es requerido.";
    public const string PLAN_INVALID_CURRENCY_ENUM = "El campo 'currency' es inválido o está vacío, el código de la moneda en tres letras (Formato ISO 4217). Culqi actualmente soporta las siguientes monedas: 'PEN' y 'USD' . ";
    public const string PLAN_INVALID_IMAGE = "El campo 'image' es inválido o está vacío. El valor debe ser una cadena y debe ser una URL válida."; 
    public const string PLAN_INVALID_IMAGE_RANGE = "El campo 'image' es inválido o está vacío. El valor debe tener un rango de 5 a 250 caracteres.";
    public const string PLAN_INVALID_INTERVAL_COUNT_RANGE = "El campo 'interval_count' solo admite valores numéricos en el rango 0 a 9999. ";
    public const string PLAN_INVALID_MIN_AMOUNT_FILTER_PUBLIC_API = "El filtro 'min_amount' es inválido o está vacío, debe tener un valor numérico.";
    public const string PLAN_MIN_AMOUNT_FILTER_RANGE_PUBLIC_API = "El filtro 'min_amount' admite valores en el rango 300 a 500000. ";
    public const string PLAN_INVALID_MAX_AMOUNT_FILTER_PUBLIC_API = "El filtro 'max_amount' es inválido o está vacío, debe tener un valor numérico. ";
    public const string PLAN_MAX_AMOUNT_FILTER_RANGE_PUBLIC_API = "El filtro 'max_amount' admite valores en el rango 300 a 500000. ";

    public const string PLAN_INVALID_CREATION_DATE_FROM_RANGE_PUBLIC_API = "El campo 'creation_date_from' debe tener una longitud de 10 o 13 caracteres. ";
    public const string PLAN_INVALID_CREATION_DATE_TO_RANGE_PUBLIC_API = "El campo 'creation_date_to' debe tener una longitud de 10 o 13 caracteres. ";
    public const string PLAN_INVALID_LIMIT_FILTER = "El filtro 'limit' es inválido o está vacío, debe tener un valor numérico. ";
    public const string PLAN_LIMIT_FILTER_RANGE = "El filtro 'limit' admite valores en el rango 1 a 100 .";
    public const string PLAN_INVALID_BEFORE_FILTER = "El campo 'before' es inválido. La longitud debe ser de 25 caracteres.";
    public const string PLAN_INVALID_AFTER_FILTER = "El campo 'after' es inválido. La longitud debe ser de 25 caracteres. ";
    public const string PLAN_INVALID_PLAN_ID_REQUIRED = "El campo 'plan_id' es requerido.";
    public const string SUBSCRIPTION_INVALID_PLAN_TYPE = "El campo 'plan_id' es inválido. La longitud debe ser de 25 caracteres";
    public const string SUBSCRIPTION_INVALID_TYC_REQUIRED = "El campo 'tyc' es requerido.";
    public const string SUBSCRIPTION_INVALID_TYC = "El campo 'tyc' es inválido o está vacío. El valor debe ser un booleano";
    public const string SUBSCRIPTION_INVALID_CARD_RANGE = "El campo 'card_id' debe tener una longitud 25.";
    public const string SUBSCRIPTION_INVALID_PLAN_ID_LENGTH = "El campo 'plan_id' es inválido. La longitud debe ser de 25 caracteres.";
    public const string SUBSCRIPTION_INVALID_TYPE_STATUS = "El campo 'status' es inválido o está vacío, debe ser un valor numérico.";
    public const string SUBSCRIPTION_INVALID_STATUS = "El campo 'status' es inválido. Estos son los únicos valores permitidos: 1, 2, 3, 4, 5, 6, 8 .";
    public const string SUBSCRIPTION_INVALID_LENGTH_CREATION_DATE_FROM_FILTER_PUBLIC_API = "El campo 'creation_date_from' debe tener una longitud de 10 o 13 caracteres.";
    public const string SUBSCRIPTION_INVALID_LENGTH_CREATION_DATE_TO_FILTER_PUBLIC_API = "El campo 'creation_date_to' debe tener una longitud de 10 o 13 caracteres.";
    public const string SUBSCRIPTION_INVALID_TYPE_BEFORE = "El campo 'before' es inválido o está vacío, debe ser una cadena.";
    public const string SUBSCRIPTION_INVALID_RANGE_BEFORE = "El campo 'before' debe tener una longitud de 25 caracteres. ";
    public const string SUBSCRIPTION_INVALID_TYPE_AFTER = "El campo 'after' es inválido o está vacío, debe ser una cadena.";
    public const string SUBSCRIPTION_INVALID_RANGE_AFTER = "El campo 'after' debe tener una longitud de ${FieldsLength.GENERATED_ID} caracteres.";
    public const string SUBSCRIPTION_INVALID_CARD_REQUIERD = "El campo 'card_id' es requerido.";
    public const string SUBSCRIPTION_INVALID_CARD_TYPE = "El campo 'card_id' es inválido o está vacío, debe ser una cadena";
    public const string PLAN_INVALID_AMOUNT_RANGE_USD = "El campo 'amount' admite valores en el rango 1 a 1500. ";
    public const string PLAN_INVALID_AMOUNT_RANGE_PEN = "El campo 'amount' admite valores en el rango 300 a 500000. ";
    public const string PLAN_INVALID_INITIAL_CYCLES_AMOUNT_IS_NOT_NUMBER = "El campo 'initial_cycles.amount' es inválido o está vacío, debe tener un valor numérico. ";
    public const string PLAN_AMOUNT_PAY_AMOUNT_EQUAL = "El campo 'initial_cycles.amount' es inválido o está vacío. El valor no debe ser igual al monto del plan.";
    public const string PLAN_INVALID_INITIAL_CYCLES_RANGE = "El campo 'initial_cycles.count' solo admite valores numéricos en el rango de 1 a 9999. ";
    public const string PLAN_INVALID_INITIAL_CYCLE_AMOUNT_RANGE = "El campo 'initial_cycles.amount' admite valores en el rango 300 a 5000000. ";
    public const string PLAN_INITIAL_CYCLES_COUNT_NON_ZERO = "El campo 'initial_cycles.count' solo admite valores numéricos en el rango 0 a 9999. ";
    public const string PLAN_INITIAL_CYCLES_AMOUNT_NON_ZERO = "El campo 'initial_cycles.amount' es inválido, debe ser 0.";
    public const string METADATA_INVALID = "Enviaste el campo metadata con un formato incorrecto. "; 
    public const string METADATA_LIMIT_20 = "Enviaste más de 20 parámetros en el metadata. El límite es 20.";
    public const string METADATA_LIMIT_KEY_30_CHARACTERS_RF = "El objeto 'metadata' es inválido, límite key (1 - 30), value (1 - 200)";
    public const string PLAN_UPDATE_INVALID_STATUS_ENUM = "El campo 'status' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos: 1 y 2";

    public const string PLAN_INVALID_STATUS_FILTER = "El filtro 'status' tiene un valor inválido o está vacío, debe tener un valor numérico.";
    public const string PLAN_INVALID_STATUS_FILTER_ENUM = "El filtro 'status' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos: 1, 2.";

}