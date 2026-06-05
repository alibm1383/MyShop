namespace Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<CategoryToProduct>? CategoryToProducts { get; set; }
    }
}
