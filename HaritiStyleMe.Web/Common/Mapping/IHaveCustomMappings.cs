using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaritiStyleMe.Web.Common.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}