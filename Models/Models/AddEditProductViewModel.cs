using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Models.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام کالا را وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا توضیحات کالا را وارد کنید")]
        public string Description { get; set; }
        [Required(ErrorMessage = "لطفا قیمت کالا را وارد کنید")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "لطفا تعداد کالا را وارد کنید")]
        public int Quantity { get; set; } = 10;
        [Required(ErrorMessage = "لطفا تصویر کالا را وارد کنید")]
        public IFormFile Picture { get; set; }
        [MinLength(1, ErrorMessage = "لطفا حداقل یک گروه انتخاب کنید")]
        public List<int> CategoriesId { get; set; } = [];
    }
}
