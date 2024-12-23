﻿using culqi.net;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Security.Cryptography;


namespace culqi.net;

public class JsonData
{

    Util util = new Util();


    public Dictionary<string, object> JsonToken()
    {
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"card_number", "4111111111111111"},
                {"cvv", "123"},
                {"expiration_month", "09"},
                {"expiration_year", "2025"},
                {"email", "test.culqi@culqi.com"}
            };
        return map;
    }
    public Dictionary<string, object> JsonUpdateToken()
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };
        return map;
    }
    public Dictionary<string, object> JsonTokenYape()
    {
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", "600"},
                {"fingerprint", "12bff21638a4be364811b2919508e510"},
                {"number_phone", "900000001"},
                {"otp", "123232"}
            };
        return map;
    }

    public Dictionary<string, object> JsonCharge(string source_id)
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"order_id", "777"}
            };
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", 1000},
                {"capture", false},
                {"currency_code", "PEN"},
                {"description", "Venta de prueba"},
                {"email", "test"+GetRandomString()+"@culqi.com"},
                {"installments", 0},
                {"metadata", metadata},
                {"source_id", source_id}
            };
        return map;
    }

    public Dictionary<string, object> JsonUpdateCharge()
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };
        return map;
    }

    public Dictionary<string, object> JsonOrder()
    {
        int order_number = GetRandomNumber();
        TimeSpan t = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1);
        int secondsSinceEpoch = (int)t.TotalSeconds;

        Dictionary<string, object> client_details = new Dictionary<string, object>
            {
                {"first_name", "Juan"},
                {"last_name", "Diaz"},
                {"email", "juan.diaz@culqi.com"},
                {"phone_number", "+51948747421"},

            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", 1000},
                {"currency_code", "PEN"},
                {"description", "Venta de prueba"},
                {"order_number", "pedido"+Convert.ToString(order_number)},
                {"client_details", client_details},
                {"expiration_date", secondsSinceEpoch},
                {"confirm", true}

            };
        return map;
    }

    public Dictionary<string, object> JsonConfirmOrder(string order_id)
    {
        string[] order_types = { "cuotealo", "cip" };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"order_id", order_id},
                {"order_types", order_types}
            };
        return map;
    }
    public Dictionary<string, object> JsonUpdateOrder()
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };
        return map;
    }

    public Dictionary<string, object> JsonCustomHeader()
    {
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"X-Charge-Channel", "recurrent"},
                {"X-add-header", null},
                {"X-config-valid", "  "},
            };

        return map;
    }

    public Dictionary<string, object> JsonPlan()
    {
        Dictionary<string, object> initialCycles = new Dictionary<string, object>
            {
                {"count", 0},
                {"has_initial_charge", false},
                {"amount", 0},
                {"interval_unit_time", 1}
            };

        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"DNI", 123456782}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"name", "CY PCI -PLAN-"+util.GetRandomString()},
                {"short_name", "cp-prueba"},
                {"description", "Cypress PCI"},
                {"amount", 300},
                {"currency", "PEN"},
                {"interval_unit_time", 1},
                {"interval_count", 1},
                {"initial_cycles", initialCycles},
                {"metadata", metadata}
            };

        return map;
    }

    // public Dictionary<string, object> JsonUpdatePlan()
    // {
    //     Dictionary<string, object> metadata = new Dictionary<string, object>
    //         {
    //             {"abc", "555"}
    //         };

    //     Dictionary<string, object> map = new Dictionary<string, object>
    //         {
    //             {"metadata", metadata}
    //         };
    //     return map;
    // }
    public Dictionary<string, object> JsonUpdatePlan()
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"},
                {"123456789012345678901234567890", "555"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"status", 1},
                {"short_name", "cp-prueba"},
                {"description", "Cypress PCI"},
                {"name", "CY PCI -PLAN-"+util.GetRandomString()},
                {"metadata", metadata},
            };
        return map;
    }
    public Dictionary<string, object> JsonCustomer()
    {
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"address", "Av Lima 123"},
                {"address_city", "Lima"},
                {"country_code", "PE"},
                {"email", "test"+GetRandomString()+"@culqi.com"},
                {"first_name", "Test"},
                {"last_name", "Culqi"},
                {"phone_number", "99004356"}
            };
        return map;
    }

    public Dictionary<string, object> JsonCard(string customer_id, string token_id)
    {
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"customer_id", customer_id},
                {"token_id", token_id}
            };
        return map;
    }
    public Dictionary<string, object> JsonUpdateCard()
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };
        return map;
    }

    public Dictionary<string, object> JsonSubscription(string card_id, string plan_id)
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"envTest", "Autogenerado de Cypress"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"card_id", card_id},
                {"plan_id", plan_id},
                {"metadata", metadata},
                {"tyc", true},
            };

        return map;
    }

    public Dictionary<string, object> JsonUpdateSubscription(string card_id)
    {
        Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"},
                {"123456789012345678901234567890", "555"}
            };

        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"card_id", card_id},
                {"metadata", metadata}
            };
        return map;
    }

    public Dictionary<string, object> JsonRefund(string charge_id)
    {
        Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", 500},
                {"charge_id", charge_id},
                {"reason", "solicitud_comprador"}
            };
        return map;
    }
    public int GetRandomNumber()
    {
        using (RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider())
        {
            byte[] tokenBuffer = new byte[6];       // `int32` takes 4 bytes in C#
            rngCrypt.GetBytes(tokenBuffer);
            return BitConverter.ToInt32(tokenBuffer, 0);
        }
    }

    public string GetRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", "");
        return path;
    }
}