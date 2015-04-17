using System.Threading.Tasks;
namespace OnionWebApiStarterKit.Core.Services.Decorators
{
    public interface IAsyncPreRequestHandler<in TRequest>
    {
        Task Handle(TRequest request);
    }
}
