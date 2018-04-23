using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmpleadoController : ApiController
    {
        PruebaBDContext conexion = new PruebaBDContext();
    //READ
        public List<Empleado> Get()
        {
            var emp = (from c in conexion.Empleados select c).ToList();
            return emp;
        }
        [HttpGet]
        [Route("api/Empleado/GetEmpleados")]
        public IEnumerable<Empleado> GetEmpleados()
        {
            return conexion.Empleados.AsEnumerable();
        }

        [HttpGet]
        [Route("api/Empleado/GetEmpleadoPorId")]
        public IHttpActionResult GetEmpleadoPorId(Guid id)
        {
                var empleadoExists = (from e in conexion.Empleados where e.Id==id select e).FirstOrDefault();

                if (empleadoExists!=null)
                {
                    return Ok(empleadoExists);
                }
                else
                {
                    return NotFound();
                }
        }
    //CREATE
        [HttpPost]
        [Route("api/Empleado/AddEmpleado")]
        public IHttpActionResult AddEmpleado([FromBody]Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                conexion.Empleados.Add(empleado);
                conexion.SaveChanges();

                return Ok(empleado);
            }
            else
            {
                return BadRequest();
            }
        }
    //UPDATE
        [HttpPut]
        [Route("api/Empleado/UpdateEmpleado")]
        public IHttpActionResult UpdateEmpleado(Guid id, [FromBody]Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                var empleadoExists = conexion.Empleados.Count(c => c.Id == id) > 0;

                if (empleadoExists)
                {
                    conexion.Entry(empleado).State = EntityState.Modified;
                    conexion.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    //DELETE
        [HttpDelete]
        [Route("api/Empleado/DeleteEmpleado")]
        public IHttpActionResult DeleteEmpleado(Guid id)
        {
            var empleado = conexion.Empleados.Find(id);

            if (empleado != null)
            {
                conexion.Empleados.Remove(empleado);
                conexion.SaveChanges();

                return Ok(empleado);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
