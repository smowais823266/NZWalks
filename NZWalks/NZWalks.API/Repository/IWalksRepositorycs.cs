using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository
{
    public interface IWalksRepositorycs
    {
         Task<Walk> CreateAsync(Walk dto);

        Task<List<Walk>> GetAllAsync(string? FilterOn=null, string? FilterBy=null, 
            string? sortBy = null, bool isAsc = true, int pageNbr = 1, int pageSize = 1000);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
