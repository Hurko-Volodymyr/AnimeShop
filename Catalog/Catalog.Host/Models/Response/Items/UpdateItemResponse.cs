namespace Catalog.Host.Models.Response.Items
{
    public class UpdateItemResponse<T>
    {
        public T IsUpdated { get; set; } = default(T) !;
    }
}
