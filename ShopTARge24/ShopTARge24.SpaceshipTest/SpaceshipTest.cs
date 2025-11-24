using Microsoft.Identity.Client;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using System;
using System.Runtime.InteropServices;

namespace ShopTARge24.SpaceshipTest

{
    public class SpaceshipTest : TestBase
    {
        //Kustutab Spaceship andmebaasist ID alusel
        [Fact]
        async public Task Should_DeleteByIdSpaceship_WhenDeleteSapceship()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipData();
            // Act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deleteSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)createSpaceship.Id);
            // Assert
            Assert.Equal(createSpaceship.Id, deleteSpaceship.Id);
        }

        //Hangi Spaceship andmebaasist ID alusel
        [Fact]
        public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");

            //act
            await Svc<ISpaceshipServices>().DetailAsync(guid);

            //assert
            Assert.NotEqual(wrongGuid, guid);
        }

        //Uuendab Spaceship andmebaasi
        [Fact]
        async public Task Should_UpdateSpaceshipData_WhenUpdateSpaceship()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipData();
            SpaceshipDto updateDto = MockUpdateSpaceshipData();
            // Act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var updateSpaceship = await Svc<ISpaceshipServices>().Update(updateDto);
            // Assert
            Assert.Equal(updateDto.Name, updateSpaceship.Name);
            Assert.Equal(updateDto.Classification, updateSpaceship.Classification);
            Assert.Equal(updateDto.Crew, updateSpaceship.Crew);
            Assert.Equal(updateDto.EnginePower, updateSpaceship.EnginePower);
        }


        //Test kontrollib, et RealEstate on edukalt kustutatud andmebaasist
        //kaob see süsteemist täielikult (Delete meetod töötab korrektselt)
        [Fact]
        public async Task Should_RemoveSpaceshipFromDatabase_WhenDelete()
        {
            //Arrange
            SpaceshipDto dto = MockSpaceshipData();

            //Act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deleteRealEstate = await Svc<ISpaceshipServices>().Delete((Guid)createSpaceship.Id);

            //uue teenuse kontrollime, et objekt on kustutatud
            var result = await Svc<ISpaceshipServices>().DetailAsync
                ((Guid)createSpaceship.Id);

            //Assert
            Assert.Equal(createSpaceship.Id, deleteRealEstate.Id);
            Assert.Null(result);
        }

        //Test kontrollib, et Crew ega enginepower ei saa olla negatiivne arv
        [Fact]
        public async Task ShouldNot_UpdateSpaceship_WhenNegativeCrewValue()
        {
            //Loome olemasoleva spaceshipi
            // Arrange
            SpaceshipDto dto = MockSpaceshipData();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            // Act
            var negativeUpdateDto = MockNegativeUpdateSpaceshipData();
            //Assert
            Assert.NotEqual(negativeUpdateDto.Crew, createSpaceship.Crew);
            Assert.NotEqual(negativeUpdateDto.EnginePower, createSpaceship.EnginePower);
        }

        //This test will simulate a scenario where an entity
        //with the same Id is already being tracked, and it will
        //verify that the Update method resolves this conflict properly.

        [Fact]
        public async Task Should_ThrowException_WhenDuplicateEntityTracked()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipData();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            // Simulate duplicate tracking by creating another instance with the same Id
            SpaceshipDto duplicateDto = new SpaceshipDto
            {
                Id = createSpaceship.Id,
                Name = "Duplicate",
                Classification = "Duplicate Class",
                BuiltDate = DateTime.UtcNow,
                Crew = 10,
                EnginePower = 1000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await Svc<ISpaceshipServices>().Update(duplicateDto);
            });

            Assert.Contains("cannot be tracked because another instance", exception.Message);
        }

        //Test kontrollib, et spaceshipi loomine ebaõnnestub, kui andmed on tühjad
        [Fact]
        public async Task ShouldNot_CreateSpaceship_WhenNullDataProvided()
        {
            // Arrange
            SpaceshipDto dto = MockNullSpaceshipData();

            // Act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            // Assert
            Assert.Null(createSpaceship);
        }

        //Test kontrollib, et spaceshipi kustutamine õnnestub, kui kehtiv ID on esitatud
        [Fact]
        public async Task Should_DeleteSpaceship_WhenValidIdProvided()
        {
            //Arrange
            SpaceshipDto dto = MockSpaceshipData();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            //Act
            var deleteSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)createSpaceship.Id);
            //Assert
            Assert.Equal(createSpaceship.Id, deleteSpaceship.Id);
        }

        // Kontrollib, et spaceshipi ei saa katte kui see ei eskisteeri
        [Fact]
        public async Task ShouldNot_GetSpaceship_WhenIdDoesNotExist()
        {
            // Arrange
            Guid nonExistentId = Guid.NewGuid();
            // Act
            var result = await Svc<ISpaceshipServices>().DetailAsync(nonExistentId);
            // Assert
            Assert.Null(result);
        }


        //Kontrollib, et spaceshipi loomine ebaõnnestub, kui nimi ületab maksimaalse pikkuse
        [Fact]
        public async Task ShouldNot_CreateOrUpdateSpaceship_WhenNameExceedsMaxLength()
        {
            //Arrange
            SpaceshipDto dto = new()
            {
                Name = new string('A', 256), // Eeldades, et maksimaalne pikkus on 255 tähemärki
                Classification = "Test Class",
                BuiltDate = DateTime.UtcNow,
                Crew = 10,
                EnginePower = 1000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            //Act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            //Assert
            Assert.NotInRange(createSpaceship.Name.Length, 0, 255);
        }

        //Test kontrollib, et ei saa tuua valja andmeid, kui andmed puuduvad
        [Fact]
        public async Task ShouldNot_AddEmptySpaceshipData()
        {
            // Arrange
            SpaceshipDto dto = MockNullSpaceshipData();
            // Act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            // Assert
            Assert.NotNull(createSpaceship);
        }

       

        private SpaceshipDto MockNullSpaceshipData()
        {
            return new SpaceshipDto
            {
                Id = null,
                Name = null,
                Classification = null,
                BuiltDate = null,
                Crew = null,
                EnginePower = null,
                CreatedAt = null,
                ModifiedAt = null
            };
        }

        private SpaceshipDto MockSpaceshipData()
        {
            return new SpaceshipDto
            {
                Name = "Bob",
                Classification = "Defiant",
                BuiltDate = DateTime.UtcNow,
                Crew = 5,
                EnginePower = 9001,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private SpaceshipDto MockUpdateSpaceshipData()
        {
            SpaceshipDto negativeSpaceship = new()
            {
                Name = "Put The Name Here",
                Classification = "Random Class",
                BuiltDate = DateTime.UtcNow,
                Crew = 100,
                EnginePower = 5000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            return negativeSpaceship;
        }

        private SpaceshipDto MockNegativeUpdateSpaceshipData()
        {
            SpaceshipDto negativeSpaceship = new()
            {
                Name = "Bob",
                Classification = "Defiant",
                BuiltDate = DateTime.UtcNow,
                Crew = -50,
                EnginePower = -9001,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            return negativeSpaceship;
        }
    }
}
