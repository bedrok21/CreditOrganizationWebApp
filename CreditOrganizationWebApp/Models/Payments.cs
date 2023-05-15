using System.ComponentModel.DataAnnotations;

namespace CreditOrganizationWebApp.Models
{
    public class Payment
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public decimal PaymentSum { get; set; }


        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public DateTime PaymentDate { get; set; }


        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public int LoanId { get; set; }

        public virtual Client? Client { get; set; }

        public virtual Loan? Loan { get; set; } 
    }
}
