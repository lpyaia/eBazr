namespace Order.Core.Common
{
    public static class ContextNames
    {
        public static class Exchange
        {
            public static string BasketCheckout => @"Basket\Checkout";
        }

        public static class Queue
        {
            public static string OrderCheckout => @"Order\Checkout";
        }
    }
}
