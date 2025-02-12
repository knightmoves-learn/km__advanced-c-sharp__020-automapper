using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyApi.Models
{
    public class UtilityProvider
    {
        public int Id {get; set;}
        public string? ProvidedUtility {get; set;}
        public int HomeId {get; set;}
        [JsonIgnore]
        public Home? Home {get; set;} = null!;
    }
}