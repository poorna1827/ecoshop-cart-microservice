
using CartMicroservice.Models;

namespace Tests.MockData
{
    internal class CartMockData
    {

        public static List<Cart> GetSampleCartItems()
        {
            return new List<Cart>
            {
                new Cart
                {
                CartId = new Guid("10188938-5308-4B19-8E97-57E7F36A6184"),
                CId = new Guid("20188938-5308-4B19-8E97-57E7F36A6184"),
                PId = new Guid("30188938-5308-4B19-8E97-57E7F36A6184"),
                Quantity = 5

                },
                new Cart
                {
                CartId = new Guid("10288938-5308-4B19-8E97-57E7F36A6184"),
                CId = new Guid("20288938-5308-4B19-8E97-57E7F36A6184"),
                PId = new Guid("30288938-5308-4B19-8E97-57E7F36A6184"),
                Quantity = 8

                },

                new Cart
                {
                CartId = new Guid("10388938-5308-4B19-8E97-57E7F36A6184"),
                CId = new Guid("20388938-5308-4B19-8E97-57E7F36A6184"),
                PId = new Guid("30388938-5308-4B19-8E97-57E7F36A6184"),
                Quantity = 10

                },

            };
        }
    }
}
