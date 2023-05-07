namespace Catalog.Host.Models.Response.Items
{
    public class DeleteItemResponse<T>
    {
        public T IsDeleted { get; set; } = default(T) !;
    }
}
