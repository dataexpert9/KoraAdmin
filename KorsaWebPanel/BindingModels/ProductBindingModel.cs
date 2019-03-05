using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasketWebPanel.BindingModels
{
    public class ProductBindingModel
    {

        public ProductBindingModel()
        {
            ProductImages = new List<ProductImagesBindingModel>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1, 10000, ErrorMessage = "Please enter a valid price")]
        [RegularExpression(MyRegularExpressions.Price, ErrorMessage = "Please enter a valid price")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        // [Required]
        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public short Status { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select category")]
        public int Category_Id { get; set; }

        public int Store_Id = Global.StoreId;

        public string Size { get; set; }

        public bool IsPopular { get; set; }

        public virtual CategoryBindingModel Category { get; set; }

        public virtual StoreBindingModel Store { get; set; }

        public List<ProductImagesBindingModel> ProductImages;

    }

    public class ProductImagesBindingModel
    {
        public int Id { get; set; }

        public string Url { get; set; }
        public string Title { get; set; }
        public int? Product_Id { get; set; } = 0;
        public bool IsVideo { get; set; }
        public string Description { get; set; }
        public ProductBindingModel Product { get; set; }
    }
}