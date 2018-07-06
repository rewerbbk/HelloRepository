using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Utility
{
    public static class Compression
    {

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string ZipBase64(string str)
        {
            byte[] zipped = Zip(str);
            return Convert.ToBase64String(zipped);
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static string UnZipBase64(string str)
        {
            byte[] zipped = Convert.FromBase64String(str);
            return Unzip(zipped);
        }

        public static void CopyTo(Stream source, Stream destination)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = source.Read(bytes, 0, bytes.Length)) != 0)
            {
                destination.Write(bytes, 0, cnt);
            }
        }
    }
}
