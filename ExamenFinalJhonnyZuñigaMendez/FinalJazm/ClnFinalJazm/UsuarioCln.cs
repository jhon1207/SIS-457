using CadFinalJazm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFinalJazm
{
    public class UsuarioCln
    {
        public static int insertar(Usuario usuario)
        {
            using (var contexto = new FinaJazmEntities())
            {
                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();
                return usuario.id;
            }
        }

        public static int actualizar(Usuario usuario)
        {
            using (var contexto = new FinaJazmEntities())
            {
                var existente = contexto.Usuario.Find(usuario.id);
                existente.usuario = usuario.usuario.Trim();
                existente.clave = usuario.clave.Trim();
                return contexto.SaveChanges();
            }
        }

        public static int cambiarClave(int id, string clave)
        {
            using (var contexto = new FinaJazmEntities())
            {
                var existente = contexto.Usuario.Find(id);
                existente.clave = clave;
                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var contexto = new FinaJazmEntities())
            {
                var existente = contexto.Usuario.Find(id);
                existente.registroActivo = false;
                return contexto.SaveChanges();
            }
        }

        public static Usuario get(int id)
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.Usuario.Find(id);
            }
        }

        public static Usuario validar(string usuario, string clave)
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.Usuario
                    .Where(x => x.usuario == usuario && x.clave == clave && x.registroActivo == true)
                    .FirstOrDefault();
            }
        }

        public static List<Usuario> listar()
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.Usuario.Where(x => x.registroActivo.Value).ToList();
            }
        }

        public static List<paUsuarioListar_Result> listarPa(string parametro)
        {
            using (var contexto = new FinaJazmEntities())
            {
                return contexto.paUsuarioListar(parametro).ToList();
            }
        }
    }
}
