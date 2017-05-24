using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.EF.Mappers;

namespace WebProject2017Server.Modèle
{
    public class Journey : WithID
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "La description est obligatoire")]
        public string Description { get; set; }
        public int FreeSeat { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'heure de départ est invalide")]
        [DataType(DataType.DateTime)]
        public DateTime StartingDateTime { get; set; }
        [Required(AllowEmptyStrings =false ,ErrorMessage ="L'heure d'arrivée est invalide")]
        [DataType(DataType.DateTime)]
        public DateTime DriverUpDatetime { get; set; }

        public virtual User Driver { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse de départ est invalide")]
        public virtual Address StartingAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse d'arrivée est invalide")]
        public virtual Address DriverUpAddress { get; set; }
        public virtual List<User> Passengers { get; set; }

        public Journey()
        {

        }
        public Journey (string description, User driver, Address startingAddress, Address driveUpAddress)
        {
            this.Description = description;
            this.Driver = driver;
            this.StartingAddress = startingAddress;
            this.DriverUpAddress = driveUpAddress;
            
        }

    }
}
