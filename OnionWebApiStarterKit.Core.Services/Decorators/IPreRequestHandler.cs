namespace OnionWebApiStarterKit.Core.Services.Decorators
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}
