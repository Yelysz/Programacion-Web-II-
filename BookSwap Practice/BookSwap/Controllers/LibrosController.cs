using Microsoft.AspNetCore.Mvc;
using BookSwap.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookSwap.Controllers
{
    public class LibrosController : Controller
    {
        // Simulación: lista estática de datos (reemplazar luego con EF Core)
        private static List<Libro> _libros = new List<Libro>
        {
            new Libro { Id = 1, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", ISBN = "978-0307474728" },
            new Libro { Id = 2, Titulo = "1984", Autor = "George Orwell", ISBN = "978-0451524935" },
            new Libro { Id = 3, Titulo = "El Principito", Autor = "Antoine de Saint-Exupéry", ISBN = "978-0156013925", Disponible = false }
        };

        // ACCIÓN 1: Catálogo (Index - GET /Libros)
        public IActionResult Index()
        {
            return View(_libros);
        }

        // ACCIÓN 2: Búsqueda (Buscar - GET /Libros/Buscar?q=termino)
        public IActionResult Buscar(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                ViewBag.TerminoBusqueda = "";
                return View("Index", _libros);
            }

            var resultados = _libros.Where(l =>
                l.Titulo.Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                l.Autor.Contains(q, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();

            ViewBag.TerminoBusqueda = q;
            return View("Index", resultados);
        }

        // ACCIÓN 3: Compartir (GET - Muestra el formulario)
        [HttpGet]
        public IActionResult Compartir()
        {
            return View();
        }

        // ACCIÓN 4: Compartir (POST - Procesa el envío del formulario)
        [HttpPost]
        public IActionResult Compartir(Libro nuevoLibro)
        {
            if (ModelState.IsValid)
            {
                nuevoLibro.Id = _libros.Max(l => l.Id) + 1;
                _libros.Add(nuevoLibro);
                return RedirectToAction("Index");
            }
            return View(nuevoLibro);
        }
    }
}