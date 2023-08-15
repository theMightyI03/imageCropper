using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ImageCropperApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageCropperController : ControllerBase
    {
        private readonly ImageCropper _imageCropper;

        public ImageCropperController(ImageCropper imageCropper)
        {
            _imageCropper = imageCropper;
        }

        [HttpPost("crop")]
        public async Task<IActionResult> CropImage([FromBody] ImageProcessRequest processRequest)
        {
            try
            {
                byte[] processedImage = await _imageCropper.CropImage(processRequest.ImageUrl, processRequest.Width, processRequest.Height, processRequest.StartX, processRequest.StartY);

                return File(processedImage, "image/jpeg"); // Ändern Sie den MIME-Typ je nach Bildformat
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("resize")]
        public async Task<IActionResult> ResizeImage([FromBody] ImageProcessRequest processRequest)
        {
            try
            {
                byte[] processedImage = await _imageCropper.ResizeImage(processRequest.ImageUrl, processRequest.Width, processRequest.Height);

                return File(processedImage, "image/jpeg"); // Ändern Sie den MIME-Typ je nach Bildformat
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("rotate")]
        public async Task<IActionResult> RotateImage([FromBody] ImageProcessRequest processRequest)
        {
            try
            {
                byte[] processedImage = await _imageCropper.RotateImage(processRequest.ImageUrl, processRequest.RotateDegree);

                return File(processedImage, "image/jpeg"); // Ändern Sie den MIME-Typ je nach Bildformat
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


    }

    public class ImageProcessRequest
    {
        public string? ImageUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int RotateDegree { get; set; }
    }
}
