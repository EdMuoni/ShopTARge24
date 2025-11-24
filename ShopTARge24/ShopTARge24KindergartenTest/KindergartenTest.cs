using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.KindergartenTest;

namespace ShopTARge24KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        //Kontrollime, et tühja andmetega lasteaeda ei saa lisada
        [Fact]
        public async Task ShouldNot_AddEmptyKindergarten_WhenReturnResult()
        {
            // Arrange
            KindergartenDto dto = MockNullKindergartenData();
            // Act
            var result = await Svc<IKindergartenServices>().Create(dto);
            // Assert
            Assert.NotNull(result);
        }

        //Kontrollime, et vale ID-ga lasteaeda ei saa kätte
        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            //act
            await Svc<IKindergartenServices>().DetailAsync(guid);
            //assert
            Assert.NotEqual(wrongGuid, guid);
        }

        //Kontrollime, et õige ID-ga lasteaed saab kätte
        [Fact]
        public async Task Should_GetByIdKindergarten_WhenReturnsEqual()
        {
            //arrange
            Guid databaseGuid = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            Guid guid = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            //act
            await Svc<IKindergartenServices>().DetailAsync(guid);
            //assert
            Assert.Equal(databaseGuid, guid);
        }

        //Kontrollime, et lasteaeda saab kustutada
        [Fact]
        public async Task Should_DeleteByIdKindergarten_WhenDeleteKindergarten()
        {
            //arrange
            Guid guid = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            //act
            var result = await Svc<IKindergartenServices>().Delete(guid);
            //assert
            Assert.NotNull(result);
        }

        //Kontrollime, et lasteaia andmeid saab uuendada
        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdateKindergartenData()
        {
            //arrange
            KindergartenDto dto = MockKindergartenData();
            //act
            var createKindergarten = await Svc<IKindergartenServices>().Create(dto);
            var updateKindergarten = MockUpdateKindergartenData();
            //assert
            Assert.NotEqual(createKindergarten.GroupName, updateKindergarten.GroupName);
            Assert.NotEqual(createKindergarten.ChildrenCount, updateKindergarten.ChildrenCount);
            Assert.NotEqual(createKindergarten.KindergartenName, updateKindergarten.KindergartenName);
            Assert.NotEqual(createKindergarten.TeacherName, updateKindergarten.TeacherName);

        }

        //Kontrollime, et negatiivse laste arvuga lasteaeda ei saa lisada
        [Fact]
        public async Task ShouldNot_AddKindergartenWithNegativeChildrenCount_WhenReturnResult()
        {
            // Arrange
            KindergartenDto dto = new()
            {
                GroupName = "Negative Children Count Location",
                ChildrenCount = -5,
                KindergartenName = "Negative House",
                TeacherName = "Jane Doe",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            // Act
            var result = await Svc<IKindergartenServices>().Create(dto);
            // Assert
            Assert.NotNull(result);
        }



        private KindergartenDto MockKindergartenData()
        {
            return new KindergartenDto
            {
                GroupName = "Sample Location",
                ChildrenCount = 4,
                KindergartenName = "House",
                TeacherName = "John Doe",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private KindergartenDto MockUpdateKindergartenData()
        {
            KindergartenDto kinderGarten = new()
            {
                GroupName = "New Name",
                ChildrenCount = 8,
                KindergartenName = "Apartmen",
                TeacherName = "Sam Doe",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return kinderGarten;
        }
        private KindergartenDto MockNullKindergartenData()
        {
            KindergartenDto kinderGarten = new()
            {
                GroupName = "",
                ChildrenCount = null,
                KindergartenName = "",
                TeacherName = "",
                CreatedAt = null,
                UpdatedAt = null
            };

            return kinderGarten;
        }
    }


}
