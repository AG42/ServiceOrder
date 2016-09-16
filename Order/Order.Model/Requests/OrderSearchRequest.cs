namespace Order.Model.Requests
{
    public class OrderSearchRequest
    {
        public string OrderCode { get; set; }
        // public string CountryCode { get; set; }
        public string ComapanyCode { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ComapanyCode);
        }
    }
}
