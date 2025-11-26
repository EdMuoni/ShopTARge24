using Microsoft.Identity.Client;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System;


namespace ShopTARge24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = 120,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");

            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealestate_WhenReturnsEqual()
        {
            //arrange
            Guid databaseGuid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            Guid guid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();

            //act
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            //assert
            Assert.Equal(addRealEstate.Id, deleteRealEstate.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //arrange
            var dto = MockRealEstateData();

            //act
            var realEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var realEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);

            //assert
            Assert.NotEqual(realEstate1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //arrange
            var guid = new Guid("68ce7565-9105-4945-b428-b8e25ec061c6");

            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            domain.Area = 200;
            domain.Location = "Updated Location";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            //act
            await Svc<IRealEstateServices>().Update(dto);

            //assert
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            //Võrrelda RoomNumbrit ja kasutada DoesNotMatch
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.DoesNotMatch(dto.Location, domain.Location);
        }

        //[Fact]
        //public async Task Should_UpdaterealEstate_WhenUpdateDataVersion2()
        //{

        //    //lõpus kontrollime et andmed on erinevad
        //    //arrange and act
        //    //alguses andmed luuakse ja kasutame MockRealEstateDto meetodit
        //    RealEstateDto dto = MockRealEstateData();
        //    var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

        //    //andmed uuendatakse ja kasutame uut Mock meetodit(selle peab ise tegema)
        //    RealEstateDto updatedDto = MockUpdateRealEstateData();
        //    var result = await Svc<IRealEstateServices>().Update(updatedDto);

        //    //assert
        //    Assert.DoesNotMatch(createRealEstate.Location, result.Location);
        //    Assert.NotEqual(createRealEstate.ModifiedAt, result.ModifiedAt);
        //}

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            //arrange
            //kasutate MockRealEstateData meetodit, kus on andmed
            //tuleb kasutada Create meetodit, et andmed luua
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //tuleb teha uus meetod nimega MockNullRealEstateData(),
            //kus on tühjad andmed e null või ""
            RealEstateDto nullDto = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(nullDto);

            //assert
            //toimub võrdlemine, et andmed ei ole võrdsed
            Assert.NotEqual(createRealEstate.Id, result.Id);
        }


        //tuleb välja mõelda kolm erinevat xUnit testi RealEstate kohta
        //saate teha 2-3 in meeskonnas
        //kommentaari kirjutate, mida iga test kontrollib

        private RealEstateDto MockNullRealEstateData()
        {
            return new RealEstateDto
            {
                Id = null,
                Area = null,
                Location = "",
                RoomNumber = null,
                BuildingType = "",
                CreatedAt = null,
                ModifiedAt = null
            };
        }

        private RealEstateDto MockRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 150,
                Location = "Sample Location",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100,
                Location = "Sample Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;

        }

        //Meeskonnas tehtud testid:

        //tuleb välja mõelda kolm erinevat xUnit testi RealEstate kohta
        //saate teha 2-3 in meeskonnas
        //kommentaari kirjutate, mida iga test kontrollib


        //Ei saa sisestada negatiivset andmeid
        // Kontrollib, et kui proovime sisestada negatiivseid väärtusi kinnisvara omaduste jaoks,
        // siis tagastatakse õige veateade.

        //Ei saa uuendada andmeid negatiivseteks
        // Kontrollib, et kui proovime sisestada negatiivseid väärtusi kinnisvara omaduste jaoks,
        // siis tagastatakse õige veateade.



        [Fact]
        //Ei saa sisestada negatiivset andmeid
        // Kontrollib, et kui proovime sisestada negatiivseid väärtusi kinnisvara omaduste jaoks,
        // siis tagastatakse õige veateade.

        //public async Task Should_CreateRealEstateWithNegativeAre_WhenAreaISNegativeToCreate()

        public async Task ShouldNot_CreateRealEstateWithNegativeValues_WhenAttemptToCreate()
        {
            //Arrange
            //Loome DTO negatiivse väärtustega
            RealEstateDto dto = new()
            {
                Area = -120, //Negatiivne pindala
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Area, dto.Area);
        }

        //Test kontrollib, et RealEstate on edukalt kustutatud andmebaasist
        //kaob see süsteemist täielikult (Delete meetod töötab korrektselt)
        [Fact]
        public async Task Should_RemoveRealEstateFromDatabase_WhenDelete()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createRealEstate.Id);

            //uue teenuse kontrollimen, et objekt on kustutatud
            //var freshService = Svc<IRealEstateServices>();
            var result = await Svc<IRealEstateServices>().DetailAsync
                ((Guid)createRealEstate.Id);

            //Assert
            Assert.Equal(createRealEstate.Id, deleteRealEstate.Id);
            Assert.Null(result);
        }

        [Fact]
        //Ei saa sisestada negatiivset andmeid, kui uuendame olemasolevat kinnisvara
        // Kontrollib, et kui proovime sisestada negatiivseid väärtusi kinnisvara omaduste jaoks,
        // siis tagastatakse õige veateade.

        public async Task ShouldNot_UpdateRealEstateWithNegativeValues_WhenAttemptToUpdate()
        {
            //Arrange
            //Loome olemasoleva kinnisvara
            RealEstateDto dto = MockRealEstateData();
            var addRealEstate1 = await Svc<IRealEstateServices>().Create(dto);
            //Act
            var negativeRealEstate = await Svc<IRealEstateServices>().Update(dto);
            //Assert
            Assert.NotNull(negativeRealEstate);
        }

        [Fact]
        public async Task Should_ReturnCorrectRealEstate_WhenCallingDetails()
        {
            // Kontrollib, et Details tagastab õige maja andmed
            //arrange
            RealEstateDto dto = MockRealEstateData();
            var created = await Svc<IRealEstateServices>().Create(dto);

            //act
            var details = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id);

            //assert
            Assert.NotNull(details);
            Assert.Equal(created.Id, details.Id);
            Assert.Equal(created.Area, details.Area);
            Assert.Equal(created.Location, details.Location);
        }

        //Test kontrollib
        [Fact]
        public async Task Should_UpdateRealEstateRoomNumber_WhenUpdateRoomNumber()
        {

            //arrange
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //Loo taiesti uus DTO uuendamiseks, kus tracking on välja lülitatud
            RealEstateDto updatedDto = MockUpdateRealEstateData();

            //act
            //Uuendame ainult RoomNumber
            updatedDto.RoomNumber = 10;
            //kasutame Create, et valtida tracking viga
            var result = await Svc<IRealEstateServices>().Create(updatedDto);

            //assert
            //kontrollime, et RoomNumber on uuendatud
            Assert.Equal(10, result.RoomNumber);
            Assert.NotEqual(createRealEstate.RoomNumber, result.RoomNumber);

            //kontrollime, et teised andmed on samad
            Assert.Equal(createRealEstate.Location, result.Location);
        }


        [Fact]
        public async Task ShouldUpdateModifiedAt_WhenUpdateData()
        {
            //arrange - loome meetod Create
            RealEstateDto dto = MockRealEstateData();
            var created = await Svc<IRealEstateServices>().Create(dto);

            //act - uued MockUpdateRealEstateData andmed
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            //arrange - kontrollime, et ModifiedAt muutus
            Assert.NotEqual(created.ModifiedAt, result.ModifiedAt);

        }

        [Fact]

        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            //Arrange
            //teeme - muutuja CreatedAt originaaliks, mis peab jääma samaks
            //loome CreatedAt
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2025-11-17T09:22:52.6417870+02:00";

            //act - uuendame MockUpdateRealEstate andmeid
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(dto);
            result.CreatedAt = DateTime.Parse("2025-11-17T09:22:52.6417870+02:00");

            //assert - kontrollime, et uuendamisel ei uuendaks CreatedAt
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt);
        }

        [Fact]
        public async Task ShouldCheckRealEstateIdUnique()
        {
            //arrange -loome kaks erinevat RealEstate objekti
            RealEstateDto dto1 = MockRealEstateData();
            RealEstateDto dto2 = MockRealEstateData();

            //act - kasutame Id loomiseks
            var create1 = await Svc<IRealEstateServices>().Create(dto1);
            var create2 = await Svc<IRealEstateServices>().Create(dto2);

            //assert - kontrollime, et Id-d on erinevad
            Assert.NotEqual(create1.Id, create2.Id);
        }

        //Tuleb kontrollida, et tühja kinnisvara lisamine ei õnnestu
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstae()
        {
            //Arrange
            RealEstateDto dto = new()
            {
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);
        }

        //Third test to modified parameter should be updated when updated when real estate is updated
        [Fact]
        public async Task ShouldUpdate_ModifiedAt_Parameter()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            var createdRealEstateResult = await Svc<IRealEstateServices>().Create(dto);

            //Act
            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //Assert
            Assert.NotEqual(dto.CreatedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task Should_ReturnRealEstate_WhenCorrectDetailAsync()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deyailedRealEstate = await Svc<IRealEstateServices>().DetailAsync((Guid)createdRealEstate.Id);

            //Assert
            Assert.NotNull(deyailedRealEstate);
            Assert.Equal(createdRealEstate.Id, deyailedRealEstate.Id);
            Assert.Equal(createdRealEstate.Location, deyailedRealEstate.Location);
            Assert.Equal(createdRealEstate.Area, deyailedRealEstate.Area);
            Assert.Equal(createdRealEstate.RoomNumber, deyailedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, deyailedRealEstate.BuildingType);

        }


        [Fact]

        public async Task Should_UpdateRealEstate_WhenPartialUpdate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var updatedDto = new RealEstateDto
            {
                Area = 99,
                Location = "Changed Location Only",
                RoomNumber = createdRealEstate.RoomNumber,
                BuildingType = createdRealEstate.BuildingType,
                CreatedAt = createdRealEstate.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(updatedDto);

            //Arrange
            Assert.NotEqual(createdRealEstate.Area, updatedRealEstate.Area);
            Assert.DoesNotMatch(createdRealEstate.Area.ToString(), updatedRealEstate.Area.ToString());
            Assert.Equal("Changed Location Only", updatedRealEstate.Location);
            Assert.NotEqual(createdRealEstate.Location, updatedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, updatedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, updatedRealEstate.BuildingType);
        }

        [Fact]

        public async Task Should_AddValidRealEstate_WhenDataTypeIsValid()
        {
            //Arrange
            var dto = new RealEstateDto
            {
                Area = 85,
                Location = "Tartu",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //Act
            var realEstate = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.IsType<int>(realEstate.RoomNumber);
            Assert.IsType<string>(realEstate.Location);
            Assert.IsType<DateTime>(realEstate.CreatedAt);
        }

        [Fact]
        public async Task Should_CreateRealEstate_AndAssist()
        {
            //Arrange
            var dto = MockRealEstateData();
            dto.Id = Guid.Empty;
            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);
            //Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        //Kontrollib, et kustutatud RealEstate pole leitav andmebaasist
        public async Task Should_ReturnNull_WhenReadingDeletedRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            var created = await Svc<IRealEstateServices>().Create(dto);

            //Act
            await Svc<IRealEstateServices>().Delete((Guid)created.Id);

            //Assert
            var result = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id);

            Assert.Null(result);
        }

        [Fact]

        public async Task ShouldNot_UpdateCreatedTime_WhenUpdatedRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new()
            {
                Id = dto.Id,
                Area = 180,
                Location = "Another Updated Location",
                RoomNumber = 6,
                BuildingType = "Cottage",
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            //Act - kasutame Update meetodit
            var updateRealEstate = await Svc<IRealEstateServices>().Update(domain);

            //Assert
            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }

        //Kustubab RealEstate andmed koos piltidega
        [Fact]
        public async Task Should_DeleteRelatedImages_WhenDeleteRealEstate()
        {
            //Arrange
            var dto = new RealEstateDto
            {
                Area = 120,
                Location = "Image Test Location",
                RoomNumber = 4,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var created = await Svc<IRealEstateServices>().Create(dto);
            var id = (Guid)created.Id;

            var db = Svc<ShopTARge24Context>();
            db.FileToDatabases.Add(new Core.Domain.FileToDatabase
            {
                Id = Guid.NewGuid(),
                RealEstateId = id,
                ImageTitle = "kitchen.jpg",
                ImageData = new byte[] { 1, 2, 3 },
            });

            db.FileToDatabases.Add(new Core.Domain.FileToDatabase
            {
                Id = Guid.NewGuid(),
                RealEstateId = id,
                ImageTitle = "livingroom.jpg",
                ImageData = new byte[] { 4, 5, 6 },
            });
            await db.SaveChangesAsync();

            // Act
            await Svc<IRealEstateServices>().Delete(id);

            // Assert
            var leftovers = db.FileToDatabases.Where(x => x.RealEstateId == id).ToList();

            Assert.Empty(leftovers);
        }

        private RealEstateDto MockNegativeUpdateRealEstateData()
        {
            RealEstateDto negativeRealEstate = new()
            {
                Area = -100,
                Location = "Secret Location",
                RoomNumber = -7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return negativeRealEstate;
        }


    }
}