using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace  PointOfSale.DAL.Domains
{
    public class Email
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FromEmail { get; set; }

        public string FromPassword { get; set; }

        public string SmtpClient { get; set; }
        public int SmtpPort { get; set; }

        [NotMapped]
        public string ToEmail { get; set; }

        [NotMapped]
        public string MessageSubject { get; set; }

        [NotMapped]
        public string MessageBody { get; set; }

        [NotMapped]
        public bool IsBodyHtml { get; set; }
    }
}
