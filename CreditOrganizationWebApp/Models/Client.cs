using System.ComponentModel.DataAnnotations;

namespace CreditOrganizationWebApp.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
    }
}
