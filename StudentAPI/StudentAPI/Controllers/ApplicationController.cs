using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.BLL.Courses;
using StudentAPI.BLL.Register;
using StudentAPI.BLL.Semester;
using StudentAPI.DAL.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IApplicationDetailsService _applicatioDetailsService;
        private readonly ICourseService _courseService;

        public ApplicationController(IApplicationService applicationService, IApplicationDetailsService applicatioDetailsService
            , ICourseService courseService)
        {
            _applicationService = applicationService;
            _applicatioDetailsService = applicatioDetailsService;
            _courseService = courseService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationMaster>>> GetStudentRegisters()
        {
            return await _applicationService.GetAll().Include(x => x.Student).ToListAsync();
        }

        // GET: api/Semester/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationMaster>> GetStudentRegister(long id)
        {
            //get fooditem from order details
            var applicationDetails = await (from register in _applicationService.GetAll()
                                      join detail in _applicatioDetailsService.GetAll()
                                      on register.Id equals detail.ApplicationMasterId
                                      join course in _courseService.GetAll()
                                      on detail.CourseDetailsId equals course.Id
                                      where register.Id == id

                                      select new
                                      {
                                          register.Id,
                                          detail.ApplicationMasterId,
                                          detail.CourseDetailsId,
                                          detail.Frequency,
                                          detail.CoursePrice,
                                          course.CourseName
                                      }).ToListAsync();

            // get order master
            var applicationMaster = await (from a in _applicationService.GetAll()
                                     where a.Id == id
                                     select new
                                     {
                                         a.Id,
                                         a.ApplicationNumber,
                                         a.StudentId,
                                         a.TeachingMethod,
                                         a.GrandTotal,
                                         deletedOrderItemIds = "",
                                         applicationDetails = applicationDetails
                                     }).FirstOrDefaultAsync();

            if (applicationMaster == null)
            {
                return NotFound();
            }

            return Ok(applicationMaster);
        }

        // PUT: api/Semester/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutStudentRegister(long id, ApplicationMaster register)
        {
            try
            {
                if (id != register.Id)
                {
                    return BadRequest();
                }
                _applicationService.Update(register);

                //existing semesters & newly added semesters
                foreach (ApplicationDetail item in register.ApplicationDetails)
                {
                    if (item.Id == 0)
                        _applicatioDetailsService.Insert(item);
                    else
                        _applicatioDetailsService.Update(item);
                }

                //deleted semesters 
                foreach (var i in register.DeletedMasterDetailsIds.Split(',').Where(x => x != ""))
                {
                    ApplicationDetail recordToRemove = _applicatioDetailsService.Get(Convert.ToInt32(i));
                    _applicatioDetailsService.Delete(recordToRemove);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderMasterExists(id))
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

        // POST: api/Semester
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ApplicationMaster> PostStudentRegister(ApplicationMaster register)
        {
            _applicationService.Insert(register);
            return CreatedAtAction("GetStudentRegisters", new { id = register.Id }, register);
        }

        // DELETE: api/Semester/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderRegister(int id)
        {
            //removed application details first 
            var register = _applicationService.DeleteRecord(id);

            return NoContent();
        }

        private bool OrderMasterExists(long id)
        {
            return _applicationService.GetAll().Any(e => e.Id == id);
        }
    }
}
