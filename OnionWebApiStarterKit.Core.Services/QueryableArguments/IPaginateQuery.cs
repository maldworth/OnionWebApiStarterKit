namespace OnionWebApiStarterKit.Core.Services
{
    public interface IPaginateQuery<T> : IOrderByQuery<T>, ITakeQuery
    {
        int PageIndex { get; }
    }
}
