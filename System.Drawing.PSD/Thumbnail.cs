using System.Drawing.Imaging;
using System.IO;

namespace System.Drawing.PSD
{
    public class Thumbnail : ImageResource
    {
        public Bitmap Image { get; private set; }

        public Thumbnail(ImageResource imageResource) : base(imageResource)
        {
            using (var dataReader = DataReader)
            {
                var num = dataReader.ReadInt32();
                var width = dataReader.ReadInt32();
                var height = dataReader.ReadInt32();
                dataReader.ReadInt32();
                dataReader.ReadInt32();
                dataReader.ReadInt32();
                dataReader.ReadInt16();
                dataReader.ReadInt16();
                if (num == 1)
                {
                    using (var memoryStream = new MemoryStream(dataReader.ReadBytes(
                        (int) (dataReader.BaseStream.Length - dataReader.BaseStream.Position))))
                        Image = (Bitmap) System.Drawing.Image.FromStream(memoryStream).Clone();
                    if (ID == 1033)
                    {
                    }
                }
                else
                    Image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            }
        }
    }
}