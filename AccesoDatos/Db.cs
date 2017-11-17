using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace AccesoDatos
{
    public class Db
    {
        // 
        static DbProviderFactory fabricaProveedorBaseDatos = DbProviderFactories.GetFactory("System.Data.SqlClient");
        //static DbProviderFactory fabricaProveedorBaseDatos = DbProviderFactories.GetFactory("System.Data.SQLite");

        private string cadenaConexion;
        private int tiempoDeEspera;

        public Db(string cadenaConexion, int tiempoDeEspera)
        {
            this.cadenaConexion = cadenaConexion;
            this.tiempoDeEspera = tiempoDeEspera;
        }
            
        public IEnumerable<T> Leer<T>(string sql, Func<IDataReader, T> funcionConstruccionObjeto, params object[] parametros)
        {
            using (var conexion = CrearConexion())
            {
                using (var comando = CrearComando(CommandType.Text, sql, conexion, parametros))
                {
                    comando.CommandTimeout = tiempoDeEspera;
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            yield return funcionConstruccionObjeto(lector);
                        }
                    }
                }
            }
        }

        public int Insertar(string sql, params object[] parametros)
        {
            using (var conexion = CrearConexion())
            {
                using (var comando = CrearComando(CommandType.Text, sql , conexion, parametros))
                {
                    return int.Parse(comando.ExecuteScalar().ToString());
                }
            }
        }

        public int Actualizar(string sql, params object[] parametros)
        {
            using (var conexion = CrearConexion())
            {
                using (var comando = CrearComando(CommandType.Text, sql, conexion, parametros))
                {
                    return comando.ExecuteNonQuery();
                }
            }
        }

        public int Eliminar(string sql, params object[] parametros)
        {
            return Actualizar(sql, parametros);
        }

        DbConnection CrearConexion()
        {
            var conexion = fabricaProveedorBaseDatos.CreateConnection();
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            return conexion;
        }

        DbCommand CrearComando(CommandType tipoComando, string sql, DbConnection conexion, params object[] parametros)
        {
            var command = fabricaProveedorBaseDatos.CreateCommand();
            command.Connection = conexion;
            command.CommandText = sql;
            command.CommandType = tipoComando;
            command.AddParameters(parametros);
            return command;
        }

        DbDataAdapter CrearAdaptador(DbCommand comando)
        {
            var adaptador = fabricaProveedorBaseDatos.CreateDataAdapter();
            adaptador.SelectCommand = comando;
            return adaptador;
        }
    }

    public static class ExtensionesBaseDatos
    {
        public static void AddParameters(this DbCommand comando, object[] parametros)
        {
            if (parametros != null && parametros.Length > 0)
            {
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    string nombre = parametros[i].ToString();

                    if (parametros[i + 1] is string && (string)parametros[i + 1] == "")
                        parametros[i + 1] = null;

                    object valor = parametros[i + 1] ?? DBNull.Value;

                    var parametroDb = comando.CreateParameter();
                    parametroDb.ParameterName = nombre;
                    parametroDb.Value = valor;

                    comando.Parameters.Add(parametroDb);
                }
            }
        }

        public static int ComoId(this object item, int idPorDefecto = -1)
        {
            if (item == null)
                return idPorDefecto;

            int resultado;
            if (!int.TryParse(item.ToString(), out resultado))
                return idPorDefecto;

            return resultado;
        }

        public static int ComoInt(this object item, int intPorDefecto = default(int))
        {
            if (item == null)
                return intPorDefecto;

            int resultado;
            if (!int.TryParse(item.ToString(), out resultado))
                return intPorDefecto;

            return resultado;
        }

        public static string ComoString(this object item, string stringPorDefecto = default(string))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return stringPorDefecto;

            return item.ToString().Trim();
        }

        public static DateTime ComoDateTime(this object item, DateTime dateTimePorDefector = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return dateTimePorDefector;

            DateTime resultado;
            if (!DateTime.TryParse(item.ToString(), out resultado))
                return dateTimePorDefector;

            return resultado;
        }
    }
}
