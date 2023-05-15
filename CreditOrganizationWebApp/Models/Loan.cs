using System.ComponentModel.DataAnnotations;

namespace CreditOrganizationWebApp.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public decimal LoanSum { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public DateTime LoanDate { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public int LoanPeriod { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }

    }
}
