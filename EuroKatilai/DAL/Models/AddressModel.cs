namespace DAL.Models
{
	public class AddressModel
	{
        public int? AddressId { get; set; }

        public int UserId { get; set; }
        public string? Country { get; set; }

	    public string? Region { get; set;} 

	    public string? City { get; set; }

	    public string? ZipCode  { get; set; }

	    public string? Street  { get; set; }
	    
        public string? House { get; set; }

	    public string? Appartment { get; set; }
        
        public string? Code { get; set; }
        public string? PVMCode { get; set; }
        public string? CompanyName { get; set; }

        public string? RecieverName { get; set; }
        public string? RecieverSurname { get; set; }

        public string? Phone  { get; set; }

        public string? Email  { get; set; }
        public string? Delivery { get; set; } = "";

        public int Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}