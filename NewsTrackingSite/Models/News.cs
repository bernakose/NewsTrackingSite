using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models
{
    [Table("News")]
    public partial class News
    {
        public News()
        {
            Genre = new HashSet<Genre>();
        }
        [Key]
        public int NewsID { get; set; }

        [Required(ErrorMessage = "Lütfen Haberin Adını Giriniz !")]
        [Display(Name = "Haber Adı")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Konu Giriniz !")]
        [MinLength(20,ErrorMessage ="Lütfen Haber Konusu Giriniz !")]
        [Display(Name = "Haber Konusu")]
        [DataType(DataType.MultilineText)]

        public string Description { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Haber Foto")]
        public byte[] Poster { get; set; }

        [Required(ErrorMessage = "Lütfen Haberin Çıkış Tarihini Giriniz !")]
        [Display(Name = "Çıkış Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Lütfen Haberin Çıkış Yerini Giriniz !")]
        [Display(Name = "Haber Yayınlanma Şehri")]
        public string ReleaseCountry { get; set; }

        [Required(ErrorMessage = "Lütfen Haberin Fragman Linkini Giriniz !")]
        [Display(Name = "Fragman")]
        [DataType(DataType.Url)]
        public string TrailerLink { get; set; }

        
        public virtual ICollection<Genre> Genre { get; set; }
    }
}