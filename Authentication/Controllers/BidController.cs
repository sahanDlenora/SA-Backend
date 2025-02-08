using api.Data;
using Authentication.Dtos.Bids;
using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BidController(DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet, Authorize]
        public IActionResult GetBids()
        {
            return Ok(_context.Bids.ToList());
        }

        // Get the highest bid for a specific auction
        [HttpGet("highest"), Authorize]
        public IActionResult GetHighestBid(int auctionId)
        {
            var highestBid = _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefault();

            if (highestBid == null)
                return NotFound("No bids found for this auction.");

            return Ok(highestBid);
        }


        [HttpPost("create"), Authorize]

        public async Task<IActionResult> CreateBid([FromBody] CreateBidDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            var bid = new Bid
            {
                AuctionId = dto.AuctionId,
                BidAmount = dto.BidAmount,
                BidderId = user.Id,
                Status = dto.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return Ok(bid);
        }
    }
}
