using System.ComponentModel.DataAnnotations;

namespace PhotoSharing.Web.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public virtual Photo Photo { get; set; }
    }
}