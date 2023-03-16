namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaFileRequestIndex
    {
        Task RebuildIndex();
        Task BuildIndex();
        IList<IMediaFileRequestIndexItem> GetIndexOrderByDate();
    }

    public interface IMediaFileRequestIndexItem
    {
        Guid Id { get; }
        Guid MediaDirectoryId { get; }
        DateTimeOffset MediaFileShootingDate { get; }
    }
}
