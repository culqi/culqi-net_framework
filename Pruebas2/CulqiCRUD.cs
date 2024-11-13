﻿using culqi.net;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;

namespace culqi.net
{
    public class CulqiCRUD
    {

        Security security = null;
        JsonData jsonData = new JsonData();

        public CulqiCRUD()
        {
            security = new Security();
            security.public_key = "pk_live_575c057b753c6675";  
            security.secret_key = "sk_live_9100c79adc0e1b26";
            security.rsa_id = "1fb81326-e909-4b84-96d5-f91377edbf07";
            security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDUqR6drBDE7YFPDjCNgMD22X1v3zkF5Q+JSoEv5HhFfcDWfoGV0Qn9c64/9sxZpgeaT4yEiEDhpiewme/Rv94DKTmBO5KiphHg2b3kZW1Bm5EeBkcJoH4hnMJO54+7HZGXsPGKSND3gcuJ8jVySCWRu07a71Lr8X9NNQhVMqRqCQIDAQAB";
        }

        //create
        public HttpResponseMessage CreateToken()
        {
            return new Token(security).Create(jsonData.JsonToken());
        }

        public HttpResponseMessage CreateTokenEncrypt()
        {
            return new Token(security).Create(jsonData.JsonToken(), security.rsa_id, security.rsa_key);
        }

        public HttpResponseMessage CreateTokenYape()
        {
            return new Token(security).CreateYape(jsonData.JsonTokenYape());
        }
        
        public HttpResponseMessage CreateCharge()
        {
            HttpResponseMessage data = CreateToken();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]));

        }

        public HttpResponseMessage CreateChargeWithCustomHeader()
        {
            HttpResponseMessage data = CreateToken();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]), jsonData.JsonCustomHeader());
        }

        public HttpResponseMessage UpdateCharge()
        {
            HttpResponseMessage data = CreateCharge();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Charge(security).Update(jsonData.JsonUpdateCharge(), (string)json_object["id"]);
        }

        public HttpResponseMessage CreateChargeEncrypt()
        {
            HttpResponseMessage data = CreateToken();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]), security.rsa_id, security.rsa_key);
        }

        public HttpResponseMessage CreateChargeEncryptWithCustomHeader()
        {
            HttpResponseMessage data = CreateToken();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]), security.rsa_id, security.rsa_key, jsonData.JsonCustomHeader());
        }

        public HttpResponseMessage CreateChargeCapture()
        {
            HttpResponseMessage charge_data = CreateCharge();

            var json_charge = JObject.Parse(charge_data.Content.ReadAsStringAsync().Result);

            return new Charge(security).Capture((string)json_charge["id"]);
        }

        public HttpResponseMessage CreateOrder()
        {
            return new Order(security).Create(jsonData.JsonOrder());
        }
        public HttpResponseMessage ConfirmOrder()
        {
            HttpResponseMessage data = CreateOrder();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            return new Order(security).Create(jsonData.JsonConfirmOrder((string)json_object["id"]));
        }
        public HttpResponseMessage UpdateOrder()
        {
            HttpResponseMessage data = CreateOrder();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Order(security).Update(jsonData.JsonUpdateOrder(), (string)json_object["id"]);
        }

        public HttpResponseMessage CreateOrderEncrypt()
        {
            return new Order(security).Create(jsonData.JsonOrder(), security.rsa_id, security.rsa_key);

        }

        public HttpResponseMessage CreatePlan()
        {
            return new Plan(security).Create(jsonData.JsonPlan());
        }
        public HttpResponseMessage UpdatePlan()
        {
            HttpResponseMessage data = CreatePlan();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Plan(security).Update(jsonData.JsonUpdatePlan(), (string)json_object["id"]);
        }
        public HttpResponseMessage CreateCustomer()
        {
            return new Customer(security).Create(jsonData.JsonCustomer());
        }

        public HttpResponseMessage CreateCard()
        {
            HttpResponseMessage token = CreateToken();
            HttpResponseMessage customer = CreateCustomer();
            
            var json_token = JObject.Parse(token.Content.ReadAsStringAsync().Result);
            var json_customer = JObject.Parse(customer.Content.ReadAsStringAsync().Result);

            return new Card(security).Create(jsonData.JsonCard((string)json_customer["id"], (string)json_token["id"]));
        }

        public HttpResponseMessage UpdateCard()
        {
            HttpResponseMessage data = CreateCard();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Card(security).Update(jsonData.JsonUpdateCard(), (string)json_object["id"]);
        }

        public HttpResponseMessage CreateSubscription()
        {
            HttpResponseMessage plan_data = CreatePlan();
            var json_plan = JObject.Parse(plan_data.Content.ReadAsStringAsync().Result);

            HttpResponseMessage card_data = CreateCard();
            var json_card = JObject.Parse(card_data.Content.ReadAsStringAsync().Result);

            Console.WriteLine((string)json_card["id"]);
            Console.WriteLine((string)json_plan["id"]);
            return new Subscription(security).Create(jsonData.JsonSubscription((string)json_card["id"], (string)json_plan["id"]));
        }

        public HttpResponseMessage UpdateSubscription()
        {
            HttpResponseMessage data = CreateSubscription();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Console.WriteLine(json_object);
            HttpResponseMessage card_data = CreateCard();
            var json_card = JObject.Parse(card_data.Content.ReadAsStringAsync().Result);

            return new Subscription(security).Update(jsonData.JsonUpdateSubscription((string)json_card["id"]), (string)json_object["id"]);
        }
        public HttpResponseMessage CreateRefund()
        {
            HttpResponseMessage data = CreateCharge();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            return new Refund(security).Create(jsonData.JsonRefund((string)json_object["id"]));
        }

        //find

        public HttpResponseMessage GetToken(string id)
        {
            return new Token(security).Get(id);
        }

        public HttpResponseMessage GetOrder(string id)
        {
            return new Order(security).Get(id);
        }

        public HttpResponseMessage GetCharge(string id)
        {
            return new Charge(security).Get(id);
        }

        public HttpResponseMessage GetPlan(string id)
        {
            return new Plan(security).Get(id);
        }
        public HttpResponseMessage GetCustomer(string id)
        {
            return new Customer(security).Get(id);
        }
        public HttpResponseMessage GetCard(string id)
        {
            return new Card(security).Get(id);
        }
        public HttpResponseMessage GetSubscription(string id)
        {
            return new Subscription(security).Get(id);
        }

        public HttpResponseMessage GetRefund(string id)
        {
            return new Refund(security).Get(id);
        }

        //Delete

        public HttpResponseMessage DeleteSubscription(string id)
        {
            return new Subscription(security).Delete(id);
        }
        public HttpResponseMessage DeleteCard(string id)
        {
            return new Card(security).Delete(id);
        }
        public HttpResponseMessage DeleteCustomer(string id)
        {
            return new Customer(security).Delete(id);
        }
        public HttpResponseMessage DeletePlan(string id)
        {
            return new Plan(security).Delete(id);
        }
    }
}