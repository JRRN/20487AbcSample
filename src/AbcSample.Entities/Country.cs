using Microsoft.WindowsAzure.Storage.Table;

namespace AbcSample.Entities
{
    public class Country:TableEntity
    {
        public Country(string alpha3):base("countries",alpha3)
        {
        }

        public string Description { get; set; }
    }
}
