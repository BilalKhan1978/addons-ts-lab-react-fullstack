using addons_ts_lab_puppiesApi.Models;
using addons_ts_lab_puppiesApi.ViewModels;

namespace addons_ts_lab_puppiesApi.Services
{
    public interface IPuppyService
    {
      Task<List<PuppyDto>> GetAllPuppies();
      Task<PuppyDto> GetPuppyById(Guid id);
      Task<Puppy> AddPuppy(AddPuppyRequestDto addPuppyRequestDto, string filename);
      Task UpdatePuppy(Guid id,UpdatePuppyRequestDto updatePuppyRequestDto);
      Task DeletePuppyById(Guid id);
    }
}
