using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdsBoard.Domain.Entities
{
    public class Advertisement
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int AdId { get; set; }
        [Display(Name = "Ваше ім'я")]
        public string OwnerId { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Будь ласка введіть заголовок")]
        public string Headline { get; set; }

        [Display(Name = "Текст оголошення")]
        [Required(ErrorMessage = "Будь ласка введіть текс оголошення")]
        public string Details { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
