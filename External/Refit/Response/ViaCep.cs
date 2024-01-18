
namespace Test.Rubens.Raizen.WebApi.External.Refit.Response
{
    public class ViaCep : IViaCep
    {
        private readonly IViaCepRefit _cepRefit;

        public ViaCep(IViaCepRefit cepRefit) => _cepRefit = cepRefit;
       
        public async Task<ViaCepResponse> GetResponse(string zipCode)
        {
            var existing = await _cepRefit.GetDataViaCep(zipCode);

            if (existing is not null && existing.IsSuccessStatusCode)
                return existing.Content;

            return null;

        }
    }
}
