using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherApp.Template
{
    [Serializable]
    internal class WeatherTemplate
    {
        [JsonProperty("coord")]
        internal WeatherCoord position = new WeatherCoord();

        [JsonProperty("weather")]
        internal List<Weather> weather = new List<Weather>();

        [JsonProperty("main")]
        internal MainInfo mainInfo = new MainInfo();

        [JsonProperty("wind")]
        internal Wind wind = new Wind();

        [JsonProperty("clouds")]
        internal Clouds clouds = new Clouds();

        [JsonProperty("sys")]
        internal SysInfo sysInfo = new SysInfo();

        [JsonProperty("name")]
        internal string town;

        //[JsonProperty("timezone")]
        //internal string timezone;

        //[JsonProperty("id")]
        //internal string id;

        //[JsonProperty("town")]
        //internal string town;

        //[JsonProperty("cod")]
        //internal string cod;
    }

    struct SysInfo
    {
        [JsonProperty("id")]
        internal string id;

        [JsonProperty("message")]
        internal string message;

        [JsonProperty("country")]
        internal string country;

        [JsonProperty("sunrise")]
        internal string sunrise;

        [JsonProperty("sunset")]
        internal string sunset;
    }

    struct WeatherCoord
    {
        [JsonProperty("lon")]
        internal string lon;

        [JsonProperty("lat")]
        internal string lat;
    }

    struct Weather
    {
        [JsonProperty("id")]
        internal string id;

        [JsonProperty("main")]
        internal string main;

        [JsonProperty("description")]
        internal string description;

        [JsonProperty("icon")]
        internal string icon;
    }

    struct MainInfo
    {
        [JsonProperty("temp")]
        internal string temperature;

        //[JsonProperty("feels_like")]
        //internal string feelsLike;

        [JsonProperty("temp_min")]
        internal string tempMin;

        [JsonProperty("temp_max")]
        internal string tempMax;

        [JsonProperty("pressure")]
        internal string pressure;

        [JsonProperty("humidity")]
        internal string humidity;
    }

    struct Wind
    {
        [JsonProperty("speed")]
        internal string speed;

        [JsonProperty("deg")]
        internal string deg;
    }

    struct Clouds
    {
        [JsonProperty("all")]
        internal string allClouds;
    }
}