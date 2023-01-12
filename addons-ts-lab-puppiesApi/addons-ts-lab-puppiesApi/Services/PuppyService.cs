using addons_ts_lab_puppiesApi.Data;
using addons_ts_lab_puppiesApi.Models;
using addons_ts_lab_puppiesApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace addons_ts_lab_puppiesApi.Services
{
    public class PuppyService : IPuppyService
    {
        private readonly PuppyDbContext _dbContext;
        public PuppyService(PuppyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PuppyDto>> GetAllPuppies()
        {
            var puppies= await _dbContext.Puppies.ToListAsync();
            var puppyList = new List<PuppyDto>();
            puppies.ForEach(puppy =>
            {
                var puppyDto = new PuppyDto
                {
                    Id = puppy.Id,
                    Name = puppy.Name,
                    Breed = puppy.Breed,
                    BirthDate = puppy.BirthDate.ToShortDateString(),
                    ImageName = puppy.ImageName

                };
                puppyList.Add(puppyDto);
            });
            return puppyList;
        }
        public async Task<PuppyDto> GetPuppyById(Guid id)
        {
           var puppy = await _dbContext.Puppies.FindAsync(id);
            if (puppy == null)
                throw new Exception("No Record Found");
            var puppyDto = new PuppyDto
            {
                Id = puppy.Id,
                Name = puppy.Name,
                Breed = puppy.Breed,
                BirthDate = puppy.BirthDate.ToShortDateString(),
                ImageName = puppy.ImageName

            };
            return puppyDto;   
        }
        public async Task<Puppy> AddPuppy(AddPuppyRequestDto addPuppyRequestDto, string fileName)
        {
            var puppy = new Puppy()
            {
                Id = new Guid(),
                Name = addPuppyRequestDto.Name,
                Breed = addPuppyRequestDto.Breed,
                BirthDate = addPuppyRequestDto.BirthDate,
                ImageName = fileName,

            };
            await _dbContext.Puppies.AddAsync(puppy);
            await _dbContext.SaveChangesAsync();
            return puppy;
        }
        public async Task UpdatePuppy(Guid id,UpdatePuppyRequestDto updatePuppyRequestDto)
        {
            var puppy = await _dbContext.Puppies.FindAsync(id);
            
            //var bytes = await updatePuppyRequestDto.Image.GetBytes();
            puppy.Name = updatePuppyRequestDto.Name;
            puppy.Breed = updatePuppyRequestDto.Breed;
            puppy.BirthDate = updatePuppyRequestDto.BirthDate;
            puppy.ImageName = updatePuppyRequestDto.ImageName;
           // puppy.Image = bytes;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeletePuppyById(Guid id)
        {
            var puppy = await _dbContext.Puppies.FindAsync(id);           
              _dbContext.Remove(puppy);
            await _dbContext.SaveChangesAsync();
        }
    }
}
