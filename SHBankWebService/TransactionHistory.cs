using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SHBankWebService
{
    [DataContract]
    public class TransactionHistory
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public string SenderAccountNumber { get; set; }

        [DataMember]
        public string ReceiverAccountNumber { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}