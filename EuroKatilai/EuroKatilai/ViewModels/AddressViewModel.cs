namespace EuroKatilai.ViewModels
{
    public class AddressViewModel
    {
        public enum AddressStatusEnum { Order, Saved, Deleted }
        public int? AddressId { get; set; }

        public int UserId { get; set; }
        public string Country { get; set; }

        public string Region { get; set; } = null!;

        public string City { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string House { get; set; } = null!;

        public string? Appartment { get; set; } = "";

        public string? Code { get; set; } = "";
        public string? PVMCode { get; set; } = "";
        public string? CompanyName { get; set; } = "";

        public string? RecieverName { get; set; }
        public string? RecieverSurname { get; set; }

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Delivery { get; set; } = "";

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public AddressStatusEnum Status { get; set; } = AddressStatusEnum.Order;

    }
}
