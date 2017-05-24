using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.EF.Mappers;

namespace WebProject2017Server.Modèle
{
    public class Address : WithID
    {
        public string Country { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string Road { get; set; }
        public string Number { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Journey> JourneysStaterAddress { get; set; }
        public virtual ICollection<Journey> JourneysDriverUpAddress { get; set; }

        public Address()
        {

        }
        public Address(Address address)
        {
            this.ID = address.ID;
            this.Country = address.Country;
            this.Locality = address.Locality;
            this.PostalCode = address.PostalCode;
            this.Road = address.Road;
            this.Number = address.Number;
        }
        public Address(string country, string locality,string postalCode, string road, string number)
        {
            this.Country = country;
            this.Locality = locality;
            this.PostalCode = postalCode;
            this.Road = road;
            this.Number = number;
        }
    }
}
