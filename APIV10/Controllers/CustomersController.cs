using APIV10.Models.ReservApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIV10.Controllers
{
    [ApiController]
    [Route("Customers")]
    public class CustomersController : Controller
    {
        private readonly ReservAppContext _context;

        // DTO para Crear (Ya lo tenías)
        public class CustomerCreateDto
        {
            public string? Name { get; set; }
            public string? LastName { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        // DTO para Actualizar (Nuevo: No incluye Password para no romper la encriptación)
        public class CustomerUpdateDto
        {
            public string? Name { get; set; }
            public string? LastName { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public bool? Status { get; set; }
        }

        public CustomersController(ReservAppContext context)
        {
            _context = context;
        }

        // 1. OBTENER TODOS LOS CLIENTES
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Opcional: Podrías usar .Select() aquí si no quieres devolver el PasswordHash en el JSON
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // 2. OBTENER UN CLIENTE POR ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new { message = "Cliente no encontrado" });
            }

            return Ok(customer);
        }

        // 3. CREAR CLIENTE (con Stored Procedure)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var nombreParam = new SqlParameter("@Name", customerDto.Name ?? (object)DBNull.Value);
                var apellidoParam = new SqlParameter("@LastName", customerDto.LastName ?? (object)DBNull.Value);
                var telefonoParam = new SqlParameter("@PhoneNumber", customerDto.PhoneNumber ?? (object)DBNull.Value);
                var emailParam = new SqlParameter("@Email", customerDto.Email ?? (object)DBNull.Value);
                var passParam = new SqlParameter("@Password", customerDto.Password);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC ra_AddUser @Name, @LastName, @PhoneNumber, @Email, @Password, 1",
                    nombreParam, apellidoParam, telefonoParam, emailParam, passParam);

                return Ok(new { message = "Cliente creado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear cliente", error = ex.Message });
            }
        }

        // 4. ACTUALIZAR CLIENTE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDto customerDto)
        {
            // Primero buscamos si el cliente existe
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return NotFound(new { message = "Cliente no encontrado para actualizar" });
            }

            try
            {
                // Actualizamos solo los campos que vienen en el DTO
                // No tocamos PasswordHash aquí para no corromper la contraseña
                existingCustomer.Name = customerDto.Name;
                existingCustomer.LastName = customerDto.LastName;
                existingCustomer.PhoneNumber = customerDto.PhoneNumber;
                existingCustomer.Email = customerDto.Email;

                // Si envían status, lo actualizamos, si no, lo dejamos igual
                if (customerDto.Status.HasValue)
                {
                    existingCustomer.Status = customerDto.Status.Value;
                }

                // Guardamos cambios con Entity Framework normal
                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cliente actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar", error = ex.Message });
            }
        }

        // 5. ELIMINAR CLIENTE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new { message = "Cliente no encontrado" });
            }

            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Cliente eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar", error = ex.Message });
            }
        }
    }
}