using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.Modèle;

namespace WebProject2017Server.EF.Mappers
{
    public class EFAddressMapper : EFMapper<Address>
    {
        public EFAddressMapper(string connectionString): base(connectionString) { }





        public Expression<Func<Address, bool>> buildExpression(Address address)
        {
            Expression<Func<Address, bool>> expression = a => a.ID >=0;
            var pre = PredicateBuilder.True<Address>();
            
            if(address == null)
            {
                return null;
            }
            else if (address.ID > 0)
            {
                Expression<Func<Address, bool>> expression2 = a => a.ID == address.ID;
                var body = Expression.AndAlso(expression.Body, expression2.Body);
                expression = Expression.Lambda<Func<Address, bool>>(Expression.AndAlso(expression.Body, expression2.Body), expression.Parameters.Single());
            }
            else if (address.Country != null && address.Country != "")
            {
                Expression<Func<Address, bool>> expression2 = a => a.Country == address.Country;
                var body = Expression.AndAlso(expression.Body, expression2.Body);
                expression = Expression.Lambda<Func<Address, bool>>(Expression.AndAlso(expression.Body, expression2.Body), expression.Parameters.Single());
            }
            else if (address.Locality != null && address.Locality != "")
            {
                Expression<Func<Address, bool>> expression2 = a => a.Locality == address.Locality;
                var body = Expression.AndAlso(expression.Body, expression2.Body);
                expression = Expression.Lambda<Func<Address, bool>>(Expression.AndAlso(expression.Body, expression2.Body), expression.Parameters.Single());
            }
            else if (address.PostalCode != null && address.PostalCode != "")
            {
                Expression<Func<Address, bool>> expression2 = a => a.PostalCode == address.PostalCode;
                var body = Expression.AndAlso(expression.Body, expression2.Body);
                expression = Expression.Lambda<Func<Address, bool>>(Expression.AndAlso(expression.Body, expression2.Body), expression.Parameters.Single());
            }
            else if (address.Road != null && address.Road != "")
            {
                //Expression<Func<Address, bool>> expression2 = a2 => a2.Road == address.Road;
                pre.And(a2 => a2.Road == address.Road);
                //var body = Expression.And(expression.Body, expression2.Body);
                //expression = Expression.Lambda<Func<Address, bool>>(Expression.AndAlso(expression.Body, expression2.Body), expression.Parameters.Single());
            }
            else if (address.Number != null && address.Number != "")
            {
                pre.And(a2 => a2.Number == address.Number);
                //Expression<Func<Address, bool>> expression2 = a2 => a2.Number == address.Number;
                //var body = Expression.And(expression.Body, expression2.Body);
                //expression = Expression.Lambda<Func<Address, bool>>(Expression.AndAlso(expression.Body, expression2.Body), expression.Parameters.Single());
            }
            return pre;
        }
        
    }
}
