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
        public AccuRegionDto? Region { get; set; }
        public AccuCountryDto? Country { get; set; }
        public AccuAdministrativeAreaDto? AdministrativeArea { get; set; }
        public AccuTimezoneDto? Timezone { get; set; }
        public AccuGeoPositionDto? GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public AccuSupplementalAdminAreasDto[]? SupplementalAdminAreas { get; set; }
        public AccuDataSetsDto? DataSets { get; set; }
    }

    public class AccuRegionDto
    {
        public string? ID { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
    }

    public class AccuCountryDto
    {
        public string? ID { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
    }

    public class AccuAdministrativeAreaDto
    {
        public string? ID { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
        public int Level { get; set; }
        public string? LocalizedType { get; set; }
        public string? EnglishType { get; set; }
        public int? CountryID { get; set; }
    }

    public class AccuTimezoneDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int GmtOffset { get; set; }
        public bool IsDaylightSaving { get; set; }
        public string? NextOffsetChange { get; set; }
    }

    public class AccuGeoPositionDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Elevation { get; set; }
    }

    public class AccuSupplementalAdminAreasDto
    {
        public int Level { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
    }

    public class AccuDataSetsDto
    {
        public string[]? DataSet { get; set; }
    }
}
