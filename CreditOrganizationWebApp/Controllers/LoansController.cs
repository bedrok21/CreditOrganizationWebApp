using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreditOrganizationWebApp.Models;

namespace CreditOrganizationWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly CreditOrganizationAPIContext _context;

        public LoansController(CreditOrganizationAPIContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var dbauctionContext = _context.Loans.Include(a=>a.Client);
          if (dbauctionContext == null)
          {
              return NotFound();
          }
            return await dbauctionContext.ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
          if (_context.Loans == null)
          {
              return NotFound();
          }
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
          if (_context.Loans == null)
          {
              return Problem("Entity set 'CreditOrganizationAPIContext.Loans'  is null.");
          }
            var client = await _context.Clients.FindAsync(loan.ClientId);

            if (client == null)
            {
                return Problem("Client Doesn`t Exists.");
            }

            loan.Client = client;
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            if (_context.Loans == null)
            {
                return NotFound();
            }
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return (_context.Loans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
