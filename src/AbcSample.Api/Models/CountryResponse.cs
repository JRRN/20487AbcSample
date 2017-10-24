using T4TS;

namespace AbcSample.Api.Models
{
    [TypeScriptInterface]
    public class CountryResponse
    {
        public string Id { get; set; }

        public string Description { get; set; }
        
    }
}