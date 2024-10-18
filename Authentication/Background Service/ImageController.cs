using Authentication.PublicClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Background_Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            var fileName = new UploadHandler().Upload(file);

            if (fileName.Contains("not valid"))
            {
                return BadRequest(fileName); // Return error if file is not valid
            }

            // Construct the full URL to the uploaded file
            var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{fileName}";

            return Ok(fileUrl); // Return the URL of the uploaded image
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

            if (!System.IO.File.Exists(path))
            {
                return NotFound("Image not found");
            }

            var image = System.IO.File.ReadAllBytes(path);
            return File(image, "image/jpeg");
        }

    }

}
