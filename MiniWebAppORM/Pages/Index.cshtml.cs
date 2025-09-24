using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniWebAppORM.Data;
using MiniWebAppORM.Models;

namespace MiniWebAppORM.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Usuario> Usuarios { get; set; } = new();

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Usuarios = await _context.Usuarios
                                     .AsNoTracking()
                                     .OrderBy(u => u.Id)
                                     .ToListAsync();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string nombre, string email)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError(string.Empty, "Nombre y Email son requeridos.");
                await OnGetAsync(); // recargar lista
                return Page();
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre.Trim(),
                Email  = email.Trim()
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            // PRG pattern
            return RedirectToPage();
        }
    }
}