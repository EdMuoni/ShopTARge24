using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopTARge24.Core.Dto
{
    public class AccuLocationRootDto
    {
        public string? LocalObservationDateTime { get; set; }
        public long EpochTime { get; set; }
        public string? WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string? PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public AccuTemperatureDto? Temperature { get; set; }
        public string? MobileLink { get; set; }
        public string? Link { get; set; }

    }
    public class AccuTemperatureDto
    {
        public AccuWeatherUnitDto? Metric { get; set; }
        public AccuWeatherUnitDto? Imperial { get; set; }
    }

    public class AccuWeatherUnitDto
    {
        public double Value { get; set; }
        public string? Unit { get; set; }
        public int UnitType { get; set; }
    }
}