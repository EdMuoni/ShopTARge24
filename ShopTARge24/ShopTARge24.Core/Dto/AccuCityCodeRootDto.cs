using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.Core.Dto
{
    public class AccuCityCodeRootDto
    {
        public string? Version { get; set; }
        public int Key { get; set; }
        public string? Type { get; set; } = null;
        public int Rank { get; set; }
        public string? LocalizedName { get; set; } = null;
        public string? EnglishName { get; set; }
        public int? PrimaryPostalCode { get; set; }
        public Region? Region { get; set; }
        public Country? Country { get; set; }
        public AdministrativeArea? AdministrativeArea { get; set; }
        public Timezone? Timezone { get; set; }
        public GeoPosition? GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public SupplementalAdminAreas[]? SupplementalAdminAreas { get; set; }
        public DataSets? DataSets { get; set; }
    }

    public class Region
    {
        public string? ID { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
    }

    public class Country
    {
        public string? ID { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
    }

    public class AdministrativeArea
    {
        public string? ID { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
        public int Level { get; set; }
        public string? LocalizedType { get; set; }
        public string? EnglishType { get; set; }
        public int? CountryID { get; set; }
    }

    public class Timezone
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int GmtOffset { get; set; }
        public bool IsDaylightSaving { get; set; }
        public string? NextOffsetChange { get; set; }
    }

    public class GeoPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Elevation { get; set; }
    }

    public class SupplementalAdminAreas
    {
        public int Level { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
    }

    public class DataSets
    {
        public string[]? DataSet { get; set; }
    }
}
