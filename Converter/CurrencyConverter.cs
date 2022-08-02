using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static LogicLoopTask.Models.ConverterModel;
namespace LogicLoopTask.Converter
{
    public class CurrencyConverter
    {
        public static string Converter(string amount,string from,string to)
        {
            var client = new RestClient("https://api.apilayer.com/fixer/convert?to=" + to + "&from=" + from + "&amount=" + amount + "");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "fAyOHVcWUgAauZXS7vMrplQRX5ty0D89");

            //Sending Request to fixer API for getting response in convert currency
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                //Deserialize into model class
                var rootModel = JsonConvert.DeserializeObject<Root>(response.Content);

                //Deserialize into jsonObject (For return specific property)
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Content);
                var cAmt = jsonObject.Value<string>("result");  //return specific value based on json object property 

                if (!string.IsNullOrEmpty(cAmt) && jsonObject.Value<string>("success") == "True")
                {
                    //Concatinate all return response into specific result
                    string result = amount + "" + from + " in " + cAmt + "" + to;

                    return result;
                }
            }
            return null;
           
        }
       
    }
}
