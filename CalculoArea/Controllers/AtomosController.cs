using CalculoArea.Models;
using Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculoArea.Controllers
{
    public class AtomosController : Controller
    {
        // GET: Atomos
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Calcular(string codProteina)
		{
			AtomoRepository atomoRepository = new AtomoRepository();

			if (!ModelState.IsValid)
			{
				return View("Index", new XmlProteinaModel() { CodProteina = codProteina });
			}
			else 
			{
				if (!atomoRepository.CodigoProteinaExisteNaBase(codProteina))
				{
					ModelState.AddModelError("CodProteina", "Código não encontrado na base de dados do site http://www.rcsb.org");
					return View("Index", new XmlProteinaModel() { CodProteina = codProteina });
				}
			}

			var Proteina = atomoRepository.RetornarAtomos();

			return View("AreasDeContato", Proteina);
		}
		 
    }
}