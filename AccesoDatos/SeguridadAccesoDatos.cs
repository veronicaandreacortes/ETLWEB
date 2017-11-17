using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;

namespace AccesoDatos
{
    public class SeguridadAccesoDatos : ISeguridadAccesoDatos
    {
        protected static Db db = null;

        public SeguridadAccesoDatos(string cadenaConexion, int tiempoDeEspera)
        {
            if (db == null)
            {
                db = new Db(cadenaConexion, tiempoDeEspera);
            }
        }

        #region usuario

        public LoginRespuesta Login(LoginPeticion login)
        {
            string sql =
                    @"SELECT 
                        [idusuario],
                        [correo],
                        [clave],
                        [nombrecompleto],
                        [celular]
                  FROM [usuario]
                  WHERE [correo] = @correo AND [clave] = @clave ";

            object[] parametros = { "@correo", login.Correo, "@clave", login.Clave, };

            Usuario usuario = db.Leer(sql, ConstruirObjetoUsuario, parametros).FirstOrDefault();

            if (usuario != null)
            {
                return new LoginRespuesta() { Mensaje = "EXITO" };
            }
            else
            {
                return new LoginRespuesta() { Mensaje = "FALLIDO" };
            }
        }

        public IList<Usuario> ObtenerListaUsuarios()
        {
            string sql =
                @"SELECT [idusuario],
                           [correo],
                           [clave],
                           [nombrecompleto],
                           [celular]
                 FROM [usuario]
                ORDER BY [idusuario] ASC";

            return db.Leer(sql, ConstruirObjetoUsuario).ToList();
        }

        public IList<Usuario> BuscarUsuarios(Usuario usuario)
        {
            string sqlSelect =
                @"SELECT [idusuario],
                           [correo],
                           [clave],
                           [nombrecompleto],
                           [celular]
                 FROM [usuario] WHERE 1=1 ";

            string sqlWhere = string.Empty;

            List<object> parametros = new List<object>();

            if (!string.IsNullOrWhiteSpace(usuario.nombrecompleto))
            {
                sqlWhere += " AND nombrecompleto LIKE @nombrecompleto ";
                parametros.Add("@nombrecompleto");
                parametros.Add("%" + usuario.nombrecompleto + "%");
            }

            if (!string.IsNullOrWhiteSpace(usuario.celular))
            {
                sqlWhere += " AND celular LIKE @celular";
                parametros.Add("@celular");
                parametros.Add("%" + usuario.celular + "%");
            }

            if (!string.IsNullOrWhiteSpace(usuario.correo))
            {
                sqlWhere += " AND correo LIKE @correo";
                parametros.Add("@correo");
                parametros.Add("%" + usuario.correo + "%");
            }

            return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoUsuario, parametros.ToArray()).ToList();
        }

        public Usuario Crear(Usuario usuario)
        {
            string sql =
                    @"INSERT INTO [usuario]
                    (
                        [correo],
                        [clave],
                        [nombrecompleto],
                        [celular]                       
                    )
                    VALUES
                    (
                        @correo,
                        @clave,
                        @nombrecompleto,
                        @celular                       
                    ); SELECT last_insert_rowid() FROM usuario;";

            usuario.idusuario = db.Insertar(sql, ObtenerParametrosUsuario(usuario));
            return usuario;
        }

        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            string sql =
                @"SELECT 
                        [idusuario],
                        [correo],
                        [clave],
                        [nombrecompleto],
                        [celular]
                  FROM [usuario]
                  WHERE [correo] = @correo
                ORDER BY [idusuario] ASC";

            object[] parametros = { "@correo", correo };

            return db.Leer(sql, ConstruirObjetoUsuario, parametros).FirstOrDefault();
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            string sql =
                @"SELECT 
                        [idusuario],
                        [correo],
                        [clave],
                        [nombrecompleto],
                        [celular]
                  FROM [usuario]
                  WHERE [idusuario] = @idusuario
                ORDER BY [idusuario] ASC";

            object[] parametros = { "@idusuario", id };

