using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCallTest
{
    class output
    {
        public class RootObject
        {
            public int count { get; set; }
            public List<CityWeatherDetail> CitiesWeatherDetails { get; set; }
        }
        public class CityWeatherDetail
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public WeatherDetails WeatherDetails { get; set; }
        }
        public class WeatherDetails
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public double MinTemp { get; set; }
            public double MaxTemp { get; set; }
        }
    }
}

