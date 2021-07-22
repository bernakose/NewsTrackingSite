using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models.ViewModels
{
    public class NContactModel
    {
        [Required(ErrorMessage = "İsmi girmeyi unuttunuz !")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-Posta adresi girmeyi unuttunuz !")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Geçerli bir E-Posta giriniz !")]
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Telefon numarasınını girmeyi unuttunuz!")]
        [Display(Name = "Telefon No")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Mesaj içeriğini girmeyi unuttunuz!")]
        [Display(Name = "Mesaj İçeriği")]
        public string Message { get; set; }
    }
}