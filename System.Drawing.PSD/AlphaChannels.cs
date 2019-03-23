using System.Collections.Generic;
using System.IO;

namespace System.Drawing.PSD
{
    public class AlphaChannels : ImageResource
    {
        public IEnumerable<string> ChannelNames => _channelNames;

        public AlphaChannels() : base(1006)
        {
            _channelNames = new List<string>();
        }

        public AlphaChannels(ImageResource imageResource) : base(imageResource)
        {
            _channelNames = new List<string>();
            var dataReader = imageResource.DataReader;
            while (dataReader.BaseStream.Length - dataReader.BaseStream.Position > 0L)
            {
                var count = dataReader.ReadByte();
                var text = new string(dataReader.ReadChars(count));
                if (text.Length > 0)
                {
                    _channelNames.Add(text);
                }
            }
            dataReader.Close();
        }

        protected override void StoreData()
        {
            var memoryStream = new MemoryStream();
            var binaryReverseWriter = new BinaryReverseWriter(memoryStream);
            foreach (var text in ChannelNames)
            {
                binaryReverseWriter.Write((byte)text.Length);
                binaryReverseWriter.Write(text.ToCharArray());
            }
            binaryReverseWriter.Close();
            memoryStream.Close();
            Data = memoryStream.ToArray();
        }

        private List<string> _channelNames;
    }
}