using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository
{
    public class WalksRepository : IWalksRepositorycs
    {
        private readonly NZWalksDbContext dbContext;

        public WalksRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
           await dbContext.SaveChangesAsync();
           return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? FilterBy = null, string? FilterQuery = null, 
            string? sortBy = null, bool isAsc = true, int pageNbr = 1, int pageSize = 1000)
        {
            var walk = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            
            //filtering
            if(!string.IsNullOrWhiteSpace(FilterBy) && !string.IsNullOrWhiteSpace(FilterQuery))
            {
               if(FilterBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(x => x.Name.Contains(FilterQuery));
                }
            }

            //sorting
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Contains("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walk = (isAsc)?walk.OrderBy(x => x.Name):walk.OrderByDescending(x => x.Name);
                }

                else if(sortBy.Contains("Length",StringComparison.OrdinalIgnoreCase))
                {
                    walk = (isAsc)?walk.OrderBy(x => x.LengthInKm): walk.OrderByDescending(x => x.LengthInKm);
                }
            }
                
            var skipResult = (pageNbr - 1) * pageSize;

            return await walk.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId; 
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x =>x.Id == id);

            if(walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            dbContext.SaveChanges();
            return walk;
        }

      
    }
}
