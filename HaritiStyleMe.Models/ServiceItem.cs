using System;
using System.ComponentModel.DataAnnotations;

namespace HaritiStyleMe.Models
{
    public class ServiceItem
    {
        #region Constructors
        public ServiceItem()
        {
        }

        public ServiceItem(string name, decimal price, TimeSpan duration, Category category)
        {
            this.Name = name;
            this.Price = price;
            this.Duration = duration;
            this.Category = category;
        }
        #endregion

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "ServiceItem name must be between {2} and {1} characters")]
        public string Name { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Price must be non-negative")]
        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
