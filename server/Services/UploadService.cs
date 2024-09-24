




namespace allspice.Services;

public class UploadService
{
    private readonly UploadRepository _uploadRepository;

    public UploadService(UploadRepository uploadRepository)
    {
        _uploadRepository = uploadRepository;
    }
}
