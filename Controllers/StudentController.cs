using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        StudentContext studentContext = new StudentContext(); 
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            students = studentContext.GetAllStudents().ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Student student)
        {
            if (ModelState.IsValid)
            {
                studentContext.AddStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = studentContext.GetStudentById(id);

            if(student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] Student student)
        {
            if (id == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                studentContext.UpdateStudent(student);
                return RedirectToAction("Index");
            }

            return View(studentContext);
        }

      
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var student = studentContext.GetStudentById(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var student = studentContext.GetStudentById(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStudent(int? id)
        {
            studentContext.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
