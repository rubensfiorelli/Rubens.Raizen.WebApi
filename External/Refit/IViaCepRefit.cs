using Refit;
using Test.Rubens.Raizen.WebApi.External.Refit.Response;

namespace Test.Rubens.Raizen.WebApi.External.Refit
{
    public interface IViaCepRefit
    {
        [Get("/ws/{zipCode}/json")]
        Task<ApiResponse<ViaCepResponse>> GetDataViaCep(string zipCode);
    }
}
