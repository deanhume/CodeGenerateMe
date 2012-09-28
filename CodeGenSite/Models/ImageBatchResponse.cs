namespace CodeGenSite.Models
{
    public class ImageBatchResponse
    {
        public int SuccessfulCount { get; set; }
        public ImageResponse[] Images { get; set; }
    }
}