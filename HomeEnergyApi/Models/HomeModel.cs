using System.ComponentModel.DataAnnotations;
namespace HomeEnergyApi.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }

        public string OwnerLastName { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public HomeUsageData? HomeUsageData { get; set; }

        public ICollection<UtilityProvider> UtilityProviders { get; set; }

        public Home(string ownerLastName, string? streetAddress, string? city)
        {
            OwnerLastName = ownerLastName;
            StreetAddress = streetAddress;
            City = city;
        }
    }
}