using APIPracticaProgramada4.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace APIPracticaProgramada4.Controllers
{
    public class ListaController : ApiController
    {
        // GET: Lista (localhost/api/Lista) ====================================== [Funciona, Pagina inicial] ==========================================
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public IHttpActionResult GetAllLists(bool includeClassification = false) 
        {
            IList<ListaViewModel> lists = null;

            using (var ctx = new Practica4Entities()) 
            {
                lists = ctx.Lists.Include("Clasification")
                    .Select(s => new ListaViewModel()
                    {
                        ListId = s.ListId,
                        ListName = s.ListName,
                        ListDescription = s.ListDescription,
                        ClassificationId = s.ClassificationId,
                        Clasification = s.Clasification == null || includeClassification == false ? null : new ClasificationViewModel()
                        {
                            ClassificationId = s.Clasification.ClassificationId,
                            PriorityLevel = s.Clasification.PriorityLevel
                        }
                    }).ToList<ListaViewModel>();
            }

            if (lists.Count == 0)
            {
                return NotFound();
            }
            return Ok(lists);
        }

        // GET by Id: Id (localhost/api/Lista/{ListId}) ====================================== [Funciona, GET hechos en postman] ==========================================
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Lista/{ListId}")]
        public IHttpActionResult GetListById(int ListId)
        {
            ListaViewModel list = null;

            using (var ctx = new Practica4Entities())
            {
                list = ctx.Lists.Include("Clasification")
                    .Where(s => s.ListId == ListId)
                    .Select(s => new ListaViewModel()
                    {
                        ListId = s.ListId,
                        ListName = s.ListName,
                        ListDescription = s.ListDescription,
                        ClassificationId = s.ClassificationId
                    }).FirstOrDefault<ListaViewModel>();
            }
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // POST: Lista (localhost/api/Lista) ====================================== [Funciona, POSTS hechos en postman] ==========================================
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostNewList(ListaViewModel list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            using (var ctx = new Practica4Entities())
            {
                ctx.Lists.Add(new List() 
                {
                    ListId = list.ListId,
                    ListName = list.ListName,
                    ListDescription = list.ListDescription,
                    ClassificationId = list.ClassificationId
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: Lista(localhost/api/Lista) ====================================== [Funciona, PUTS hechos en postman] ==========================================
        public IHttpActionResult Put(ListaViewModel list) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            using (var ctx = new Practica4Entities())
            {
                var existeList = ctx.Lists.Where(s => s.ListId == list.ListId) 
                    .FirstOrDefault<List>();

                if (existeList != null)
                {
                    existeList.ListName = list.ListName;
                    existeList.ListDescription = list.ListDescription;
                    existeList.ClassificationId = list.ClassificationId;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        // GET: Lista(localhost/api/Lista/length) ====================================== [Funciona, GET hecho en postman] ==========================================
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Lista/length")]
        public IHttpActionResult GetLength()
        {
            using (var ctx = new Practica4Entities())
            {
                int count = ctx.Lists.Count();
                return Ok(count);
            }
        }

        // GET: Lista(localhost/api/Lista/sort) ====================================== [Funciona, GET hecho en postman] ==========================================
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Lista/sort")]
        public IHttpActionResult GetSortedLists()
        {
            using (var ctx = new Practica4Entities())
            {
                var sortedLists = ctx.Lists.Include("Clasification").OrderBy(l => l.ListName).Select(l => new ListaViewModel
                {
                    ListId = l.ListId,
                    ListName = l.ListName,
                    ListDescription = l.ListDescription,
                    ClassificationId = l.ClassificationId
                }).ToList();

                return Ok(sortedLists);
            }
        }


        // DELETE: Lista(localhost/api/Lista/delete/{ListId}) ====================================== [Funciona, DELETE hecho en postman] ==========================================
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Lista/delete/{ListId}")]
        public IHttpActionResult Delete(int ListId) 
        {
            if (ListId <= 0)
            {
                return BadRequest("Not a valid student id");
            }

            using (var ctx = new Practica4Entities())
            {
                var list = ctx.Lists
                    .Where(s => s.ListId == ListId)
                    .FirstOrDefault<List>();

                ctx.Entry(list).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}