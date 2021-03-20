using Dapper.Contrib.Extensions;
using System.Runtime.Serialization;

namespace TiendaVirtual.Entities.Models
{
    [DataContract]
    [Table("Status")]
    public class Status
    {
        public Status()
        {
        }

        [Key]
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Status_Description { get; set; }

        public override bool Equals(object obj)
        {
            var conversionObjeto = obj as Status;
            if (conversionObjeto == null)
            {
                return false;
            }
            if (this.Id != conversionObjeto.Id ||
                this.Status_Description != conversionObjeto.Status_Description)
            {
                return false;
            }
            return true;
        }
    }
}
