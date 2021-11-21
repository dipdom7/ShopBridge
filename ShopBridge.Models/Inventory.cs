using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Models
{
    public class Inventory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name length can be 20 chars long")]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }

        public float Price { get; set; }

        [DisplayName("Upload Image")]
        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [DataType(DataType.Upload)]
        [NotMapped]
        public IFormFile ImageFile
        {
            get; set;

        }
    }
}
