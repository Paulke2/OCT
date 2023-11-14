using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models; // Make sure to replace this with the correct namespace
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
    public class GradeTypeController : BaseController, GenericRestController<GradeTypeDTO>
    {
        public GradeTypeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{SchoolId}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolId, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeDTO? result = await _context
                    .GradeTypes
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Select(gt => new GradeTypeDTO
                    {
                        SchoolId = gt.SchoolId,
                        GradeTypeCode = gt.GradeTypeCode,
                        Description = gt.Description,
                        CreatedBy = gt.CreatedBy,
                        CreatedDate = gt.CreatedDate,
                        ModifiedBy = gt.ModifiedBy,
                        ModifiedDate = gt.ModifiedDate
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

                var result = await _context.GradeTypes.Select(gt => new GradeTypeDTO
                {
                    SchoolId = gt.SchoolId,
                    GradeTypeCode = gt.GradeTypeCode,
                    Description = gt.Description,
                    CreatedBy = gt.CreatedBy,
                    CreatedDate = gt.CreatedDate,
                    ModifiedBy = gt.ModifiedBy,
                    ModifiedDate = gt.ModifiedDate
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
        public async Task<IActionResult> Post([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes
                    .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId && x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.GradeType gradeType = new EF.Models.GradeType
                    {
                        SchoolId = _GradeTypeDTO.SchoolId,
                        GradeTypeCode = _GradeTypeDTO.GradeTypeCode,
                        Description = _GradeTypeDTO.Description,
                        CreatedBy = _GradeTypeDTO.CreatedBy,
                        CreatedDate = _GradeTypeDTO.CreatedDate,
                        ModifiedBy = _GradeTypeDTO.ModifiedBy,
                        ModifiedDate = _GradeTypeDTO.ModifiedDate
                    };

                    _context.GradeTypes.Add(gradeType);
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
        public async Task<IActionResult> Put([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes
                    .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId && x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                itm.SchoolId = _GradeTypeDTO.SchoolId;
                itm.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;
                itm.Description = _GradeTypeDTO.Description;
                itm.CreatedBy = _GradeTypeDTO.CreatedBy;
                itm.CreatedDate = _GradeTypeDTO.CreatedDate;
                itm.ModifiedBy = _GradeTypeDTO.ModifiedBy;
                itm.ModifiedDate = _GradeTypeDTO.ModifiedDate;

                _context.GradeTypes.Update(itm);
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
        [Route("Delete/{SchoolId}/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(int SchoolId, string GradeTypeCode)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes
                    .Where(x => x.SchoolId == SchoolId && x.GradeTypeCode == GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypes.Remove(itm);
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

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }
    }
}
