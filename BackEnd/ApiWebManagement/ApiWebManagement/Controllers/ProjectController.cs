﻿using APIwebmoi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace API.Controllers
{
    public class ProjectController : Controller
    {
        private readonly WebNangCaoQlda2Context _context;
        public ProjectController(WebNangCaoQlda2Context context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetTenProject")]
        public async Task<ActionResult<IEnumerable<Project>>> GetTenProject(string sdt)
        {
            var k = await _context.Projects.Where(i => i.OwnerIds == sdt).ToListAsync();

            return k;
        }

        [HttpGet]
        [Route("GetColumn")]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumn(int id_project)
        {
            var y = await _context.Columns.Where(i => i.IdProject == id_project).ToListAsync();

            return y;
        }

        [HttpGet]
        [Route("GetTask")]
        public async Task<ActionResult<IEnumerable<APIwebmoi.Models.Task>>> GetTask(string sdt)
        {
            // Lấy danh sách id_task từ UserTask mà id_user khớp với sdt
            var taskIds = await _context.UserTasks
                                        .Where(i => i.IdUser == sdt)
                                        .Select(i => i.IdTask)
                                        .ToListAsync();

            // Lấy danh sách Task với id_task có trong danh sách taskIds và id_column khớp với tham số id_column
            var tasks = await _context.Tasks
                                      .Where(i => taskIds.Contains(i.id_task))
                                      .ToListAsync();

            return tasks;
        }
        [HttpPost("createPro")]
        
        public async Task<ActionResult> createPro(APIwebmoi.Models.Project pr)
        {
            // Lấy danh sách id_task từ UserTask mà id_user khớp với sdt
           _context.Projects.Add(pr);
            await _context.SaveChangesAsync();


            return Ok (new {mesage="ok"});
        }
         [HttpPost("createColumn")]
        
        public async Task<ActionResult> createColumn(APIwebmoi.Models.Column column)
        {
            // Lấy danh sách id_task từ UserTask mà id_user khớp với sdt
           _context.Columns.Add(column);
            await _context.SaveChangesAsync();


            return Ok (new {mesage="ok"});
        }
    }
}
