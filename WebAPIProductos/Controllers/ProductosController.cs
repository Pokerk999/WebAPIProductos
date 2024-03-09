using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProductos.DTOs;
using WebAPIProductos.Entidades;

namespace WebAPIProductos.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ProductosController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }
       

        [HttpGet] // api/productos
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<ProductoDTO>> Get()
        {
            var productos = await context.Productos.ToListAsync();
            return mapper.Map<List<ProductoDTO>>(productos);
        }

        [HttpGet("{id:int}", Name = "obtenerProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await context.Productos
               .FirstOrDefaultAsync(productoBD => productoBD.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return mapper.Map<ProductoDTO>(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var existeProductoConElMismoNombre = await context.Productos.AnyAsync(x => x.Name == productoCreacionDTO.Name);

            if (existeProductoConElMismoNombre)
            {
                return BadRequest($"Ya existe un producto con el nombre {productoCreacionDTO.Name}");
            }

            var producto = mapper.Map<Producto>(productoCreacionDTO);

            context.Add(producto);
            await context.SaveChangesAsync();

            var productoDTO = mapper.Map<ProductoDTO>(producto);

            return CreatedAtRoute("obtenerProducto", new { id = producto.Id }, productoDTO);
        }

        [HttpPut("{id:int}")] // api/productos/1 
        public async Task<ActionResult> Put(ProductoCreacionDTO productoCreacionDTO, int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.Id.Equals(id));

            if (!existe)
            {
                return NotFound();
            }

            var producto = mapper.Map<Producto>(productoCreacionDTO);
            producto.Id = id;

            context.Update(producto);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")] // api/productos/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.Id.Equals(id));

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Producto() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
