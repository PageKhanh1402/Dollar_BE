using System.ComponentModel.DataAnnotations;

namespace DollarProject.Enums
{
    public enum ActivityType
    {
        Login,
        Logout,
        ProductView,
        AddToCart,
        RemoveFromCart,
        AddToWishlist,
        RemoveFromWishlist,
        PlaceOrder,
        CancelOrder,
        LeaveReview,
        UpdateProfile,
        ChangePassword
    }
}