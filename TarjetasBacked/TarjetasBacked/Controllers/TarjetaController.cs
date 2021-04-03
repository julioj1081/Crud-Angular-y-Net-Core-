using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarjetasBacked.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TarjetasBacked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        //variable de solo lectura
        private readonly AplicationDBContext _context;
        //despues de hacer la migracion
        public TarjetaController(AplicationDBContext context)
        {
            _context = context;
        }


        // GET: api/<TarjetaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaTarjetas = await _context.TarjetaCredito.ToListAsync();
                return Ok(listaTarjetas);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<TarjetaController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Models.TarjetaCredito tarjeta)
        {
            try
            {
                _context.Add(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(tarjeta);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<TarjetaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Models.TarjetaCredito tarjeta)
        {
            try
            {
                if(id != tarjeta.Id)
                {
                    return NotFound();
                }
                else
                {
                    _context.Update(tarjeta);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "La tarjeta a sido actualizada"});
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<TarjetaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarjeta = await _context.TarjetaCredito.FindAsync(id);
                if(tarjeta.Id == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.TarjetaCredito.Remove(tarjeta);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Tarjeta eliminada con exito" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
