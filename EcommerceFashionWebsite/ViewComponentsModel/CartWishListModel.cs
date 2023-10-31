using EcommerceFashionWebsite.Models;

namespace EcommerceFashionWebsite.ViewComponentsModel
{
    public class CartWishListModel
    {
        public int NumberWishList { get; set; }
        public ICollection<CartModel> CartModels { get; set; }

        public int TotalPrice { get; set; } = 0;
    }
}
