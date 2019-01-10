using Dapper;
using DarkSide;
using Dog_Management.Models.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dog_Management.Models.Profile
{
    public class ProfileManager : Manager
    {
        public ProfileManager(Concrete concrete) : base(concrete) { }
        public async Task<List<Dog>> GetDogsByUserId(int userId)//хранимая процедура получения всех собак 
        {
            List<Dog> dogs = null;
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                dogs = (await cnt.QueryAsync<Dog>(
                    sql: "GetDogsByUserId",
                    commandType: CommandType.StoredProcedure,
                      param: new
                      {
                          userId
                      }
                    )).ToList();
            }
            return dogs;
        }
        public async Task<Dog> GetDogById(int DogId)
        {
            Dog dog = null;
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                dog = (await cnt.QueryAsync<Dog>(
                    sql: "GetDogByDogId",
                    commandType: CommandType.StoredProcedure,
                      param: new
                      {
                          DogId
                      }
                    )).FirstOrDefault();
            }
            return dog;
        }
        public async Task<dynamic> GetDogCatalogs()
        {
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                using (var reader = await cnt.QueryMultipleAsync(sql: "GetDogCatalogs",commandType: CommandType.StoredProcedure))
                {
                    return new
                    {
                        Colors = reader.Read<IndexType>(),
                        Breeds = reader.Read<IndexType>()
                    };
                }
            }
        }

        public async Task ChangeDogInfo(Dog model)
        {
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                await cnt.ExecuteAsync(
                   sql: "ChangeDogInfo",
                   commandType: CommandType.StoredProcedure,
                   param: new
                   {
                       model.Id,
                       model.Name,
                       model.LicenseNumber,
                       model.IsMale,
                       ColorId = model.Color.Id,
                       BreedId = model.Breed.Id,
                       model.DateOfBirth,
                       model.BloodLine
                   }
                   );
            }
        }
    }
}