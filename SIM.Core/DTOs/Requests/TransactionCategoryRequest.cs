#nullable disable

namespace SIM.Core.DTOs.Requests
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryRequest : CreateCategoryRequest 
    {
        public int Id { get; set; }
    }
}
