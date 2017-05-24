using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.EF.Mappers;

namespace WebProject2017Server.Modèle
{
    public class User : WithID
    {
        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings =false ,ErrorMessage ="Le login est invalide")]
        public string Login { get; set; }
        [DataType (DataType.Password)]
        [Required(AllowEmptyStrings = false,  ErrorMessage = "Le mot de passe est invalide")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom est invalide")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom est invalide")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le numero de téléphone est invalide")]
        public string Phone { get; set; }
        [Index(IsUnique =true)]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse Email est invalide")]
        public string Email { get; set; }
        [DefaultValue(UserRank.Member)]
        public UserRank? Rank { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<Journey> UserJourneys { get; set; }
        public virtual List<Journey> DriverJourneys { get; set; }

        public User()
        {

        }
        public User(string login, string password, string phone, string email, UserRank rank, Address userAddress)
        {
            this.Login = login;
            this.Password = password;
            this.Phone = phone;
            this.Email = email;
            this.Rank = rank;
            this.Address = userAddress;
        }
    }

    public enum UserRank
    {
        Admin,
        Moderator,
        Member        
    }
}
