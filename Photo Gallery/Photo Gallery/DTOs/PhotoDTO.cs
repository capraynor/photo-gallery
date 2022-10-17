namespace Photo_Gallery.DTOs
{
    public class PhotoDTO
    {
        string FileName { get; set; }
        string FilePath { get; set; }
        string ThumbnailFilePath { get; set; }
        string ShottingDate { get; set; }
        string CreatedDate { get; set; }

        double Longitude { get; set; }
        double Latitude { get; set; }
        string MD5Str { get; set; }
        Guid Id { get; set; }
        

    }
}
