using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandByClientes.Contexto;
using StandByClientes.Entidades;
using StandByClientes.Models;

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
        public async Task<IActionResult> Index(string RazaoSocial, string cnpj, bool ativo)
        {           

            if (RazaoSocial == null && cnpj == null && ativo ==false)
            {
                return View(await _context.Ciente.ToListAsync());
            }

            if (RazaoSocial == null || RazaoSocial == "")
            {
                var cliente = await _context.Ciente.FindAsync(RazaoSocial);

                string cnpj1; 
                
                var teste = _context.Ciente.Where(x => x.Cnpj == cnpj).ToList();

                foreach (var item in teste)
                {
                    cnpj1 = item.Cnpj;
                }


                return View(_context.Ciente.Where(x => x.Cnpj == cnpj).ToList());
            }
            return View(_context.Ciente.ToListAsync());
        }

        //public class PesquisarViewModel
        //{
        //    public string RazaoSocial { get; set; }
        //    public string Cnpj { get; set; }
        //}

        public async Task<ViewResult> Pesquisar(string RazaoSocial, string cnpj, bool ativo)
        {
            if (RazaoSocial == null || RazaoSocial =="")
            {  
                return View(_context.Ciente.Where(x => x.Cnpj == cnpj).FirstOrDefault());
            }

            return View(await _context.Ciente.ToListAsync());
        }


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Ciente
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
        public async Task<IActionResult> Create([Bind("Id,Razao_Social,Cnpj,Data_Fundacao,Capital,Quarentena,Status_Cliente,Classificacao")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Ciente.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Razao_Social,Cnpj,Data_Fundacao,Capital,Quarentena,Status_Cliente,Classificacao")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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

            var cliente = await _context.Ciente
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
            var cliente = await _context.Ciente.FindAsync(id);
            _context.Ciente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Ciente.Any(e => e.Id == id);
        }
    }
}
