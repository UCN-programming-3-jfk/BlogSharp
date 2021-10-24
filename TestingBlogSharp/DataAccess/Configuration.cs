using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBlogSharp.DataAccess
{
    public static class Configuration 
    {
        public const string CONNECTION_STRING = "Data Source=.;Integrated Security=True;initial catalog=BlogSharp";
    }
}
