using System.ComponentModel.DataAnnotations;

namespace webodev.Models
{
    public class SifremiUnuttumViewModel
    {
        [Required(ErrorMessage = "Lütfen bir email adresi girin.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        public string Email { get; set; }
    }
}
