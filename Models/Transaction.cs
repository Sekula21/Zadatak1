using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Zadatak1.Models
{
    public class Transaction
    {
        [Key]
        public Guid ID { get; set; }
        [AllowNull]
        public Guid BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        [AllowNull]
        public User Buyer { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
    }
}
