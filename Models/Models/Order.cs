using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Order
    {

        public int OrderId { get; set; }
        public int  UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsFinally { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
