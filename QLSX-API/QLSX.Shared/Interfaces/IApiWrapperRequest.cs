namespace QLSX.Shared.Interfaces
{
    public interface IApiWrapperRequest
    {
        public string RequestPath { get; }

        public bool IsValid { get; }
    }
}
