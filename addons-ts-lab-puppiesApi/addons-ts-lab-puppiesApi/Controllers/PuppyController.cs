using addons_ts_lab_puppiesApi.Models;
using addons_ts_lab_puppiesApi.Services;
using addons_ts_lab_puppiesApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using System.Reflection;

namespace addons_ts_lab_puppiesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuppyController : Controller
    {
        private readonly IPuppyService _puppyService;
       private readonly IWebHostEnvironment _hostEnvironment;
        public PuppyController(IPuppyService puppyService, IWebHostEnvironment hostEnvironment)
        {
            _puppyService = puppyService;
            _hostEnvironment = hostEnvironment;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllPuppies()
        {
            try
            {
                var puppyList =  await _puppyService.GetAllPuppies();
                puppyList.ForEach(x => x.ImageSrc = x.ImageName == null ? null :
                 String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName));
                return Ok(puppyList);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOnePuppy([FromRoute] Guid id)
        {
            try
            {
                var puppy = await _puppyService.GetPuppyById(id);
                puppy.ImageSrc = puppy.ImageName == null ? null : String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, puppy.ImageName);
                return Ok(puppy);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPuppy([FromForm] AddPuppyRequestDto addPuppyRequestDto)
        {
            
             try
            {
                var fileName = addPuppyRequestDto.Image == null ? null : await SaveImage(addPuppyRequestDto.Image);
                var addPuppy = await _puppyService.AddPuppy(addPuppyRequestDto, fileName);
                return Ok(addPuppy);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }






        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePuppy([FromRoute] Guid id, [FromForm] UpdatePuppyRequestDto updatePuppyRequestDto)
        {
            try
            {
                var fileName = "";
                var puppy= await _puppyService.GetPuppyById(id);
                if (puppy == null)
                    return NotFound();
                if (updatePuppyRequestDto.Image != null)
                {
                    if(puppy.ImageName!=null)
                    DeleteImage(puppy.ImageName);
                    updatePuppyRequestDto.ImageName = updatePuppyRequestDto.Image == null ? null : await SaveImage(updatePuppyRequestDto.Image);

                }
                else
                    updatePuppyRequestDto.ImageName = puppy.ImageName;
                await _puppyService.UpdatePuppy(id, updatePuppyRequestDto);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuppy([FromRoute] Guid id)
        {
            try
            {
                var puppy = await _puppyService.GetPuppyById(id);
                if (puppy == null)
                    return NotFound();
                if (puppy.Image != null)
                {
                    DeleteImage(puppy.ImageName);

                }
                await _puppyService.DeletePuppyById(id);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

    }
}

