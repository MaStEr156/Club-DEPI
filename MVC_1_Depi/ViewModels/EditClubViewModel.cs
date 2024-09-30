using MVC_1_Depi.Data.Enums;
using MVC_1_Depi.Models;

namespace MVC_1_Depi.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
    }
}
