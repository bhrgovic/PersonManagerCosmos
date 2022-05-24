using CosmosBrunoHrgovic.Dao;
using CosmosBrunoHrgovic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CosmosBrunoHrgovic.Controllers
{
    public class PeopleController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;

        // GET: People
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM Person"));
        }

        // GET: People/Details/5
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id) => await ShowPerson(id);

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Telephone,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddPersonAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id) => await ShowPerson(id);

        // POST: People/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id) => await ShowPerson(id);

        // POST: People/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id,FirstName,LastName,Telephone,Email")] Person person)
        {
            await service.DeletePersonAsync(person);
            return RedirectToAction("Index");
        }

        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var person = await service.GetPersonAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
    }
}
