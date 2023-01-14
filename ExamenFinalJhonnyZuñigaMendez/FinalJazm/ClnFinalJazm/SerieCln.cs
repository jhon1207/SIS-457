using CadFinalJazm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFinalJazm
{
    public class SerieCln
    {
        public static int insertar(Serie serie)
        {
            using (var contexto = new FinaJazmEntities())
            {
                contexto.Serie.Add(serie);
                contexto.SaveChanges();
                return serie.id;
            }
        }
        public static int actualizar(Serie serie)
        {
            using (var contexto = new FinaJazmEntities())
            {
                var existe = contexto.Serie.Find(serie.id);
                existe.titulo = serie.titulo.Trim();
                existe.sinopsis = serie.sinopsis.Trim();
                existe.director = serie.director.Trim();
                existe.duracion = serie.duracion;
                return contexto.SaveChanges();
            }
        }
        public static int eliminar(int id)
        {
            using (var contexto = new FinaJazmEntities())
            {
                var existe = contexto.Serie.Find(id);
                existe.registroActivo = false;
                return contexto.SaveChanges();
            }
        }
        public static Serie get(int id)
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.Serie.Find(id);
            }
        }
        public static List<Serie> listar()
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.Serie.Where(x => x.registroActivo.Value).ToList();
            }
        }

        public static List<paSerieListar_Result> listarPa(string parametro)
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.paSerieListar(parametro).ToList();
            }
        }
    }
}
