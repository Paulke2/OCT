using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("Get/{SchoolID}/{SectionNo}")]
        public async Task<IActionResult> Get(int SchoolID, int SectionNo)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context
                    .Sections
                    .Where(x => x.SectionNo == SectionNo)
                    .Where(x => x.SchoolId == SchoolID)
                    .Select(sp => new SectionDTO
                    {

                        SectionId = sp.SectionId,
                        CourseNo = sp.CourseNo,
                        SectionNo = sp.SectionNo,
                        StartDateTime = sp.StartDateTime,
                        Location = sp.Location,
                        InstructorId = sp.InstructorId,
                        Capacity = sp.Capacity,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId,
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

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                    SectionId = sp.SectionId,
                    CourseNo = sp.CourseNo,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime,
                    Location = sp.Location,
                    InstructorId = sp.InstructorId,
                    Capacity = sp.Capacity,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
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
        public async Task<IActionResult> Post([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionNo == _SectionDTO.SectionNo).FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Section section = new EF.Models.Section
                    {
                        SectionId = _SectionDTO.SectionId,
                        CourseNo = _SectionDTO.CourseNo,
                        SectionNo = _SectionDTO.SectionNo,
                        StartDateTime = _SectionDTO.StartDateTime,
                        Location = _SectionDTO.Location,
                        InstructorId = _SectionDTO.InstructorId,
                        Capacity = _SectionDTO.Capacity,
                        CreatedBy = _SectionDTO.CreatedBy,
                        CreatedDate = _SectionDTO.CreatedDate,
                        ModifiedBy = _SectionDTO.ModifiedBy,
                        ModifiedDate = _SectionDTO.ModifiedDate,
                        SchoolId = _SectionDTO.SchoolId,
                    };
                    _context.Sections.Add(section);
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
        public async Task<IActionResult> Put([FromBody] SectionDTO _SectionDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionNo == _SectionDTO.SectionNo).FirstOrDefaultAsync();


                itm.SectionId = _SectionDTO.SectionId;
                        itm.CourseNo = _SectionDTO.CourseNo;
                       itm.SectionNo = _SectionDTO.SectionNo;
                        itm.StartDateTime = _SectionDTO.StartDateTime;
                        itm.Location = _SectionDTO.Location;
                        itm.InstructorId = _SectionDTO.InstructorId;
                        itm.Capacity = _SectionDTO.Capacity;
                        itm.CreatedBy = _SectionDTO.CreatedBy;
                        itm.CreatedDate = _SectionDTO.CreatedDate;
                        itm.ModifiedBy = _SectionDTO.ModifiedBy;
                        itm.ModifiedDate = _SectionDTO.ModifiedDate;
                        itm.SchoolId = _SectionDTO.SchoolId;

                _context.Sections.Update(itm);
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
        [Route("Delete/{SectionNo}")]
        public async Task<IActionResult> Delete(int SectionNo)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionNo == SectionNo).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
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
    }
}