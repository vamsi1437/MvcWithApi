using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCAss.Models;
using Newtonsoft.Json;

namespace MVCAss.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<StudentModel> studata = null;
            using(WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44366/api/";
                var json = webClient.DownloadString("Students");
                var list = JsonConvert.DeserializeObject<List<StudentModel>>(json);
                studata = list.ToList();
                return View(studata);
            }
           
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            StudentModel studata;
            using (WebClient webClient=new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44366/api/";
                var json = webClient.DownloadString("Students/" +id);
                studata = JsonConvert.DeserializeObject<StudentModel>(json);
            }


            return View(studata);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44366/api/";
                    var url = "Students/POST";
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(url, data);
                    JsonConvert.DeserializeObject<StudentModel>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentModel studata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44366/api/";
                var json = webClient.DownloadString("Students/" + id);
                studata = JsonConvert.DeserializeObject<StudentModel>(json);
            }


            return View(studata);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentModel model)
        {
            try
            {
               using(WebClient webClient= new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44366/api/Students/"+id;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(webClient.BaseAddress, "PUT", data);
                    StudentModel modeldata = JsonConvert.DeserializeObject<StudentModel>(response);
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            StudentModel studata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44366/api/";
                var json = webClient.DownloadString("Students/" + id);
                studata = JsonConvert.DeserializeObject<StudentModel>(json);
            }
            return View(studata);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, StudentModel model)
        {
            try
            {
                using(WebClient webClient=new WebClient())
                {
                    NameValueCollection nv = new NameValueCollection();
                    string url = "https://localhost:44366/api/Students/" + id;
                    var response = Encoding.ASCII.GetString(webClient.UploadValues(url, "DELETE", nv));
                }

                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}
