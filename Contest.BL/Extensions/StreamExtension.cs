using System.IO;

namespace Contest.BL.Extensions
{
    public static class StreamExtension
    {
        public static byte[] GetBytes(this Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                return reader.ReadBytes((int)stream.Length);
            }
        }
    }
}
