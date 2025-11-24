using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.KindergartenTest;

namespace ShopTARge24KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        private RealEstateDto MockRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                GroupName = "Sample Location",
                ChildrenCount = 4,
                KindergartenName = "House",
                TeacherName = "John Doe",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Secret Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;
        }
        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = null,
                Location = "",
                RoomNumber = null,
                BuildingType = "",
                CreatedAt = null,
                ModifiedAt = null
            };

            return realEstate;
        }
    }


}
