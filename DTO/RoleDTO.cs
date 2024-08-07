using System.ComponentModel.DataAnnotations;

namespace SmartTicket.DTO
{
	public class RoleDTO
	{

		[Required]
		[MinLength(1)]
        public string Name { get; set; }

    }
}
