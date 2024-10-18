﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Authentication.Models
{
    public class Auction
    {
        [Key]
        public int AuctionId { get; set; }


        [StringLength(255)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuctionImage { get; set; } = string.Empty;
        public string AuctionCategory { get; set; } = string.Empty;
        public string SellerId { get; set; }

        public int? WinnerId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal StartingBid { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal? WinningBid { get; set; }

        [StringLength(100)]
        public string? Status { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;



        public IdentityUser Seller { get; set; } // Reference to the user

    }
}
