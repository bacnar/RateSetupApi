using System.IO;

namespace RateSetup.Models.FileUpload
{
    public class GetObjectModel
    {
        public string ContentType { get; set; }

        public Stream Content { get; set; }
    }
}
