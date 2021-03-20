using Dapper.Contrib.Extensions;
using System;
using System.Runtime.Serialization;

namespace TiendaVirtual.Entities.Models
{
    [DataContract]
    [Table("Orders")]
    public class Orders : IDisposable
    {
        public Orders()
        {
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Customer_Name { get; set; }

        [DataMember]
        public string Customer_Email { get; set; }

        [DataMember]
        public string Customer_Mobile { get; set; }

        [DataMember]
        public int Status_Id { get; set; }

        [DataMember]
        public DateTime Created_At { get; set; }

        [DataMember]
        public DateTime? Updated_At { get; set; }

        [DataMember]
        public string Request_Id { get; set; }

        [DataMember]
        [Computed]
        public Status OrderStatus { get; set; }

        [DataMember]
        [Computed]
        public string UrlRaiz { get; set; }

        [DataMember]
        [Computed]
        public string UrlProcesamiento { get; set; }

        ~Orders()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.OrderStatus = null;
        }

        public override bool Equals(object obj)
        {
            var conversionObjeto = obj as Orders;
            if (conversionObjeto == null)
            {
                return false;
            }
            if (this.Customer_Email != conversionObjeto.Customer_Email ||
                this.Customer_Mobile != conversionObjeto.Customer_Mobile ||
                this.Customer_Name != conversionObjeto.Customer_Name ||
                this.Created_At != conversionObjeto.Created_At ||
                this.Updated_At != conversionObjeto.Updated_At ||
                this.UrlProcesamiento != conversionObjeto.UrlProcesamiento ||
                this.UrlRaiz != conversionObjeto.UrlRaiz ||
                this.Id != conversionObjeto.Id ||
                this.Request_Id != conversionObjeto.Request_Id ||
                this.Status_Id != conversionObjeto.Status_Id ||
                !this.OrderStatus.Equals(conversionObjeto.OrderStatus))
            {
                return false;
            }
            return true;
        }
    }
}
