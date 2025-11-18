using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dsw2025Tpi.Data.Repositories;


/*Este patrón ayuda a separar la lógica de acceso a datos del resto de la aplicación
 * , haciendo el código más limpio, mantenible y fácil de probar. 
Este código implementa un repositorio genérico usando Entity Framework Core en C#.
El repositorio (EfRepository) proporciona métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) 
sobre entidades que heredan de EntityBase. Utiliza el contexto de base de datos (Dsw2025TpiContext) 
para interactuar con la base de datos.*/
public class EfRepository : IRepository
{
    private readonly Dsw2025TpiContext _context;

    public EfRepository(Dsw2025TpiContext context)
    {
        _context = context;
    }

    // Agrega una nueva entidad a la base de datos y guarda los cambios.
    public async Task<T> Add<T>(T entity) where T : EntityBase
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    //Elimina una entidad existente y guarda los cambios.
    public async Task<T> Delete<T>(T entity) where T : EntityBase
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    //Obtiene la primera entidad que cumple una condición.
    public async Task<T?> First<T>(Expression<Func<T, bool>> predicate, params string[] include) where T : EntityBase
    {
        return await Include(_context.Set<T>(), include).FirstOrDefaultAsync(predicate);
    }

    //Obtiene todas las entidades de un tipo.
    public async Task<IEnumerable<T>?> GetAll<T>(params string[] include) where T : EntityBase
    {
        return await Include(_context.Set<T>(), include).ToListAsync();
    }

    //Obtiene todas las entidades de un tipo.
    public async Task<T?> GetById<T>(Guid id, params string[] include) where T : EntityBase
    {
        return await Include(_context.Set<T>(), include).FirstOrDefaultAsync(e => e.Id == id);
    }

    //Obtiene todas las entidades que cumplen una condición.
    public async Task<IEnumerable<T>?> GetFiltered<T>(Expression<Func<T, bool>> predicate, params string[] include) where T : EntityBase
    {
        return await Include(_context.Set<T>(), include).Where(predicate).ToListAsync();
    }

    //Actualiza una entidad existente y guarda los cambios.
    public async Task<T> Update<T>(T entity) where T : EntityBase
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    //Permite incluir propiedades de navegación (relaciones) en las consultas, útil para cargar datos relacionados.
    private static IQueryable<T> Include<T>(IQueryable<T> query, string[] includes) where T : EntityBase
    {
        var includedQuery = query;

        foreach (var include in includes)
        {
            includedQuery = includedQuery.Include(include);
        }
        return includedQuery;
    }
}

