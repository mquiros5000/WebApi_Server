﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Server
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
    }
}