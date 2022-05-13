using jqueryajaxCrudOperation.Models;
using jqueryAjaxCrudOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jqueryAjaxCrudOperation.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDBContext _db;
        public StudentController(StudentDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Student.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentModel model)
        {
            _db.Student.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AddorEdit(int id)
        {
            if(id == 0)
            {
                return View();
            }
            else
            {
                var data = await _db.Student.FindAsync(id);
                return View(data);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> AddorEdit(StudentModel model,int id)
        {
            try
            {
                    if(id == 0)
                    {
                        _db.Student.Add(model);
                        await _db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
               
                    else
                    {
                        var data = _db.Student.Where(x => x.StudentId == model.StudentId).FirstOrDefault();
                        if (data != null)
                        {
                            data.Name = model.Name;
                            data.Address = model.Address;
                            data.Email = model.Email;
                            data.Password = model.Password;
                            data.MobileNo = model.MobileNo;
                            await _db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.Student.ToList()) });
                    }
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _db.Student.FindAsync(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel model)
        {
            var data = _db.Student.Where(x => x.StudentId == model.StudentId).FirstOrDefault();
            if(data != null)
            {
                data.Name = model.Name;
                data.Address = model.Address;
                data.Email = model.Email;
                data.Password = model.Password;
                data.MobileNo = model.MobileNo;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public static string RenderRazorViewToString(Controller controller, string ViewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, ViewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            if(id != null)
            {
                var data = _db.Student.Where(x => x.StudentId == id).FirstOrDefault();
                _db.Student.Remove(data);
                await _db.SaveChangesAsync();
               
            }
            return RedirectToAction("Index");
            //return Json(new { html = Helper.RenderRazorViewToString(this, "Index", _db.Student.ToList()) });
        }
        public ActionResult Deletes(string studentIDs)
        {
            var student = studentIDs.Split(',');
            foreach (string studentID in student)
            {
                StudentModel obj = _db.Student.Find(Convert.ToInt32(studentID));
                _db.Student.Remove(obj);
            }
            _db.SaveChanges();
            //return Json("All the customers deleted successfully!");
            return RedirectToAction("Index");
            //int[] getid = null;

            //if(studentIDs != null)
            //{
            //    getid = new int[studentIDs.Length];
            //    int j = 0;
            //    foreach(string i in studentIDs)
            //    {
            //        int.TryParse(i, out getid[j++]);
            //    }
            //    List<StudentModel> getstudids = new List<StudentModel>();
            //    getstudids = _db.Student.Where(x => getid.Contains(x.StudentId)).ToList();
            //    foreach(var s in getstudids)
            //    {
            //        _db.Student.Remove(s);
            //    }
            //    _db.SaveChanges();
            //}

        }
        //[HttpPost]
        //public ActionResult Index(FormCollection formCollection)
        //{
        //    string[] ids = formCollection["ID"].Split(new char[] { ',' });
        //    foreach (string id in ids)
        //    {
        //        var employee = this.db.Employees.Find(int.Parse(id));
        //        this.db.Employees.Remove(employee);
        //        this.db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

    }
}
