using Contest.BL.Exceptions;
using Contest.BL.Interfaces;
using Contest.DA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Contest.BL.Services
{
    public class ImageService : IImageService
    {
        private const int Size = 1000;
        private const long Quality = 75;
        private readonly ContestContext _db;

        public ImageService(ContestContext db)
        {
            _db = db;
        }

        public async Task UploadContestCover(byte[] coverImageBytes, int contestId)
        {
            var contest = await _db.Contests.FirstOrDefaultAsync(x => x.Id == contestId);
            if (contest == null)
            {
                throw new NotFoundException();
            }

            var coverPath = await UploadImage(coverImageBytes, @"images\covers\");
            contest.CoverPath = coverPath;

            await _db.SaveChangesAsync();
        }

        private async Task<string> UploadImage(byte[] imageBytes, string path)
        {
            string filePath = await Task.Run(() =>
            {
                try
                {
                    var height = Size;
                    var width = Size;

                    var coverImageStream = new MemoryStream(imageBytes);
                    var coverImageBitmap = new Bitmap(coverImageStream);
                    if (coverImageBitmap.Width > coverImageBitmap.Height)
                    {
                        height = Convert.ToInt32(coverImageBitmap.Height * Size / (double)coverImageBitmap.Width);
                    }
                    else
                    {
                        width = Convert.ToInt32(coverImageBitmap.Width * Size / (double)coverImageBitmap.Height);
                    }

                    var resizedImageBitmap = new Bitmap(width, height);

                    var graphics = Graphics.FromImage(resizedImageBitmap);
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(coverImageBitmap, 0, 0, width, height);

                    var fileGuid = Guid.NewGuid().ToString();
                    var fileName = Path.GetFileName($"{fileGuid}.jpg");

                    var dirPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", path, fileName.Substring(0, 1));
                    var fileDir = Directory.CreateDirectory(dirPath);

                    var filePath = Path.Combine(dirPath, fileName);

                    var output = File.Open(filePath, FileMode.Create);

                    var qualityParamId = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, Quality);

                    var codec = ImageCodecInfo.GetImageDecoders()
                                              .FirstOrDefault(x => x.FormatID == ImageFormat.Jpeg.Guid);

                    resizedImageBitmap.Save(output, codec, encoderParameters);

                    output.Close();
                    resizedImageBitmap.Dispose();
                    coverImageBitmap.Dispose();
                    coverImageStream.Close();

                    return @"\" + path + fileName.Substring(0, 1) + @"\" + fileName;
                }
                catch (Exception)
                {
                    throw new ImageProcessingException();
                }
            });

            return filePath;
        }
    }
}
