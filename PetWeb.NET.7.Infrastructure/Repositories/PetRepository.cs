using Dapper;
using Microsoft.Extensions.Options;
using PetWeb.NET._7.Domain.Pets;
using PetWeb.NET._7.Domain.Shared;
using PetWeb.NET._7.Infrastructure.Entities;
using PetWeb.NET._7.Infrastructure.Factories;
using PetWeb.NET._7.Infrastructure.Options;
using System.Data;

namespace PetWeb.NET._7.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly DatabaseOptions _databaseOptions;

    public PetRepository(IConnectionFactory connectionFactory, IOptions<DatabaseOptions> databaseOptions)
    {
        _connectionFactory = connectionFactory;
        _databaseOptions = databaseOptions.Value;
    }
    public async Task<Page<Pet>> GetPetAsync(PetFilter query, PageRequest page)
    {
        try
        {
            using IDbConnection dbConnection = _connectionFactory.GetConnection();

            dbConnection.Open();

            DynamicParameters parameters = new();

            parameters.Add("PetId", query.PetId);
            parameters.Add("Name", query.Name);
            parameters.Add("Type", query.Type);
            parameters.Add("Offset", page.Offset);
            parameters.Add("Size", page.Size);

            IEnumerable<PetDto> PetsDto = await dbConnection.QueryAsync<PetDto>("sp_Pet_SelectPage",
                param: parameters,
                commandTimeout: _databaseOptions.CommandTimeOut,
                commandType: CommandType.StoredProcedure);

            IEnumerable<Pet> Pets = PetsDto.Select(pet => new Pet { PetId = pet.PetId, Name = pet.Name, Type = pet.Type });

            DynamicParameters parametersCount = new();

            parametersCount.Add("PetId", query.PetId);
            parametersCount.Add("Name", query.Name);
            parametersCount.Add("Type", query.Type);

            int PetsCount = await dbConnection.QuerySingleAsync<int>("sp_Pet_SelectPageCount",
                param: parametersCount,
                commandTimeout: _databaseOptions.CommandTimeOut,
                commandType: CommandType.StoredProcedure);

            return new Page<Pet>
            {
                CurrentPage = page.Page,
                Results = Pets,
                Size = page.Size,
                TotalResults = PetsCount
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int> InsertPetAsync(string name, int type)
    {
        try
        {
            using IDbConnection dbConnection = _connectionFactory.GetConnection();

            dbConnection.Open();

            DynamicParameters parameters = new();

            parameters.Add("PetId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Name", name);
            parameters.Add("Type", type);

            await dbConnection.QueryFirstOrDefaultAsync<int>("sp_Pet_Insert",
                param: parameters,
                commandTimeout: _databaseOptions.CommandTimeOut,
                commandType: CommandType.StoredProcedure);

            int petId = parameters.Get<int>("PetId");

            return petId;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<bool> UpdatePetAsync(int petId, string name, int type)
    {
        try
        {
            using IDbConnection dbConnection = _connectionFactory.GetConnection();

            dbConnection.Open();

            DynamicParameters parameters = new();

            parameters.Add("PetId", petId);
            parameters.Add("Name", name);
            parameters.Add("Type", type);

            int response = await dbConnection.ExecuteAsync("sp_Pet_Update",
                param: parameters,
                commandTimeout: _databaseOptions.CommandTimeOut,
                commandType: CommandType.StoredProcedure);

            return response > 0;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> DeletePetAsync(int petId)
    {
        try
        {
            using IDbConnection dbConnection = _connectionFactory.GetConnection();

            dbConnection.Open();

            DynamicParameters parameters = new();

            parameters.Add("PetId", petId);
            parameters.Add("Success", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await dbConnection.QueryFirstOrDefaultAsync<int>("sp_Pet_Delete",
                param: parameters,
                commandTimeout: _databaseOptions.CommandTimeOut,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("Success") > 0;
        }
        catch (Exception)
        {

            throw;
        }
    }
}