using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaritiStyleMe.Models
{
    public class Category
    {
        #region Contructors
        public Category()
        {
            this.ServiceItems = new List<ServiceItem>();
            this.Employees = new List<User>();
        }

        public Category(string name)
            : this()
        {
            this.Name = name;
        }
        #endregion

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be at between {2} and {1} characters")]
        public string Name { get; set; }

        public virtual ICollection<ServiceItem> ServiceItems { get; set; }

        public virtual ICollection<User> Employees { get; set; }
    }
}
