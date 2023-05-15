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
    public class ClientsController : ControllerBase
    {
        private readonly CreditOrganizationAPIContext _context;

        public ClientsController(CreditOrganizationAPIContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("Debtors")]
        public async Task<ActionResult<IEnumerable<Client>>> GetDebtors()
        {
            var loans = await _context.Loans.Include(c=>c.Client).ToListAsync();
            var payments = await _context.Payments.ToListAsync();
            var debtors = new List<Client>();
            foreach (var loan in loans)
            {
                var paymentsSum = payments.Where(a => a.LoanId == loan.Id).ToList().Sum(o=>o.PaymentSum);
                int months = 0;

                months = (DateTime.Now.Year - loan.LoanDate.Year) * 12 + (DateTime.Now.Month - loan.LoanDate.Month);

                if (DateTime.Now.Month < loan.LoanDate.Month)
                {
                    months-=12;
                }

                if (DateTime.Now.Day < loan.LoanDate.Day)
                {
                    months--; 
                }
                if ( loan.LoanSum / loan.LoanPeriod * months > paymentsSum)
                {
                    debtors.Add(loan.Client);
                }
            }

            if (debtors == null)
            {
                return NotFound();
            }
            return debtors;
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
          if (_context.Clients == null)
          {
              return Problem("Entity set 'CreditOrganizationAPIContext.Clients'  is null.");
          }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
