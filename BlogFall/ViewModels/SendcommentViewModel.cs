using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogFall.ViewModels
{
    public class SendcommentViewModel
    {
        public int? ParentId { get; set; }

        public int PostId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir E-mail Adresi giriniz.")]
        public string AuthorEmail { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }
    }
}