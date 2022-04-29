using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SHBankWebService
{
    [DataContract]
    public class Account
    {
        [DataMember]
        [Key]
        public string AccountNumber { get; set; }
        [DataMember]
        public double Balance { get; set; }
        [DataMember]
        public string SecurityCode { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string Token { get; set; }
    }
}