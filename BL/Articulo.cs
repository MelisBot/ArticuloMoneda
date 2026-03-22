using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /*Capa de la logica de negocio
     *Metodo GetAll para la lista y Add para agregar
     *EF para el mapeo de la DB y manejo de exepciones
     *Creado Melissa Jimenez    2026
     */
    public class Articulo
    {
        //Add
        public static ML.Result AddEF(ML.Articulo articulo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ArticulosEntities1 context = new DL.ArticulosEntities1())
                {
                    int filasAfectadas = context.ArticuloAdd(
                        articulo.Nombre,
                        articulo.Precio
                        );

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar al usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //GetAll
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ArticulosEntities1 context = new DL.ArticulosEntities1())
                {
                    var query = context.ArticuloGetAll().ToList();
                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {
                        foreach (var fila in query)
                        {
                            ML.Articulo articuloItem = new ML.Articulo();


                            //Articulo
                            articuloItem.IdArticulo = fila.IdArticulo;
                            articuloItem.Nombre = fila.Nombre;
                            articuloItem.Precio = fila.Precio.Value; //Value para numerico

                            result.Objects.Add(articuloItem);
                        }
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

    }
}
