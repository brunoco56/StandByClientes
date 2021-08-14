using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandByClientes.Entidades;
using StandByClientes.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StandByClientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Contexto.Contexto _context;

        public ClientesController(Contexto.Contexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string razaoSocial, int Ativo, string cnpj)
        {
            var q = _context.Cliente.AsQueryable();

            if (string.IsNullOrEmpty(razaoSocial) && string.IsNullOrEmpty(cnpj))
            {
                if (Ativo == 1)
                {
                    q = q.Where(x => x.Status_Cliente == true);
                    q = q.OrderBy(c => c.Status_Cliente);
                    return View(q.ToList());
                }

                if (Ativo == -1)
                {
                    q = q.Where(x => x.Status_Cliente == false);
                    q = q.OrderBy(c => c.Status_Cliente);
                    return View(q.ToList());
                }

                return View(await _context.Cliente.ToListAsync());
            }

            else
            {
                if (!string.IsNullOrEmpty(razaoSocial) && string.IsNullOrEmpty(cnpj))
                {
                    q = q.Where(x => x.Razao_Social.Contains(razaoSocial));
                    q = q.OrderBy(c => c.Razao_Social);
                    return View(q.ToList());
                }
                else if (!string.IsNullOrEmpty(cnpj) && string.IsNullOrEmpty(razaoSocial))
                {
                    q = q.Where(x => x.Cnpj.Contains(cnpj));
                    q = q.OrderBy(c => c.Cnpj);
                    
                    return View(q.ToList());
                }
                else
                {
                    q = q.Where(x => x.Cnpj.Contains(cnpj));
                    q.Include(t => t.Razao_Social).Where(t => t.Razao_Social.Contains(razaoSocial));
                    q = q.OrderBy(c=>c.Razao_Social);
                    if (q==null)
                    {
                        return View(await _context.Cliente.ToListAsync());
                    }
                    else
                    return View(q.ToList());
                }
            }
            
        }


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Razao_Social,Cnpj,Data_Fundacao,Capital,Quarentena,Status_Cliente,Classificacao")] ClienteViewModel cliente)
        {
            var q = _context.Cliente.AsQueryable();

            var confereCnpj = q.Where(x => x.Cnpj.Equals(cliente.Cnpj));
            var confereRazaoSocial = q.Where(g => g.Razao_Social.Equals(cliente.Razao_Social));

            if (confereCnpj.Count() == 0 && confereRazaoSocial.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    Cliente cliente1 = new Cliente
                    {
                        Id = cliente.Id,
                        Razao_Social = cliente.Razao_Social,
                        Cnpj = cliente.Cnpj,
                        Data_Fundacao = cliente.Data_Fundacao,
                        Capital = cliente.Capital,
                        Quarentena = cliente.Quarentena,
                        Status_Cliente = cliente.Status_Cliente,
                        Classificacao = cliente.Classificacao

                    };

                    _context.Add(cliente1);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
                return PartialView("_dadosJaExistentes");

            return View(cliente);
        }


        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }






        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Razao_Social,Cnpj,Data_Fundacao,Capital,Quarentena,Status_Cliente,Classificacao")] ClienteViewModel clienteVielModel)
        {
            Cliente cliente = new Cliente();

            if (id != clienteVielModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cliente.Id = clienteVielModel.Id;
                    cliente.Razao_Social = clienteVielModel.Razao_Social;
                    cliente.Cnpj = clienteVielModel.Cnpj;
                    cliente.Data_Fundacao = clienteVielModel.Data_Fundacao;
                    cliente.Capital = clienteVielModel.Capital;
                    cliente.Quarentena = clienteVielModel.Quarentena;
                    cliente.Status_Cliente = clienteVielModel.Status_Cliente;
                    cliente.Classificacao = clienteVielModel.Classificacao;

                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(clienteVielModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }


        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
