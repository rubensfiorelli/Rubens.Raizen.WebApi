using System.Reflection.Metadata.Ecma335;

namespace Test.Rubens.Raizen.WebApi.External.Refit.Response
{

    public class ViaCepResponse
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    }
}
