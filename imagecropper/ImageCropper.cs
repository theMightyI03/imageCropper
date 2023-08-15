using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using System.Threading.Tasks;

    public class ImageCropper
    {

        public async Task<byte[]> CropImage(string imageUrl, int width, int height, int startX, int startY)
        {
            byte[] imageBytes = await APIRequestAsync(imageUrl);

            using (MemoryStream inputStream = new MemoryStream(imageBytes))
            using (Image image = Image.Load(inputStream))
            {
                image.Mutate(x => x.Crop(new Rectangle(startX, startY, width, height)));
                using (MemoryStream outputStream = new MemoryStream())
                {
                    image.Save(outputStream, new JpegEncoder());
                    return outputStream.ToArray();
                }
            }
        }
        public async Task<byte[]> ResizeImage(string imageUrl, int width, int height)
        {
            byte[] imageBytes = await APIRequestAsync(imageUrl);

            using (MemoryStream inputStream = new MemoryStream(imageBytes))
            using (Image image = Image.Load(inputStream))
            {
                image.Mutate(x => x.Resize(new Size(width, height)));
                using (MemoryStream outputStream = new MemoryStream())
                {
                    image.Save(outputStream, new JpegEncoder());
                    return outputStream.ToArray();
                }
            }
        }

        public async Task<byte[]> RotateImage(string imageUrl, int rotateDegree)
        {
            byte[] imageBytes = await APIRequestAsync(imageUrl);

            using (MemoryStream inputStream = new MemoryStream(imageBytes))
            using (Image image = Image.Load(inputStream))
            {
                image.Mutate(x => x.Rotate(rotateDegree));
                using (MemoryStream outputStream = new MemoryStream())
                {
                    image.Save(outputStream, new JpegEncoder());
                    return outputStream.ToArray();
                }
            }
        }

    private async Task<byte[]> APIRequestAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream imageStream = await response.Content.ReadAsStreamAsync())
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await imageStream.CopyToAsync(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
                else
                {
                    throw new Exception($"Error fetching image from URL: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
    }
