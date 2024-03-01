using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCulqi.util
{
    public static class SubscriptionValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            List<string> requiredPayload = new List<string> { "card_id", "plan_id", "tyc" };
            Exception resultValidation = Helper.AdditionalValidation(data, requiredPayload);
            if (resultValidation != null)    
            {
                throw new CustomException(resultValidation.Message);
            }
            else
            {
            if (data.ContainsKey("card_id"))
            {
                object card_id = data["card_id"];
                Helper.ValidateNotNull(card_id, ConstantsResponse.SUBSCRIPTION_INVALID_CARD_REQUIERD);
                Helper.ValidateTypeString(card_id, ConstantsResponse.SUBSCRIPTION_INVALID_CARD_TYPE);
                Helper.ValidateLengthSizeValue(data["card_id"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.SUBSCRIPTION_INVALID_CARD_RANGE);
            }
            if (data.ContainsKey("plan_id"))
            {
                object plan_id = data["plan_id"];
                Helper.ValidateNotNull(plan_id, ConstantsResponse.PLAN_INVALID_PLAN_ID_REQUIRED);
                Helper.ValidateTypeString(plan_id, ConstantsResponse.SUBSCRIPTION_INVALID_PLAN_TYPE);
                Helper.ValidateLengthSizeValue(data["plan_id"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.SUBSCRIPTION_INVALID_PLAN_ID_LENGTH);
            }
            if (data.ContainsKey("tyc"))
            {
                object tyc = data["tyc"];
                Helper.ValidateNotNull(tyc, ConstantsResponse.SUBSCRIPTION_INVALID_TYC_REQUIRED);
                Helper.ValidateTypeBool(tyc, ConstantsResponse.SUBSCRIPTION_INVALID_TYC);
            }
            if (data.ContainsKey("metadata"))
            {
                object metadata = data["metadata"];
                Helper.ValidateMetadataSchema(metadata);
            }
            // if (data.ContainsKey("metadata"))
            //     {
            //         Dictionary<string, object> metadata = data["metadata"] as Dictionary<string, object>;
            //         Helper.ValidateMetadata(metadata);
            //     }
            }
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("status"))
            {
                object status = data["status"];
                Helper.ValidateTypeInt(status, ConstantsResponse.SUBSCRIPTION_INVALID_TYPE_STATUS);
                Helper.ValidateEnumsIntValue(status, ConstantsRequest.VALID_SUBSCRIPTION_STATUS, ConstantsResponse.SUBSCRIPTION_INVALID_STATUS);

            }
            if (data.ContainsKey("plan_id"))
            {
                object plan_id = data["plan_id"];
                Helper.ValidateNotNull(plan_id, ConstantsResponse.PLAN_INVALID_PLAN_ID_REQUIRED);
                Helper.ValidateTypeString(plan_id, ConstantsResponse.SUBSCRIPTION_INVALID_PLAN_TYPE);
                Helper.ValidateLengthSizeValue(data["plan_id"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.SUBSCRIPTION_INVALID_PLAN_ID_LENGTH);
            }
            if (data.ContainsKey("creation_date_from"))
            {
                if (!(data["creation_date_from"] is string) || ((string)data["creation_date_from"]).Length != 10 && ((string)data["creation_date_from"]).Length != 13)
                {
                    throw new CustomException(ConstantsResponse.SUBSCRIPTION_INVALID_LENGTH_CREATION_DATE_FROM_FILTER_PUBLIC_API);
                }
            }
            if (data.ContainsKey("creation_date_to"))
            {
                if (!(data["creation_date_to"] is string) || ((string)data["creation_date_to"]).Length != 10 && ((string)data["creation_date_to"]).Length != 13)
                {
                    throw new CustomException(ConstantsResponse.SUBSCRIPTION_INVALID_LENGTH_CREATION_DATE_TO_FILTER_PUBLIC_API);
                }
            }
            if(data.ContainsKey("limit"))
            {
                object limit = data["limit"];
                Helper.ValidateTypeInt(limit, ConstantsResponse.PLAN_INVALID_LIMIT_FILTER);
                Helper.ValidateMin(limit, ConstantsRequest.MIN_LIMIT, ConstantsResponse.PLAN_LIMIT_FILTER_RANGE);
                Helper.ValidateMax(limit, ConstantsRequest.MAX_LIMIT, ConstantsResponse.PLAN_LIMIT_FILTER_RANGE);

            }
            if(data.ContainsKey("before"))
            {
                object before = data["before"];
                Helper.ValidateTypeString(before, ConstantsResponse.SUBSCRIPTION_INVALID_TYPE_BEFORE);
                Helper.ValidateLengthSizeValue(data["before"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.SUBSCRIPTION_INVALID_RANGE_BEFORE);
                
            }
            if(data.ContainsKey("after"))
            {
                object after = data["after"];
                Helper.ValidateTypeString(after, ConstantsResponse.SUBSCRIPTION_INVALID_TYPE_AFTER);
                Helper.ValidateLengthSizeValue(data["before"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.SUBSCRIPTION_INVALID_RANGE_AFTER);
            }
        }

        public static void Update(Dictionary<string, object> data)
        {
            if (data.ContainsKey("card_id"))
            {
                object card_id = data["card_id"];
                Helper.ValidateNotNull(card_id, ConstantsResponse.SUBSCRIPTION_INVALID_CARD_REQUIERD);
                Helper.ValidateTypeString(card_id, ConstantsResponse.SUBSCRIPTION_INVALID_CARD_TYPE);
                Helper.ValidateLengthSizeValue(data["card_id"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.SUBSCRIPTION_INVALID_CARD_RANGE);
            }
            if (data.ContainsKey("metadata"))
            {
                object metadata = data["metadata"];
                Helper.ValidateMetadataSchema(metadata);
            }
            // if (data.ContainsKey("metadata"))
            //     {
            //         Dictionary<string, object> metadata = data["metadata"] as Dictionary<string, object>;
            //         Helper.ValidateMetadata(metadata);
            //     }
        }
    }
}
