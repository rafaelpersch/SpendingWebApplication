using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpendingWebApplication.Models;
using SpendingWebApplication.Tools;
using System.Collections.Generic;

namespace SpendingWebApplication.Controllers
{
    public class GastoController : Controller
    {
        private List<GastoModel> gastos;
        private string caminhoJsonDatabase;

        public GastoController(IWebHostEnvironment environment)
        {
            this.caminhoJsonDatabase = Path.Combine(environment.WebRootPath, "database.json");

            try
            {
                var json = System.IO.File.ReadAllText(caminhoJsonDatabase);

                gastos = JsonConvert.DeserializeObject<List<GastoModel>>(json);
            }
            catch { }

            if (gastos == null)
            {
                gastos = new List<GastoModel>();
            }
        }

        [CustomAuthorizationFilter]
        public ActionResult Index()
        {
            ViewBag.Gastos = gastos;
            ViewBag.TotalGastos = gastos.Sum(x => x.Valor);
            return View();
        }

        [CustomAuthorizationFilter]
        public ActionResult Register()
        {
            ViewBag.Categorias = CategoriaModel.GerarListaPadrao();
            return View(new GastoModel() { Data = DateTime.Now });
        }

        [CustomAuthorizationFilter]
        public ActionResult Edit(Guid id)
        {
            var gasto = gastos.Find(x => x.Id == id);

            if (gasto == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = CategoriaModel.GerarListaPadrao();
            return View("Register", gasto);
        }


        [CustomAuthorizationFilter]
        public ActionResult Delete(Guid id)
        {
            gastos = gastos.Where(x => x.Id != id).ToList();

            System.IO.File.WriteAllText(caminhoJsonDatabase, JsonConvert.SerializeObject(gastos));

            return RedirectToAction(nameof(Index));
        }

        [CustomAuthorizationFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(GastoModel model)
        {
            try
            {
                var categorias = CategoriaModel.GerarListaPadrao();

                if (model.Categoria != null)
                {
                    model.Categoria = categorias.Find(x => x.Id == model.Categoria.Id);
                }

                if (model.Id == Guid.Empty)
                {
                    model.Id = Guid.NewGuid();
                    gastos.Add(model);
                }
                else
                {
                    for(int i = 0; i < gastos.Count; i++)
                    {
                        if (gastos[i].Id == model.Id)
                        {
                            gastos[i] = model;
                        }
                    }
                }

                System.IO.File.WriteAllText(caminhoJsonDatabase, JsonConvert.SerializeObject(gastos));
            }
            catch (Exception ex)
            {
                TempData["ApplicationMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}