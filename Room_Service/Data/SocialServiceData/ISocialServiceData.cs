namespace Room_Service.Data
{
    public interface ISocialServiceData
    {
        Task<bool> IsUserValid(string id);
    }
}
