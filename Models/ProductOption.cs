using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RefactorThis.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        //[JsonIgnore]
        //public bool IsNew { get; set; }
    }
}
