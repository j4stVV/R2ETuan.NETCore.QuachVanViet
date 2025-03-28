namespace R2ETuan.NETCore.QuachVanViet.Services.Logging
{
    public interface IRequestLogger
    {
        Task LogRequestAsync(string scheme, string host, string path, string queryString, string requestBody);
    }
}
