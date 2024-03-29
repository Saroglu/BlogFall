﻿using BlogFall.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogFall.ViewModels
{
    public class UploadAvatarViewModel
    {
        public string Photo { get; set; }

        [Required(ErrorMessage ="Lütfen bir resim dosyası seçiniz.")]
        [ProfilePhotoAttribute(MaxFileSize =900000, ErrorMessage =" 1 Mb'den küçük bir resim dosyası giriniz")]
        public HttpPostedFileBase File { get; set; }
    }
}