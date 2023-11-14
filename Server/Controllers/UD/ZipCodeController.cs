using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models; // Replace this with the actual namespace
using OCTOBER.Shared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipcodeController : BaseController, GenericRestController<ZipcodeDTO>
    {
        public ZipcodeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{Zip}")]
        public async Task<IActionResult> Get(string Zip)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                ZipcodeDTO? result = await _context
                    .Zipcodes
                    .Where(x => x.Zip == Zip)
                    .Select(z => new ZipcodeDTO
                    {
                        Zip = z.Zip,
                        City = z.City,
                        State = z.State,
                        CreatedBy = z.CreatedBy,
                        CreatedDate = z.CreatedDate,
                        ModifiedBy = z.ModifiedBy,
                        ModifiedDate = z.ModifiedDate
                    })
                    .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Zipcodes.Select(z => new ZipcodeDTO
                {
                    Zip = z.Zip,
                    City = z.City,
                    State = z.State,
                    CreatedBy = z.CreatedBy,
                    CreatedDate = z.CreatedDate,
                    ModifiedBy = z.ModifiedBy,
                    ModifiedDate = z.ModifiedDate
                })
                .ToListAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes
                    .Where(x => x.Zip == _ZipcodeDTO.Zip)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Zipcode zipcode = new EF.Models.Zipcode
                    {
                        Zip = _ZipcodeDTO.Zip,
                        City = _ZipcodeDTO.City,
                        State = _ZipcodeDTO.State,
                        CreatedBy = _ZipcodeDTO.CreatedBy,
                        CreatedDate = _ZipcodeDTO.CreatedDate,
                        ModifiedBy = _ZipcodeDTO.ModifiedBy,
                        ModifiedDate = _ZipcodeDTO.ModifiedDate
                    };

                    _context.Zipcodes.Add(zipcode);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes
                    .Where(x => x.Zip == _ZipcodeDTO.Zip)
                    .FirstOrDefaultAsync();

                itm.Zip = _ZipcodeDTO.Zip;
                itm.City = _ZipcodeDTO.City;
                itm.State = _ZipcodeDTO.State;
                itm.CreatedBy = _ZipcodeDTO.CreatedBy;
                itm.CreatedDate = _ZipcodeDTO.CreatedDate;
                itm.ModifiedBy = _ZipcodeDTO.ModifiedBy;
                itm.ModifiedDate = _ZipcodeDTO.ModifiedDate;

                _context.Zipcodes.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpDelete]
        [Route("Delete/{Zip}")]
        public async Task<IActionResult> Delete(string Zip)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes
                    .Where(x => x.Zip == Zip)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Zipcodes.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }
    }
}