            return db.Leer(sql, ConstruirObjetoUsuario, parametros).FirstOrDefault();
        }

        public Usuario Actualizar(Usuario usuario)
        {
            string sql =
                    @"UPDATE [usuario] SET 
                    [correo] = @correo,
                    [clave] = @clave,
                    [nombrecompleto] = @nombrecompleto,
                    [celular] = @celular                 
                    WHERE [idusuario] = @idusuario";

            db.Actualizar(sql, ObtenerParametrosUsuario(usuario));
            return usuario;
        }

        public void EliminarUsuario(int id)
        {
            string sql =
                    @"DELETE FROM [usuario]
                    WHERE [idusuario] = @idusuario";

            object[] parametros = { "@idusuario", id };

            db.Eliminar(sql, parametros);
        }

        private object[] ObtenerParametrosUsuario(Usuario o)
        {
            return new object[]  
            {
                
                "@idusuario", o.idusuario,
                "@correo", o.correo,
                "@clave", o.clave,
                "@nombrecompleto", o.nombrecompleto,
                "@celular", o.celular
            };
        }

        private static Func<IDataReader, Usuario> ConstruirObjetoUsuario = lectorDatos =>
        {
            Usuario result = new Usuario()
            {
                idusuario = lectorDatos["idusuario"].ComoId(),
                correo = lectorDatos["correo"].ComoString(),
                clave = lectorDatos["clave"].ComoString(),
                nombrecompleto = lectorDatos["nombrecompleto"].ComoString(),
                celular = lectorDatos["celular"].ComoString()
            };
            return result;
        };

        #endregion

        #region roles

        public IList<Roles> ObtenerListaroles()
        {
            string sql =
                @"SELECT [idrol],
                           [desrol]
                      FROM [roles]
                ORDER BY [idrol] ASC";

            return db.Leer(sql, ConstruirObjetoRol).ToList();
        }

        public Roles Crear(Roles rol)
        {
            string sql =
                    @"INSERT INTO [roles]
                    (
                       [desrol]                                             
                    )
                    VALUES
                    (
                        @desrol                      
                    ); SELECT last_insert_rowid() FROM roles;";

            rol.idrol = db.Insertar(sql, ObtenerParametrosRol(rol));
            return rol;
        }

        public Roles ObtenerRolPorId(int id)
        {
            string sql =
                @"SELECT 
                        [idrol],
                        [desrol]
                  FROM [roles]
                  WHERE [idrol] = @idrol
                ORDER BY [idrol] ASC";

            object[] parametros = { "@idrol", id };

            return db.Leer(sql, ConstruirObjetoRol, parametros).FirstOrDefault();
        }

        public Roles Actualizar(Roles rol)
        {
            string sql =
                    @"UPDATE [roles] SET 
                    [desrol] = @desrol                              
                    WHERE [idrol] = @idrol";

            db.Actualizar(sql, ObtenerParametrosRol(rol));
            return rol;
        }

        public void EliminarRol(int id)
        {
            string sql =
                    @"DELETE FROM [roles]
                    WHERE [idrol] = @idrol";

            object[] parametros = { "@idrol", id };

            db.Eliminar(sql, parametros);
        }

        private object[] ObtenerParametrosRol(Roles o)
        {
            return new object[]  
            {
                
                "@idrol", o.idrol,
                "@desrol", o.desrol
            };
        }

        private static Func<IDataReader, Roles> ConstruirObjetoRol = lectorDatos =>
        {
            Roles result = new Roles()
            {
                idrol = lectorDatos["idrol"].ComoId(),
                desrol = lectorDatos["desrol"].ComoString()
            };

            return result;
        };


        public IList<Roles> BuscarRoles(Roles rol)
        {
            string sqlSelect =
                @"SELECT [idrol],
                           [desrol]
                 FROM [roles] WHERE 1=1 ";

            string sqlWhere = string.Empty;

            List<object> parametros = new List<object>();

            if (!string.IsNullOrWhiteSpace(rol.desrol))
            {
                sqlWhere += " AND desrol LIKE @desrol ";
                parametros.Add("@desrol");
                parametros.Add("%" + rol.desrol + "%");
            }

            return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoRol, parametros.ToArray()).ToList();
        }
        
        #endregion

        #region Permisos
        
        public IList<Permisos> ObtenerListaPermisosPorUsuario(String correo)
        {
            string sql =
                @"SELECT e.* from usuario u
                        inner join perfiles p on u.idusuario  = p.idusuario
                        inner join roles r on r.idrol = p.idrol
                        inner join granulos g on g.idrol = r.idrol
                        inner join permisos e on  e.idpermiso = g.idpermiso
                        where u.correo  = @email";

            object[] parametros = { "@email", correo };

            return db.Leer(sql, ConstruirObjetoPermiso, parametros).ToList();
        }


        public IList<Permisos> ObtenerListaPermisos()
        {
            string sql =
                @"SELECT [idpermiso]    ,
                         [despermiso]  
                      FROM [permisos]
                ORDER BY [idpermiso] ASC";

            return db.Leer(sql, ConstruirObjetoPermiso).ToList();
        }

        public Permisos Crear(Permisos permiso)
        {
            string sql =
                    @"INSERT INTO [permisos]
                    (
                        [despermiso]                                             
                    )
                    VALUES
                    (
                        @despermiso 
                                            
                    ); SELECT last_insert_rowid() FROM permisos;";

            permiso.idpermiso = db.Insertar(sql, ObtenerParametrosPermiso(permiso));
            return permiso;
        }

        public Permisos ObtenerPermisoPorId(int id)
        {
            string sql =
                @"SELECT 
                        [idpermiso],
                        [despermiso]
                  FROM [permisos]
                  WHERE [idpermiso] = @idpermiso
                ORDER BY [idpermiso] ASC";

            object[] parametros = { "@idpermiso", id };

            return db.Leer(sql, ConstruirObjetoPermiso, parametros).FirstOrDefault();
        }

        public Permisos Actualizar(Permisos permiso)
        {
            string sql =
                    @"UPDATE [permisos] SET 
                    [despermiso] = @despermiso                            
                    WHERE [idpermiso] = @idpermiso";

            db.Actualizar(sql, ObtenerParametrosPermiso(permiso));
            return permiso;
        }

        public void EliminarPermiso(int id)
        {
            string sql =
                    @"DELETE FROM [Permisos]
                    WHERE [idpermiso] = @idpermiso";

            object[] parametros = { "@idpermiso", id };

            db.Eliminar(sql, parametros);
        }

        private object[] ObtenerParametrosPermiso(Permisos o)
        {
            return new object[]  
            {
                
                "@idpermiso", o.idpermiso,
                "@despermiso", o.despermiso
            };
        }

        private static Func<IDataReader, Permisos> ConstruirObjetoPermiso = lectorDatos =>
        {
            Permisos result = new Permisos()
            {
                idpermiso = lectorDatos["idpermiso"].ComoId(),
                despermiso = lectorDatos["despermiso"].ComoString()
            };

            return result;
        };

        public IList<Permisos> BuscarPermiso(Permisos permiso)
        {
            string sqlSelect =
                @"SELECT [idpermiso],
                           [despermiso]
                 FROM [permisos] WHERE 1=1 ";

            string sqlWhere = string.Empty;

            List<object> parametros = new List<object>();

            if (!string.IsNullOrWhiteSpace(permiso.despermiso))
            {
                sqlWhere += " AND despermiso LIKE @despermiso ";
                parametros.Add("@despermiso");
                parametros.Add("%" + permiso.despermiso + "%");
            }

            return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoPermiso, parametros.ToArray()).ToList();
        }
        
        #endregion

        #region Perfiles

        public IList<Perfiles> ObtenerListaPerfiles()
        {
            string sql =
                @"SELECT [idperfil]    ,
                         [idusuario]   ,
                         [idrol]
                      FROM [perfiles]
                ORDER BY [idperfil] ASC";

            return db.Leer(sql, ConstruirObjetoPerfil).ToList();
        }

        public Perfiles Crear(Perfiles Perfil)
        {
            string sql =
                    @"INSERT INTO [perfiles]
                    (
                        [idusuario],
                        [idrol]                                             
                    )
                    VALUES
                    (
                        @idusuario, 
                        @idrol                     
                    ); SELECT last_insert_rowid() FROM perfiles;";

            Perfil.idperfil = db.Insertar(sql, ObtenerParametrosPerfil(Perfil));
            return Perfil;
        }

        public Perfiles ObtenerPerfilPorId(int id)
        {
            string sql =
                @"SELECT 
                        [idperfil],
                        [idusuario],
                        [idrol]
                  FROM [Perfiles]
                  WHERE [idperfil] = @idperfil
                ORDER BY [idperfil] ASC";

            object[] parametros = { "@idperfil", id };

            return db.Leer(sql, ConstruirObjetoPerfil, parametros).FirstOrDefault();
        }

        public Perfiles Actualizar(Perfiles perfil)
        {
            string sql =
                    @"UPDATE [Perfiles] SET 
                    [idusuario] = @idusuario,  
                    [idrol] = @idrol                             
                    WHERE [idperfil] = @idperfil";

            db.Actualizar(sql, ObtenerParametrosPerfil(perfil));
            return perfil;
        }

        public void EliminarPerfil(int id)
        {
            string sql =
                    @"DELETE FROM [perfiles]
                    WHERE [idperfil] = @idperfil";

            object[] parametros = { "@idperfil", id };

            db.Eliminar(sql, parametros);
        }

        private object[] ObtenerParametrosPerfil(Perfiles o)
        {
            return new object[]  
            {
                
                "@idperfil", o.idrol,
                "@idusuario", o.idusuario,
                "@idrol", o.idrol
            };
        }

        public IList<Perfiles> BuscarPerfil(Perfiles perfil)
        {
            string sqlSelect =
                @"SELECT [idperfil],
                            [idusuario],
                           [idrol]
                 FROM [perfiles] WHERE 1=1 ";

            string sqlWhere = string.Empty;

            List<object> parametros = new List<object>();

            if (!String.IsNullOrWhiteSpace(perfil.idusuario.ToString()))
            {
                sqlWhere += " AND idusuario LIKE @idusuario ";
                parametros.Add("@idusuario");
                parametros.Add("%" + perfil.idusuario + "%");
            }

            if (!String.IsNullOrWhiteSpace(perfil.idrol.ToString()))
            {
                sqlWhere += " AND idrol LIKE @idrol ";
                parametros.Add("@idrol");
                parametros.Add("%" + perfil.idrol + "%");
            }

            return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoPerfil, parametros.ToArray()).ToList();
        }


        private static Func<IDataReader, Perfiles> ConstruirObjetoPerfil = lectorDatos =>
        {
            Perfiles result = new Perfiles()
            {
                idperfil = lectorDatos["idperfil"].ComoId(),
                idusuario = lectorDatos["idusuario"].ComoId(),
                idrol = lectorDatos["idrol"].ComoId()
            };

            return result;
        };
        #endregion

        #region Granulos

        public IList<Granulos> ObtenerListaGranulos()
        {
            string sql =
                @"SELECT [idgranular]    ,
                         [idrol]   ,
                         [idpermiso]
                      FROM [granulos]
                ORDER BY [idgranular] ASC";

            return db.Leer(sql, ConstruirObjetoGranulo).ToList();
        }

        public Granulos Crear(Granulos Granulo)
        {
            string sql =
                    @"INSERT INTO [granulos]
                    (
                        [idrol],
                        [idpermiso]
                                             
                    )
                    VALUES
                    (
                        @idrol, 
                        @idpermiso                     
                    ); SELECT last_insert_rowid() FROM granulos;";

            Granulo.idgranular = db.Insertar(sql, ObtenerParametrosGranulo(Granulo));
            return Granulo;
        }

        public Granulos ObtenerGranuloPorId(int id)
        {
            string sql =
                @"SELECT 
                        [idgranular],
                        [idrol],
                        [idpermiso]
                  FROM [granulos]
                  WHERE [idgranular] = @idgranular
                ORDER BY [idgranular] ASC";

            object[] parametros = { "@idgranular", id };

            return db.Leer(sql, ConstruirObjetoGranulo, parametros).FirstOrDefault();
        }

        public Granulos Actualizar(Granulos granulo)
        {
            string sql =
                    @"UPDATE [granulos] SET 
                    [idrol] = @idrol,  
                    [idpermiso] = @idpermiso                             
                    WHERE [idgranular] = @idgranular";

            db.Actualizar(sql, ObtenerParametrosGranulo(granulo));
            return granulo;
        }

        public void EliminarGranulo(int id)
        {
            string sql =
                    @"DELETE FROM [granulos]
                    WHERE [idgranular] = @idgranular";

            object[] parametros = { "@idgranular", id };

            db.Eliminar(sql, parametros);
        }

        private object[] ObtenerParametrosGranulo(Granulos o)
        {
            return new object[]  
            {
                
                "@idgranular", o.idgranular,
                "@idrol", o.idrol,
                "@idpermiso", o.idpermiso
            };
        }

        public IList<Granulos> BuscarGranulo(Granulos granulo)
        {
            string sqlSelect =
                @"SELECT [idgranular],
                            [idrol],
                           [idpermiso]
                 FROM [granulos] WHERE 1=1 ";

            string sqlWhere = string.Empty;

            List<object> parametros = new List<object>();

            if (!String.IsNullOrWhiteSpace(granulo.idrol.ToString()))
            {
                sqlWhere += " AND idrol LIKE @idrol ";
                parametros.Add("@idrol");
                parametros.Add("%" + granulo.idrol + "%");
            }

            if (!String.IsNullOrWhiteSpace(granulo.idpermiso.ToString()))
            {
                sqlWhere += " AND idpermiso LIKE @idpermiso ";
                parametros.Add("@idpermiso");
                parametros.Add("%" + granulo.idpermiso + "%");
            }

            return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoGranulo, parametros.ToArray()).ToList();
        }

        private static Func<IDataReader, Granulos> ConstruirObjetoGranulo = lectorDatos =>
        {
            Granulos result = new Granulos()
            {
                idgranular = lectorDatos["idgranular"].ComoId(),
                idrol = lectorDatos["idrol"].ComoId(),
                idpermiso = lectorDatos["idpermiso"].ComoId()
            };

            return result;
        };
        #endregion

        //#region Ciudades

        //public IList<Ciudades> ObtenerListaCiudades()
        //{
        //    string sql =
        //        @"SELECT [idCiudad],
        //                   [idDepartamento]
        //                   [ciudad]
        //              FROM [ciudades]
        //        ORDER BY [idciudad] ASC";

        //    return db.Leer(sql, ConstruirObjetoCiudad).ToList();
        //}

        //public Ciudades Crear(Ciudades ciudad)
        //{
        //    string sql =
        //            @"INSERT INTO [ciudades]
        //            (
        //               [idCiudad] 
        //               [idDepartamento]
        //               [ciudad]
        //            )
        //            VALUES
        //            (
        //                @idCiudad
        //                @idDepartamento
        //                @ciudad
        //            ); SELECT last_insert_rowid() FROM ciudades;";

        //    ciudad.idCiudad = db.Insertar(sql, ObtenerParametrosCiudad(ciudad));
        //    return ciudad;
        //}

        //public Roles ObtenerCiudadPorId(int id)
        //{
        //    string sql =
        //        @"SELECT 
        //                [idCiudad],
        //                [idDepartamento]
        //                [ciudad]
        //          FROM [ciudades]
        //          WHERE [idCiudad] = @idCiudad
        //        ORDER BY [idCiudad] ASC";

        //    object[] parametros = { "@idCiudad", id };

        //    return db.Leer(sql, ConstruirObjetoCiudad, parametros).FirstOrDefault();
        //}

        //public Roles Actualizar(Ciudades ciudad)
        //{
        //    string sql =
        //            @"UPDATE [ciudades] SET 
        //            [idCiudad] = @idCiudad
        //            [idDepartamento] = @idDepartamento 
        //            [ciudad] = @ciudad 
        //            WHERE [idCiudad] = @iCiudad";

        //    db.Actualizar(sql, ObtenerParametrosCiudad(ciudad));
        //    return ciudad;
        //}

        //public void EliminarCiudad(int id)
        //{
        //    string sql =
        //            @"DELETE FROM [ciudades]
        //            WHERE [idCiudad] = @idCiudad";

        //    object[] parametros = { "@idCiudad", id };

        //    db.Eliminar(sql, parametros);
        //}

        //private object[] ObtenerParametrosCiudad(Ciudades o)
        //{
        //    return new object[]
        //    {

        //        "@idCiudad", o.idCiudad,
        //        "@idDepartamento", o.idDepartamento,
        //        "@ciudad", o.ciudad,
        //    };
        //}

        //private static Func<IDataReader, Ciudades> ConstruirObjetoCiudad = lectorDatos =>
        //{
        //    Ciudades result = new Ciudades()
        //    {
        //        idCiudad = lectorDatos["idCiudad"].ComoId(),
        //        idDepartamento = lectorDatos["idDepartamento"].ComoId()
        //        ciudad = lectorDatos["ciudad"].ComoString()
        //    };

        //    return result;
        //};


        //public IList<Ciudades> BuscarCiudades(Ciudades ciudad)
        //{
        //    string sqlSelect =
        //        @"SELECT [idCiudad],
        //                   [idDepartamento]
        //                   [ciudad]
        //         FROM [ciudades] WHERE 1=1 ";

        //    string sqlWhere = string.Empty;

        //    List<object> parametros = new List<object>();

        //    if (!string.IsNullOrWhiteSpace(ciudad.ciudad))
        //    {
        //        sqlWhere += " AND ciudad LIKE @ciudad ";
        //        parametros.Add("@ciudad");
        //        parametros.Add("%" + ciudad.ciudad + "%");
        //    }

        //    return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoCiudad, parametros.ToArray()).ToList();
        //}

        //#endregion
        
        //#region Departamentos

        //public IList<Departamentos> ObtenerListaDepartamentos()
        //{
        //    string sql =
        //        @"SELECT [idDepartamento],
        //                 [departamento]
        //            FROM [departamentos]
        //        ORDER BY [idDepartamento] ASC";

        //    return db.Leer(sql, ConstruirObjetoDepartamento).ToList();
        //}

        //public Departamentos Crear(Departamentos departamento)
        //{
        //    string sql =
        //            @"INSERT INTO [departamentos]
        //            (
        //               [idDepartamento],
        //               [departamento]
        //            )
        //            VALUES
        //            (
        //                @idDepartamento,
        //                @departamento
        //            ); SELECT last_insert_rowid() FROM departamentos;";

        //    departamento.idDepartamento = db.Insertar(sql, ObtenerParametrosDepartamento(departamento));
        //    return departamento;
        //}

        //public Roles ObtenerDepartamentoPorId(int id)
        //{
        //    string sql =
        //        @"SELECT 
        //                [idDepartamento],
        //                [departamento]
        //          FROM [departamentos]
        //          WHERE [idDepartamento] = @idDepartamento
        //        ORDER BY [idDepartamento] ASC";

        //    object[] parametros = { "@idDepartamento", id };

        //    return db.Leer(sql, ConstruirObjetoDepartamento, parametros).FirstOrDefault();
        //}

        //public Roles Actualizar(Departamentos departamento)
        //{
        //    string sql =
        //            @"UPDATE [departamentos] SET 
        //            [idDepartamento] = @idDepartamento,
        //            [departamento] = @departamento 
        //            WHERE [idDepartamento] = @idDepartamento";

        //    db.Actualizar(sql, ObtenerParametrosDepartamento(departamento));
        //    return departamento;
        //}

        //public void EliminarDepartamento(int id)
        //{
        //    string sql =
        //            @"DELETE FROM [departamentos]
        //            WHERE [idDepartamento] = @idDepartamento";

        //    object[] parametros = { "@idDepartamento", id };

        //    db.Eliminar(sql, parametros);
        //}

        //private object[] ObtenerParametrosDepartamento(Departamentos o)
        //{
        //    return new object[]
        //    {
        //        "@idDepartamento", o.idDepartamento,
        //        "@departamento", o.departamento,
        //    };
        //}

        //private static Func<IDataReader, Departamentos> ConstruirObjetoDepartamento = lectorDatos =>
        //{
        //    Departamentos result = new Departamentos()
        //    {
        //        idDepartamento = lectorDatos["idDepartamento"].ComoId(),
        //        departamento = lectorDatos["departamento"].ComoString()
        //    };

        //    return result;
        //};


        //public IList<Departamentos> BuscarDepartamentos(Departamentos departamento)
        //{
        //    string sqlSelect =
        //        @"SELECT [idDepartamento],
        //                   [departamento]
        //         FROM [departamentos] WHERE 1=1 ";

        //    string sqlWhere = string.Empty;

        //    List<object> parametros = new List<object>();

        //    if (!string.IsNullOrWhiteSpace(departamento.departamento))
        //    {
        //        sqlWhere += " AND departamento LIKE @departamento ";
        //        parametros.Add("@departamento");
        //        parametros.Add("%" + departamento.departamento + "%");
        //    }

        //    return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoDepartamento, parametros.ToArray()).ToList();
        //}

        //#endregion

        //#region Clientes

        //public IList<Clientes> ObtenerListaClientes()
        //{
        //    string sql =
        //        @"SELECT [idCliente],
        //                 [nombre]
        //                 [idDepartamento],
        //                 [idCiudad]
        //              FROM [clientes]
        //        ORDER BY [idCliente] ASC";

        //    return db.Leer(sql, ConstruirObjetoClientes).ToList();
        //}

        //public Clientes Crear(Clientes cliente)
        //{
        //    string sql =
        //            @"INSERT INTO [clientes]
        //            (
        //                [idCliente],
        //                [nombre],
        //                [idDepartamento],
        //                [idCiudad]
                                                                     
        //            )
        //            VALUES
        //            (
        //                @idCliente, 
        //                @nombre,
        //                @idDepartamento,
        //                @idCiudad
        //            ); SELECT last_insert_rowid() FROM perfiles;";

        //    Cliente.idCliente = db.Insertar(sql, ObtenerParametrosPerfil(Cliente));
        //    return Cliente;
        //}

        //public Clientes ObtenerClientePorId(int id)
        //{
        //    string sql =
        //        @"SELECT 
        //                [idCliente],
        //                [nombre],
        //                [idciudad],
        //                [idDepartamento]
        //          FROM [clientes]
        //          WHERE [idCliente] = @idperfil
        //        ORDER BY [idCliente] ASC";

        //    object[] parametros = { "@idCliente", id };

        //    return db.Leer(sql, ConstruirObjetoCliente, parametros).FirstOrDefault();
        //}

        //public Perfiles Actualizar(Clientes cliente)
        //{
        //    string sql =
        //            @"UPDATE [clientes] SET 
        //            [idCliente] = @idCliente,  
        //            [nombre] = @nombre,  
        //            [idDepartamento] = @idDepartamento,
        //            [idCiudadad] = @idCiudadad 
        //            WHERE [idCliente] = @idCliente";

        //    db.Actualizar(sql, ObtenerParametrosPerfil(cliente));
        //    return cliente;
        //}

        //public void EliminarCliente(int id)
        //{
        //    string sql =
        //            @"DELETE FROM [clientes]
        //            WHERE [idCliente] = @idCliente";

        //    object[] parametros = { "@idCliente", id };

        //    db.Eliminar(sql, parametros);
        //}

        //private object[] ObtenerParametrosCliente(Clientes o)
        //{
        //    return new object[]
        //    {

        //        "@idCliente", o.idCliente,
        //        "@nombre", o.nombre,
        //        "@idDepartamento", o.idDepartamento,
        //        "@idCiudad", o.idCiudad
        //    };
        //}

        //public IList<Clientes> BuscarCliente(Clientes cliente)
        //{
        //    string sqlSelect =
        //        @"SELECT [idCliente],
        //                 [nombre],
        //                 [idDepartamento],
        //                 [idciudad]
        //         FROM [clientes] WHERE 1=1 ";

        //    string sqlWhere = string.Empty;

        //    List<object> parametros = new List<object>();

        //    if (!String.IsNullOrWhiteSpace(cliente.idDepartamento.ToString()))
        //    {
        //        sqlWhere += " AND idDepartamento LIKE @idDepartamento ";
        //        parametros.Add("@idDepartamento");
        //        parametros.Add("%" + cliente.idDepartamento + "%");
        //    }

        //    if (!String.IsNullOrWhiteSpace(ciudad.idCiudad.ToString()))
        //    {
        //        sqlWhere += " AND idCiudad LIKE @idCiudad ";
        //        parametros.Add("@idCiudad");
        //        parametros.Add("%" + ciudad.idCiudad + "%");
        //    }

        //    return db.Leer(sqlSelect + sqlWhere, ConstruirObjetoCliente, parametros.ToArray()).ToList();
        //}


        //private static Func<IDataReader, Clientes> ConstruirObjetoCliente = lectorDatos =>
        //{
        //    Clientes result = new Clientes()
        //    {
        //        idcliente = lectorDatos["idCliente"].ComoId(),
        //        nombre = lectorDatos["nombre"].ComoString(),
        //        idusuario = lectorDatos["idCiudad"].ComoId(),
        //        idrol = lectorDatos["idDepartamento"].ComoId()
        //    };

        //    return result;
        //};
        //#endregion

    }
}
