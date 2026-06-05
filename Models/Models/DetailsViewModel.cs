using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DetailsViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
