using System;
using System.Collections.Generic;
using System.Linq;
using ApiCallTest;
using Newtonsoft.Json;
using RestSharp;
using static ApiCallTest.JsonOpenWeatherMap;
using static ApiCallTest.output;

namespace ApiCallTest
{
    class Program
    {

        
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Start();
        }

        public void Start()
        {
            JsonOpenWeatherMap.RootObject rootObject = getData();
            var data = ProcessData(rootObject);
            OutPutData(data);
        }

        public JsonOpenWeatherMap.RootObject getData()
        {
            var client = new RestClient("http://api.openweathermap.org/data/2.5/group?id=4887398,4684888,5809844,5419384,5128581,5780993,5506956,5391959,4930956,4180439&appid=30b3db4152f8ddd4bd894e9ea5be2246&units=imperial");
            JsonOpenWeatherMap.RootObject data = new JsonOpenWeatherMap.RootObject();
            var request = new RestRequest();
            request.Method = Method.GET;

            var responce = client.Execute(request);
            data = JsonConvert.DeserializeObject<JsonOpenWeatherMap.RootObject>(responce.Content);
            return data;
        }

        public output.RootObject ProcessData(JsonOpenWeatherMap.RootObject data)
        {
            var output = new output.RootObject();
            output.CitiesWeatherDetails = new List<CityWeatherDetail>();
            output.count = data.cnt;
            foreach(List line in data.list)
            {
                var CWD = new CityWeatherDetail
                {
                    Id = line.id,
                    Name = line.name,
                    WeatherDetails = new WeatherDetails
                    {
                        Longitude = line.coord.lon,
                        Latitude = line.coord.lat,
                        MinTemp = line.main.temp_min,
                        MaxTemp = line.main.temp_max
                    }

                };
                output.CitiesWeatherDetails.Add(CWD);
            }
            output.CitiesWeatherDetails = output.CitiesWeatherDetails.OrderByDescending(w => w.WeatherDetails.MaxTemp).ToList();
            return output;
        }
        public void OutPutData(output.RootObject data)
        {
            var Json = JsonConvert.SerializeObject(data);

            Console.Write(Json);
            Console.Read();
        }
    }
}
