﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCulqi.util
{
    public static class PlanValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            if(data.ContainsKey("interval_unit_time")){
                object internalUnitTime = data["interval_unit_time"];
                Helper.ValidateNotNull(internalUnitTime, ConstantsResponse.PLAN_INTERVAL_UNIT_TIME_REQUIRED);
                Helper.ValidateTypeInt(internalUnitTime, ConstantsResponse.PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM);
                Helper.ValidateEnumsIntValue(internalUnitTime, ConstantsRequest.INTERVAL_UNIT_TIME_ENUM, ConstantsResponse.PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM);
            }
            if(data.ContainsKey("interval_count")){
                object internalCount = data["interval_count"];
                Helper.ValidateNotNull(internalCount, ConstantsResponse.PLAN_INTERVAL_COUNT_REQUIRED);
                Helper.ValidateTypeInt(internalCount, ConstantsResponse.PLAN_INVALID_INTERVAL_COUNT);
                Helper.ValidateMin(internalCount, ConstantsRequest.PUBLIC_INTERVAL_COUNT_MIN, ConstantsResponse.PLAN_INVALID_INTERVAL_COUNT_RANGE);
                Helper.ValidateMax(internalCount, ConstantsRequest.PUBLIC_INTERVAL_COUNT_MAX, ConstantsResponse.PLAN_INVALID_INTERVAL_COUNT_RANGE);
            }
            if(data.ContainsKey("amount")){
                object amount = data["amount"];
                Helper.ValidateNotNull(amount, ConstantsResponse.PLAN_AMOUNT_REQUIRED);
                Helper.ValidateTypeInt(amount, ConstantsResponse.PLAN_INVALID_AMOUNT);

            }
            if(data.ContainsKey("name")){
                object name = data["name"];
                Helper.ValidateNotNull(name, ConstantsResponse.PLAN_INVALID_NAME_REQUIRED);
                Helper.ValidateTypeString(name, ConstantsResponse.PLAN_INVALID_NAME);
                Helper.ValidateMin(name, ConstantsRequest.MIN_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_NAME_RANGE);
                Helper.ValidateMax(name, ConstantsRequest.MAX_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_NAME_RANGE);
            }
            if(data.ContainsKey("description")){
                object description = data["description"];
                Helper.ValidateNotNull(description, ConstantsResponse.PLAN_DESCRIPTION_REQUIRED);
                Helper.ValidateTypeString(description, ConstantsResponse.PLAN_INVALID_DESCRIPTION);
                Helper.ValidateMin(description, ConstantsRequest.MIN_LENGTH_DESCRIPTION, ConstantsResponse.PLAN_INVALID_DESCRIPTION_RANGE);
                Helper.ValidateMax(description, ConstantsRequest.MAX_LENGTH_DESCRIPTION, ConstantsResponse.PLAN_INVALID_DESCRIPTION_RANGE);
            }
            if(data.ContainsKey("short_name")){
                object short_name = data["short_name"];
                Helper.ValidateNotNull(short_name, ConstantsResponse.PLAN_SHORT_NAME_REQUIRED);
                Helper.ValidateTypeString(short_name, ConstantsResponse.PLAN_INVALID_SHORT_NAME);
                Helper.ValidateMin(short_name, ConstantsRequest.MIN_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_SHORT_NAME_RANGE);
                Helper.ValidateMax(short_name, ConstantsRequest.MAX_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_SHORT_NAME_RANGE);
            }
            if(data.ContainsKey("currency")){
                object currency = data["currency"];
                Helper.ValidateNotNull(currency, ConstantsResponse.PLAN_CURRENCY_REQUIRED);
                Helper.ValidateEnumsStringValue(data["currency"] as string, ConstantsRequest.CURRENCY_ENUM, ConstantsResponse.PLAN_INVALID_CURRENCY_ENUM);
            }
            if(data.ContainsKey("image")){
                object image = data["image"];
                Helper.ValidateTypeString(image, ConstantsResponse.PLAN_INVALID_IMAGE);
                Helper.ValidateMin(image, ConstantsRequest.MIN_IMAGE_LENGTH, ConstantsResponse.PLAN_INVALID_IMAGE_RANGE);
                Helper.ValidateMax(image, ConstantsRequest.MAX_IMAGE_LENGTH, ConstantsResponse.PLAN_INVALID_IMAGE_RANGE);
                Helper.ValidateRegex(data["image"] as string, ConstantsRequest.REGEX_IMAGE_URL, ConstantsResponse.PLAN_INVALID_IMAGE);
            }
            if(data.ContainsKey("initial_cycles")){
                Dictionary<string, object> initialCyclesData = data["initial_cycles"] as Dictionary<string, object>;
                Helper.ValidateInitialCycles(initialCyclesData, data["currency"].ToString(), Convert.ToInt32(data["amount"]));
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

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("status"))
            {
                object status = data["status"];
                Helper.ValidateTypeInt(status, ConstantsResponse.PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM);
                Helper.ValidateEnumsIntValue(status, ConstantsRequest.VALID_PLAN_STATUS, ConstantsResponse.PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM);

            }
            if (data.ContainsKey("min_amount"))
            {
                object min_amount = data["min_amount"];
                Helper.ValidateAmountValueWithError(min_amount, ConstantsResponse.PLAN_INVALID_MIN_AMOUNT_FILTER_PUBLIC_API);
                Helper.ValidateMin(min_amount, ConstantsRequest.MIN_AMOUNT_PEN * 100, ConstantsResponse.PLAN_MIN_AMOUNT_FILTER_RANGE_PUBLIC_API);
                Helper.ValidateMax(min_amount, ConstantsRequest.MAX_AMOUNT_PEN * 100, ConstantsResponse.PLAN_MIN_AMOUNT_FILTER_RANGE_PUBLIC_API);
            }
            if (data.ContainsKey("max_amount"))
            {
                object max_amount = data["max_amount"];
                Helper.ValidateAmountValueWithError(max_amount, ConstantsResponse.PLAN_INVALID_MAX_AMOUNT_FILTER_PUBLIC_API);
                Helper.ValidateMin(max_amount, ConstantsRequest.MIN_AMOUNT_PEN * 100, ConstantsResponse.PLAN_MAX_AMOUNT_FILTER_RANGE_PUBLIC_API);
                Helper.ValidateMax(max_amount, ConstantsRequest.MAX_AMOUNT_PEN * 100, ConstantsResponse.PLAN_MAX_AMOUNT_FILTER_RANGE_PUBLIC_API);
            }
            if (data.ContainsKey("creation_date_from"))
            {
                if (!(data["creation_date_from"] is string) || ((string)data["creation_date_from"]).Length != 10 && ((string)data["creation_date_from"]).Length != 13)
                {
                    throw new CustomException(ConstantsResponse.PLAN_INVALID_CREATION_DATE_FROM_RANGE_PUBLIC_API);
                }
            }
            if (data.ContainsKey("creation_date_to"))
            {
                if (!(data["creation_date_to"] is string) || ((string)data["creation_date_to"]).Length != 10 && ((string)data["creation_date_to"]).Length != 13)
                {
                    throw new CustomException(ConstantsResponse.PLAN_INVALID_CREATION_DATE_TO_RANGE_PUBLIC_API);
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
                Helper.ValidateLengthSizeValue(data["before"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.PLAN_INVALID_BEFORE_FILTER);
            }
            if(data.ContainsKey("after"))
            {
                Helper.ValidateLengthSizeValue(data["after"] as string, ConstantsRequest.GENERATED_ID, ConstantsResponse.PLAN_INVALID_AFTER_FILTER);
            }
        }

        public static void Update(Dictionary<string, object> data)
        {
            if(data.ContainsKey("name")){
                object name = data["name"];
                Helper.ValidateTypeString(name, ConstantsResponse.PLAN_INVALID_NAME);
                Helper.ValidateMin(name, ConstantsRequest.MIN_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_NAME_RANGE);
                Helper.ValidateMax(name, ConstantsRequest.MAX_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_NAME_RANGE);
            }
            if(data.ContainsKey("description")){
                object description = data["description"];
                Helper.ValidateTypeString(description, ConstantsResponse.PLAN_INVALID_DESCRIPTION);
                Helper.ValidateMin(description, ConstantsRequest.MIN_LENGTH_DESCRIPTION, ConstantsResponse.PLAN_INVALID_DESCRIPTION_RANGE);
                Helper.ValidateMax(description, ConstantsRequest.MAX_LENGTH_DESCRIPTION, ConstantsResponse.PLAN_INVALID_DESCRIPTION_RANGE);

            }
            if(data.ContainsKey("short_name")){
                object short_name = data["short_name"];
                Helper.ValidateTypeString(short_name, ConstantsResponse.PLAN_INVALID_SHORT_NAME);
                Helper.ValidateMin(short_name, ConstantsRequest.MIN_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_SHORT_NAME_RANGE);
                Helper.ValidateMax(short_name, ConstantsRequest.MAX_LENGTH_PLAN_NAME, ConstantsResponse.PLAN_INVALID_SHORT_NAME_RANGE);

            }
            if(data.ContainsKey("status")){
                object status = data["status"];
                Helper.ValidateTypeInt(status, ConstantsResponse.PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM);
                Helper.ValidateEnumsIntValue(status, ConstantsRequest.VALID_PLAN_STATUS, ConstantsResponse.PLAN_INVALID_INTERVAL_UNIT_TIME_ENUM);
            }
            if(data.ContainsKey("image")){
                object image = data["image"];
                Helper.ValidateTypeString(image, ConstantsResponse.PLAN_INVALID_IMAGE);
                Helper.ValidateMin(image, ConstantsRequest.MIN_IMAGE_LENGTH, ConstantsResponse.PLAN_INVALID_IMAGE_RANGE);
                Helper.ValidateMax(image, ConstantsRequest.MAX_IMAGE_LENGTH, ConstantsResponse.PLAN_INVALID_IMAGE_RANGE);
                Helper.ValidateRegex(data["image"] as string, ConstantsRequest.REGEX_IMAGE_URL, ConstantsResponse.PLAN_INVALID_IMAGE);
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
