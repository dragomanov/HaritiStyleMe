using HaritiStyleMe.Models;
using HaritiStyleMe.Web.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HaritiStyleMe.Web.Models
{
    public class ServiceItemViewModel : IMapFrom<ServiceItem>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }
    }
}