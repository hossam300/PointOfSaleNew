using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string Path { get; set; }
    }
}
