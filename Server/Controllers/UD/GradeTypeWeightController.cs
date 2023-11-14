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
    public class GradeTypeWeightController : BaseController, GenericRestController<GradeTypeWeightDTO>
    {
        public GradeTypeWeightController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{SchoolId}/{SectionId}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolId, int SectionId, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeWeightDTO? result = await _context
                    .GradeTypeWeights
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Select(gt => new GradeTypeWeightDTO
                    {
                        SchoolId = gt.SchoolId,
                        SectionId = gt.SectionId,
                        GradeTypeCode = gt.GradeTypeCode,
                        NumberPerSection = gt.NumberPerSection,
                        PercentOfFinalGrade = gt.PercentOfFinalGrade,
                        DropLowest = gt.DropLowest,
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

                var result = await _context.GradeTypeWeights.Select(gt => new GradeTypeWeightDTO
                {
                    SchoolId = gt.SchoolId,
                    SectionId = gt.SectionId,
                    GradeTypeCode = gt.GradeTypeCode,
                    NumberPerSection = gt.NumberPerSection,
                    PercentOfFinalGrade = gt.PercentOfFinalGrade,
                    DropLowest = gt.DropLowest,
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
        public async Task<IActionResult> Post([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights
                    .Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.GradeTypeWeight gradeTypeWeight = new EF.Models.GradeTypeWeight
                    {
                        SchoolId = _GradeTypeWeightDTO.SchoolId,
                        SectionId = _GradeTypeWeightDTO.SectionId,
                        GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode,
                        NumberPerSection = _GradeTypeWeightDTO.NumberPerSection,
                        PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade,
                        DropLowest = _GradeTypeWeightDTO.DropLowest,
                        CreatedBy = _GradeTypeWeightDTO.CreatedBy,
                        CreatedDate = _GradeTypeWeightDTO.CreatedDate,
                        ModifiedBy = _GradeTypeWeightDTO.ModifiedBy,
                        ModifiedDate = _GradeTypeWeightDTO.ModifiedDate
                    };

                    _context.GradeTypeWeights.Add(gradeTypeWeight);
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
        public async Task<IActionResult> Put([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights
                    .Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                itm.SchoolId = _GradeTypeWeightDTO.SchoolId;
                itm.SectionId = _GradeTypeWeightDTO.SectionId;
                itm.GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode;
                itm.NumberPerSection = _GradeTypeWeightDTO.NumberPerSection;
                itm.PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade;
                itm.DropLowest = _GradeTypeWeightDTO.DropLowest;
                itm.CreatedBy = _GradeTypeWeightDTO.CreatedBy;
                itm.CreatedDate = _GradeTypeWeightDTO.CreatedDate;
                itm.ModifiedBy = _GradeTypeWeightDTO.ModifiedBy;
                itm.ModifiedDate = _GradeTypeWeightDTO.ModifiedDate;

                _context.GradeTypeWeights.Update(itm);
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
        [Route("Delete/{SchoolId}/{SectionId}/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(int SchoolId, int SectionId, string GradeTypeCode)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypeWeights.Remove(itm);
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

