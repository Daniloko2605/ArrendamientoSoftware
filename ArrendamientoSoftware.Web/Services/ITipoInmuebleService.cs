using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using ArrendamientoSoftware.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web.Services
{
    public interface ITipoInmuebleService
    {
        public Task<Response<TipoInmueble>> CreateAsync(TipoInmuebleDTO dto);

        public Task<Response<TipoInmueble>> EditAsync(TipoInmuebleDTO dto);

        public Task<Response<PaginationResponse<TipoInmueble>>> GetListAsync(PaginationRequest request);

        public Task<Response<TipoInmueble>> GetOneAsync(int id);
    }

    public class TipoInmuebleService : ITipoInmuebleService
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TipoInmuebleService(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        public async Task<Response<TipoInmueble>> CreateAsync(TipoInmuebleDTO dto)
        {
            try
            {
                TipoInmueble tipoInmueble = _converterHelper.ToTipoInmueble(dto);

                await _context.TipoInmueble.AddAsync(tipoInmueble);
                await _context.SaveChangesAsync();

                return ResponseHelper<TipoInmueble>.MakeResponseSuccess(tipoInmueble, "TipoInmueble creado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<TipoInmueble>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<TipoInmueble>> EditAsync(TipoInmuebleDTO dto)
        {
            try
            {
                TipoInmueble? tipoInmueble = await _context.TipoInmueble.FirstOrDefaultAsync(b => b.Id == dto.Id);

                if (tipoInmueble is null)
                {
                    return ResponseHelper<TipoInmueble>.MakeResponseFail($"No existe el inmueble con id '{dto.Id}'");
                }

                //blog = _converterHelper.ToTipoInmueble(dto);

                tipoInmueble.Local = dto.Local;
                tipoInmueble.Oficina = dto.Oficina;
                tipoInmueble.Bodega = dto.Bodega;
                tipoInmueble.Casa = dto.Casa;
                tipoInmueble.Apartamento = dto.Apartamento;
                tipoInmueble.Finca = dto.Finca;
                tipoInmueble.PropiedadId = dto.PropiedadId;

                _context.TipoInmueble.Update(tipoInmueble);
                await _context.SaveChangesAsync();

                return ResponseHelper<TipoInmueble>.MakeResponseSuccess(tipoInmueble, "Inmueble actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<TipoInmueble>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<TipoInmueble>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<TipoInmueble> query = _context.TipoInmueble.AsQueryable().Include(b => b.Propiedad);

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Local.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Oficina.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Bodega.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Casa.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Apartamento.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Finca.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<TipoInmueble> list = await PagedList<TipoInmueble>.ToPagedListAsync(query, request);

                PaginationResponse<TipoInmueble> result = new PaginationResponse<TipoInmueble>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<TipoInmueble>>.MakeResponseSuccess(result, "Inmueble obtenido con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<TipoInmueble>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<TipoInmueble>> GetOneAsync(int id)
        {
            try
            {
                TipoInmueble? tipoInmueble = await _context.TipoInmueble.FirstOrDefaultAsync(s => s.Id == id);

                if (tipoInmueble is null)
                {
                    return ResponseHelper<TipoInmueble>.MakeResponseFail("El inmueble con el id indicado no existe");
                }

                return ResponseHelper<TipoInmueble>.MakeResponseSuccess(tipoInmueble);
            }
            catch (Exception ex)
            {
                return ResponseHelper<TipoInmueble>.MakeResponseFail(ex);
            }
        }
    }
}