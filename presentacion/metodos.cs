using negocio.Componentes;
using negocio.Entidades;
using System.Data;
using System.IO;

namespace presentacion
{
    public class metodos
    {
        public int idc_aprobacion;
        public int idc_registro;
        public string parametros;

        public string ejecutar_metodo()
        {
            string vmensaje = "";
            string del = ";";
            string[] pm_array = parametros.Split(del.ToCharArray());
            switch (idc_aprobacion)
            {
                case 1: //antes 8
                        //en este caso se manda es estatus y el id del borrador segun sea el caso

                    //en esta se hace el vaciado de informacion de un perfil de borrador a un perfil verdadero.
                    if (pm_array[0] == "aprobada")
                    {
                        PerfilesE perfilEnt = new PerfilesE();
                        perfilEnt.Idc_puestoperfil_borr = idc_registro;
                        DataSet ds_1 = new DataSet();
                        PerfilesBL perfilCom = new PerfilesBL();
                        ds_1 = perfilCom.perfiles_pendientes_acciones(perfilEnt, "autorizar");
                        vmensaje = ds_1.Tables[0].Rows[0]["mensaje"].ToString();
                    }
                    else if (pm_array[0] == "cancelada")
                    {
                        //regresamos el borrador, ya hay una funcion en perfiles_pendiente
                        perfiles_pendiente perfilPendiente = new perfiles_pendiente();
                        perfilPendiente.regresaBorrador(idc_registro);
                    }
                    else if (pm_array[0] == "detalle")
                    {
                        DataSet ds = new DataSet();
                        //entidad
                        PerfilesE entidad = new PerfilesE();
                        entidad.Idc_perfil = idc_registro;
                        //componente
                        PerfilesBL componente = new PerfilesBL();
                        ds = componente.perfiles_datos_borr(entidad);
                        DataTable tbl_puestos_perfil_borr = ds.Tables[0];

                        vmensaje = "Esta solicitud es para aprobar el siguiente Perfil: " + tbl_puestos_perfil_borr.Rows[0]["descripcion"] + "<br/> para mas información Clic <a class='btn btn-success' href='perfiles_detalle.aspx?borrador=1&uidc_puestoperfil_borr=" + idc_registro + "'>Aqui</a>";
                    }

                    break;

                case 2: //antes 10
                    //ESTE ID ES DE LA APROBACION PARA CURSOS
                    if (pm_array[0] == "aprobada")
                    {
                        //llamamos la entidad
                        CursosE EntCurso = new CursosE();
                        EntCurso.Idc_curso_borr = idc_registro; //se refiere al idc_curso_borr en este caso
                        EntCurso.Produccion = true;//Convert.ToBoolean(pm_array[1].ToString());
                        //EntCurso.Idc_usuario = Convert.ToInt32(pm_array[2]);
                        //dataset
                        DataSet ds = new DataSet();
                        //componente
                        CursosCOM ComCurso = new CursosCOM();
                        ds = ComCurso.cursosVaciar(EntCurso);
                        string menj = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (string.IsNullOrEmpty(menj)) // si esta vacio todo bien
                        {
                            //GUARDAMOS LOS ARCHIVOS
                            //recuperos el datatable
                            if (ds.Tables.Count > 1)
                            {
                                DataTable tabla_bd = ds.Tables[1]; //este viene de la base de datos
                                                                   // DataTable tabla_temp = (DataTable)Session["TablaCursoArc"]; //este viene de la interfaz
                                                                   //en teoria deben ser los mismos registros ordenados
                                                                   //recorremos un datatable de la BD
                                int indice = 0;
                                foreach (DataRow fila in tabla_bd.Rows)
                                {
                                    string fileName = fila["idc_curso_arc_borr"].ToString() + fila["extension_borr"];// ejemplo 24.pdf tabla_temp.Rows[indice]["path"].ToString(); //archivo que se va a copiar se saca de la tabla temporal
                                    string sourcePath = Path.Combine(fila["ruta_borr"].ToString());// Server.MapPath("~/temp/cursos/"); //carpeta temporal
                                    string targetPath = fila["ruta"].ToString(); //carpeta de red de destino este lo retorna la BD
                                    if (!System.IO.Directory.Exists(sourcePath))
                                    {
                                        vmensaje = "no existe el path temporal";
                                        return vmensaje;
                                    }
                                    if (!System.IO.Directory.Exists(targetPath))
                                    {
                                        vmensaje = "no existe el path de destino";
                                        return vmensaje;
                                    }
                                    //
                                    // Use Path class to manipulate file and directory paths.
                                    string fileNameNuevo = fila["idc_curso_arc"].ToString() + fila["extension_borr"].ToString(); //el nombre de archivo de destino en la unidad de red es el id del registro+ la extension

                                    string sourceFile = Path.Combine(sourcePath, fileName);
                                    string destFile = Path.Combine(targetPath, fileNameNuevo);
                                    //se copia el archivo
                                    try
                                    {
                                        File.Copy(sourceFile, destFile, true);
                                        indice = indice + 1;
                                    }
                                    catch (IOException copyError)
                                    {
                                        vmensaje = copyError.Message;
                                        return vmensaje;
                                    }

                                    //fin
                                }
                            }
                        }
                    }
                    else if (pm_array[0] == "cancelada")
                    {
                        //regresamos el borrador, ya hay una funcion en cursos_pendiente
                        cursos_pendiente CursoPendiente = new cursos_pendiente();
                        CursoPendiente.regresaBorrador(idc_registro);
                    }
                    else if (pm_array[0] == "detalle")
                    {
                        DataSet ds = new DataSet();
                        //entidad
                        CursosE entidad = new CursosE();
                        entidad.Idc_curso_borr = idc_registro;
                        entidad.Borrador = true;
                        //componente
                        CursosCOM componente = new CursosCOM();
                        ds = componente.cursos(entidad);
                        DataTable tbl_cursos_borr = ds.Tables[0];

                        vmensaje = "Esta solicitud es para aprobar el siguiente Curso: " + tbl_cursos_borr.Rows[0]["descripcion"] + "<br/> para mas información Clic <a class='btn btn-success' href='cursos_detalle.aspx?uidc_curso=" + tbl_cursos_borr.Rows[0]["idc_curso"] + "&uidc_curso_borr=" + idc_registro + "'>Aqui</a>";
                    }
                    break;

                case 3: // antes 11este es para la aprobacion de ligar un perfil con un puesto
                    if (pm_array[0] == "aprobada")
                    {
                        PuestosENT EntPuesto = new PuestosENT();
                        EntPuesto.Idc_Puesto = idc_registro;
                        //ds
                        DataSet ds = new DataSet();
                        //componente
                        PuestosCOM ComPuesto = new PuestosCOM();
                        ds = ComPuesto.puesto_perfil_temp_update(EntPuesto, true);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    }
                    else if (pm_array[0] == "cancelada")
                    {
                        PuestosENT EntPuesto = new PuestosENT();
                        EntPuesto.Idc_Puesto = idc_registro;
                        //ds
                        DataSet ds = new DataSet();
                        //componente
                        PuestosCOM ComPuesto = new PuestosCOM();
                        ds = ComPuesto.puesto_perfil_temp_update(EntPuesto, false);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    }
                    else if (pm_array[0] == "detalle")
                    {
                        DataSet ds = new DataSet();
                        //entidad
                        PuestosENT entidad = new PuestosENT();
                        entidad.Idc_Puesto = idc_registro;
                        //componente
                        PuestosCOM componente = new PuestosCOM();
                        ds = componente.puesto_aprob_detalle(entidad);
                        DataTable tbl_puesto_perfil_temp = ds.Tables[0];

                        vmensaje = "Esta solicitud es para aprobar la relación del puesto: " + tbl_puesto_perfil_temp.Rows[0]["puesto"] + " con el perfil: " + tbl_puesto_perfil_temp.Rows[0]["perfil"];
                    }
                    break;
            }

            return vmensaje;
        }
    }
}