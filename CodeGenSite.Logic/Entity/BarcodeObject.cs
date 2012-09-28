using System.IO;

namespace CodeGenSite.Logic.Entity
{
    public class BarcodeObject
    {
        public string Base64Image { get; set; }

        public byte[] ImageBytes;

        public string Base64ImageString
        {
            get { return "data:image/png;base64," + Base64Image; }
        }
    }
}
