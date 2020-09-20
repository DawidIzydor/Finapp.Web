using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Di.Tools.Creator;
using Finapp.Database;
using Finapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finapp.WebApi.Controllers
{
    // ReSharper disable once StringLiteralTypo

    /// <summary>
    ///     Controller for financial statuses
    /// </summary>
    [Route("api/finstatus")]
    [ApiController]
    [Authorize]
    public class FinancialStatusController : ControllerBase
    {
        private readonly FinappDbContext _dbContext;

        /// <inheritdoc />
        public FinancialStatusController(FinappDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/FinancialStatuses
        /// <summary>
        ///     Gets user financial source entries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FinancialStatusDto>>> GetFinancialSourceEntries()
        {
            return await GetUserFinancialStatuses().Cast<FinancialStatusDto>().ToListAsync();
        }

        private IQueryable<FinancialStatus> GetUserFinancialStatuses()
        {
            var currentUser = UserProvider.GetCurrentUser(HttpContext);
            return _dbContext.FinancialStatuses.Where(fs => fs.UserName == currentUser.Name);
        }

        // GET: api/FinancialStatuses/5
        /// <summary>
        ///     Gets a financial status entry
        /// </summary>
        /// <param name="id">Id of financial status</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<FinancialStatusDto>> GetFinancialSourceEntry(Guid id)
        {
            var financialSourceEntry = await GetUserFinancialStatuses().SingleOrDefaultAsync(fs => fs.Id == id);

            if (financialSourceEntry == null)
            {
                return NotFound();
            }

            return financialSourceEntry;
        }

        // PUT: api/FinancialStatuses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Modifies financial status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="financialStatus"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutFinancialSourceEntry(Guid id, FinancialStatusDto financialStatus)
        {
            if (await GetUserFinancialStatuses().SingleOrDefaultAsync(fs => fs.Id == id) == default)
            {
                return NotFound();
            }

            _dbContext.Entry(financialStatus).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialSourceEntryExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/FinancialStatuses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Inserts a new financial status
        /// </summary>
        /// <param name="financialStatusDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostFinancialSourceEntry(
            FinancialStatusDto financialStatusDto)
        {
            var financialStatus = financialStatusDto.CreateChild<FinancialStatus, FinancialStatusDto>();
            financialStatus.UserName = UserProvider.GetCurrentUser(HttpContext).Name;
            await _dbContext.FinancialStatuses.AddAsync(financialStatus);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetFinancialSourceEntry", new {id = financialStatus.Id}, new {financialStatus.Id});
        }

        // DELETE: api/FinancialStatuses/5
        /// <summary>
        ///     Deletes a financial status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<FinancialStatusDto>> DeleteFinancialSourceEntry(Guid id)
        {
            var financialSourceEntry = await GetUserFinancialStatuses().SingleOrDefaultAsync(fs => fs.Id == id);
            if (financialSourceEntry == null)
            {
                return NotFound();
            }

            _dbContext.FinancialStatuses.Remove(financialSourceEntry);
            await _dbContext.SaveChangesAsync();

            return financialSourceEntry;
        }

        private bool FinancialSourceEntryExists(Guid id)
        {
            return _dbContext.FinancialStatuses.Any(e => e.Id == id);
        }
    }
}