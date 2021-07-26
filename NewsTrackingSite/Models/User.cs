using NewsTrackingSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
        }

        [Key]

        public int UserID { get; set; }

        [Column(TypeName ="image")]
        [Display(Name ="Kullanıcı Foto")]
        public byte[] ProfilPicture { get; set; }

        [Required(ErrorMessage = "Kullanıcı ismi giriniz !")]
        [MinLength(3, ErrorMessage = "Lütfen en az 3 harfli isim Giriniz !")]
        [MaxLength(20, ErrorMessage = "Lütfen en fazla 20 harfli İsim Giriniz !")]
        [Display(Name = "Kullanıcı Adı")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Kullanıcı soyadını giriniz !")]
        [MinLength(3, ErrorMessage = "Lütfen SoyAdınızı Giriniz !")]
        [MaxLength(10, ErrorMessage = "Lütfen SoyAdınızı Giriniz !")]
        [Display(Name = "Soyadınız")]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Geçerli bir E-Posta giriniz !")]
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Lütfen Telefon Numaranızı Giriniz !")]
        [MaxLength(14, ErrorMessage = "Lütfen Telefon Numaranızı Giriniz !")]
        [Display(Name = "Telefon Numaranız")]
        public string Telephone { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Kullanıcı Türü")]
        public MemberType MemberType { get; set; }
    }
}