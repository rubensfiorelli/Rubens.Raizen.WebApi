using Test.Rubens.Raizen.WebApi.External.Refit.Response;

namespace Test.Rubens.Raizen.WebApi.External.Refit
{
    public interface IViaCep
    {
        Task<ViaCepResponse> GetResponse(string zipCode);
    }
}
