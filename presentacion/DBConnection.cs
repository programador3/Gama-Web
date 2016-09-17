using datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Esta Clase fue agregada al proyecto PUESTOS (17/09/2016) por la migracion que hubo de GamaWEB a Puestos
/// </summary>
public class DBConnection
{
    private SqlConnection connection;
    private string str;

    //Opciones Ventas (Agentes): 618 agenda, 1163 pedidos, 257 actividades
    private int[] salesOptions = { 618, 257 }; //Proximamente considerar cambiar la asignacion por DB

    //Opciones Revision de Herramientas :1094 Revision Herramientas,1367 Revision Basica, 1369 Revisiones Pendientes
    private int[] RevisionOptions = { 1367, 1094, 1369 }; //

    //Opciones Main Menu: 1252 Inventario, 1249 Hallazgos (Movida, Menu Hallazgos), 1344 Revision Fisica de Vehiculos, 1357 Revision de Sucursales, Revision Taller
    private int[] mainMenuOptions = { 1344, 524, 1408, 623, 121, 994 };   ///se quito 1252 paso a InventarioOptions

    //Opciones Revision de Sucursales: 1357 Revision de Sucursal
    private int[] RevisionOptionsSuc = { 1357 };

    private int[] InventarioOptions = { 1252 };

    private int[] opc_hallazgos = { 1249, 1433, 1412, 1519, 1566, 1567, 1568 };

    public DBConnection()
    {
        //str = "Data Source=192.168.0.4;Initial Catalog=GM;Persist Security Info=True;User ID=conexion;Password=GaMa90";
        // connection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings("strDeConexion"));
        string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];
        string cadena = "";
        if (cs == "P")
            cadena = recursos.cadena_conexion;
        else
            cadena = recursos.cadena_conexion_respa;
        connection = new SqlConnection(cadena);
    }

    #region "Conectar/Desconectar"

    public void conectar()
    {
        connection.Open();
    }

    public void desconectar()
    {
        connection.Close();
    }

    #endregion "Conectar/Desconectar"

    //private string strHostName = System.Net.Dns.GetHostName();

    //private string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(1).ToString();

    #region "Funciones del Inventario"

    public Int32 ChecarConteo(Int32 idarticulo, Int32 idsucursal, string conteo, string usuario, string nombrepc, string userip)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_aconteo_inventario_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@pidc_articulo", idarticulo);
        cmd.Parameters.AddWithValue("@pidc_sucursal", idsucursal);
        cmd.Parameters.AddWithValue("@pidc_usuario", usuario);
        cmd.Parameters.AddWithValue("@pconteo", Convert.ToDecimal(conteo));
        cmd.Parameters.AddWithValue("@pdirecip", userip);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);

        SqlDataReader reader = cmd.ExecuteReader();

        int vbien = 0;

        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                vbien = Convert.ToInt32(reader["bien"]);
            }
        }

        reader.Close();
        connection.Close();

        return vbien;
    }

    public DataSet getSucursales()
    {
        //Son de hecho almacenes

        connection.Open();
        SqlCommand command = new SqlCommand("SELECT [idc_sucursal], [nombre], [idc_almacen] FROM [sucursales] WHERE (([activa] = @activa) AND ([tipo] = @tipo2))", connection);
        command.CommandType = System.Data.CommandType.Text;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@activa", true);
        command.Parameters.AddWithValue("@tipo2", "F");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getcamiones_rev(Int16 idc_sucursal)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_vehiculo_x_sucursal_reparto", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_sucursal", idc_sucursal);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getrevtaller_gpos()
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_revtaller_gpos_agregar", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getrevtaller_elementos(int vidc_gporevtaller)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_revtaller_elementos_grupo_agregar", connection);

        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_gporevtaller", vidc_gporevtaller);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getcamiones_suc(Int16 idc_sucursal)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_vehiculo_x_sucursal", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_sucursal", idc_sucursal);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getcamiones_reparto()
    {
        connection.Open();
        SqlCommand command = new SqlCommand("SP_SELECCIONA_VEHICULO_REPARTO", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pvalor", "");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getempleados_suc(int vidc_sucursal)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_selecciona_empleado_sucursal", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pvalor", "");
        command.Parameters.AddWithValue("@pidc_sucursal", vidc_sucursal);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getModulos(Int16 idalmacen)

    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_modulos_almacen", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_almacen", idalmacen);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    #endregion "Funciones del Inventario"

    #region "Funcion del Login"

    public SqlDataReader checarInicioSesion(string usuario, string password)
    {
        SqlCommand command = new SqlCommand("select * FROM fn_usuario_contraseña_detalles_IVA(@PUSUARIO,@PCONTRASEÑA) as pasa", connection);
        command.CommandType = System.Data.CommandType.Text;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pUSUARIO", usuario);
        command.Parameters.AddWithValue("@pCONTRASEÑA", password);

        return command.ExecuteReader();
    }

    #endregion "Funcion del Login"

    #region "Funciones Menu Principal"

    public string direccion_ip()
    {
        ///string strHostName = System.Net.Dns.GetHostName();
        // string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(1).ToString();
        ///string ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString();
        //   string ipAddressObsolete = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();

        string ipAddressObsolete = "127.0.0.1";

        return ipAddressObsolete;
    }

    public string getOptionName(int optionID)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("select descripcion from opciones where idc_opcion=" + optionID.ToString(), connection);
        command.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = command.ExecuteReader();

        string name = "";
        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                name = reader["descripcion"].ToString();
            }
        }
        reader.Close();
        connection.Close();

        //if (Convert.ToInt32(optionID).Equals(1252))
        //{
        //    name = "conteo";
        //}

        return name;
    }

    public string getnomsuc(int vidc_sucursal)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("select nombre from sucursales where idc_sucursal=" + vidc_sucursal.ToString(), connection);
        command.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = command.ExecuteReader();

        string name = "";
        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                name = reader["nombre"].ToString();
            }
        }
        reader.Close();
        connection.Close();

        return name;
    }

    public string gettipo_revele(int vidc_elerev)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("select tipo from revsuc_elementos where idc_elerev=" + vidc_elerev.ToString(), connection);

        command.CommandType = System.Data.CommandType.Text;

        SqlDataReader reader = command.ExecuteReader();

        string vtipo = "";

        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                vtipo = reader["tipo"].ToString();
            }
        }
        reader.Close();
        connection.Close();

        return vtipo;
    }

    public string getnom_modulo(int vidc_modulo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("select nombre from modulos_alm where idc_modulo=" + vidc_modulo.ToString(), connection);

        command.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = command.ExecuteReader();

        string name = "";
        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                name = reader["nombre"].ToString();
            }
        }
        reader.Close();
        connection.Close();

        return name;
    }

    public string getnom_empleado(int vidc_empleado)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("select ltrim(rtrim(str(num_nomina)))+'.- '+ltrim(rtrim(nombre))+space(1)+ltrim(rtrim(paterno))+space(1)+ltrim(rtrim(materno)) as nombre from empleados where idc_empleado=" + vidc_empleado.ToString(), connection);

        command.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = command.ExecuteReader();

        string name = "";
        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                name = reader["nombre"].ToString();
            }
        }
        reader.Close();
        connection.Close();

        return name;
    }

    public string getnomvehiculo(int vidc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("select ltrim(rtrim(str(num_economico)))+'.- '+ltrim(rtrim(descripcion)) as nombre from vehiculos where idc_vehiculo=" + vidc_vehiculo.ToString(), connection);

        command.CommandType = System.Data.CommandType.Text;
        SqlDataReader reader = command.ExecuteReader();

        string name = "";
        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                name = reader["nombre"].ToString();
            }
        }
        reader.Close();
        connection.Close();

        return name;
    }

    public bool userCanViewSales(int userID)
    {
        bool optionAvailable = false;

        connection.Open();

        foreach (int c in salesOptions)
        {
            SqlCommand command = new SqlCommand("select * from opciones_usuarios where idc_opcion=" +
            c.ToString() + " and idc_usuario=" + userID.ToString() + " and activo=1", connection);

            command.CommandType = System.Data.CommandType.Text;

            command.CommandTimeout = 10;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                optionAvailable = true;
            }

            reader.Close();
            reader.Dispose();
        }
        connection.Close();

        return optionAvailable;
    }

    public bool userCanViewInventario(int userID)
    {
        bool optionAvailable = false;

        connection.Open();

        foreach (int c in InventarioOptions)
        {
            SqlCommand command = new SqlCommand("select * from opciones_usuarios where idc_opcion=" +
            c.ToString() + " and idc_usuario=" + userID.ToString() + " and activo=1", connection);

            command.CommandType = System.Data.CommandType.Text;

            command.CommandTimeout = 10;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                optionAvailable = true;
            }

            reader.Close();
            reader.Dispose();
        }
        connection.Close();

        return optionAvailable;
    }

    public bool userCanViewRevision(int userID)
    {
        bool optionAvailable = false;

        connection.Open();

        foreach (int c in RevisionOptions)
        {
            SqlCommand command = new SqlCommand("select * from opciones_usuarios where idc_opcion=" +
                       c.ToString() + " and idc_usuario=" + userID.ToString() + " and activo=1", connection);

            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                optionAvailable = true;
            }

            reader.Close();
            reader.Dispose();
        }
        connection.Close();

        return optionAvailable;
    }

    public bool userCanViewRevisionSuc(int userID)
    {
        bool optionAvailable = false;

        connection.Open();

        foreach (int c in RevisionOptionsSuc)
        {
            SqlCommand command = new SqlCommand("select * from opciones_usuarios where idc_opcion=" +
                       c.ToString() + " and idc_usuario=" + userID.ToString() + " and activo=1", connection);

            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                optionAvailable = true;
            }

            reader.Close();
            reader.Dispose();
        }
        connection.Close();

        return optionAvailable;
    }

    public DataTable getSalesSubmenuByUser(int userID)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("idc_opcion");
        dt.Columns.Add("descripcion");
        dt.Columns.Add("web_form");
        int[] opciones = new int[salesOptions.Length];
        //int i = 0;
        connection.Open();

        foreach (int c in salesOptions)
        {
            SqlCommand command = new SqlCommand("select opciones.idc_opcion,opciones.descripcion,opciones.web_form from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" + c.ToString() + " and opciones_usuarios.idc_usuario=" + userID.ToString() + " and opciones_usuarios.activo=1 and opciones.activo = 1", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);
                    row = dt.NewRow();
                    row[0] = 0;
                    row[0] = Convert.ToInt32(reader[0]);
                    row[1] = Convert.ToString(reader[1]);
                    row[2] = Convert.ToString(reader[2]);
                    dt.Rows.Add(row);
                }
            }

            //        row["descripcion"] = reader["descripcion"];
            //        row["web_form"] = reader["web_form"];
            //row = reader[0];

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return dt;
    }

    public int[] getRevisionSubmenuByUser(int userID)
    {
        int[] opciones = new int[RevisionOptions.Length];
        int i = 0;
        connection.Open();

        foreach (int c in RevisionOptions)
        {
            SqlCommand command = new SqlCommand("select * from opciones_usuarios where idc_opcion=" +
                c.ToString() + " and idc_usuario=" + userID.ToString() + " and activo=1", connection);

            command.CommandType = System.Data.CommandType.Text;

            command.CommandTimeout = 10;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                    opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return opciones;
    }

    /// <summary>
    /// //
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public DataTable menuhallazgos(int userID)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("idc_opcion");
        dt.Columns.Add("descripcion");
        dt.Columns.Add("web_form");
        connection.Open();
        foreach (int c in opc_hallazgos)
        {
            SqlCommand command = new SqlCommand("select opciones.idc_opcion,opciones.descripcion,opciones.web_form from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" + c.ToString() + " and opciones_usuarios.idc_usuario=" + userID.ToString() + " and opciones_usuarios.activo=1 and opciones.activo = 1 and opciones.activo = 1", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);
                    row = dt.NewRow();
                    row[0] = 0;
                    row[0] = Convert.ToInt32(reader[0]);
                    row[1] = Convert.ToString(reader[1]);
                    row[2] = Convert.ToString(reader[2]);
                    dt.Rows.Add(row);
                }
            }

            //        row["descripcion"] = reader["descripcion"];
            //        row["web_form"] = reader["web_form"];
            //row = reader[0];

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return dt;
    }

    //

    public DataTable sub_generales(int userID)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("idc_opcion");
        dt.Columns.Add("descripcion");
        dt.Columns.Add("web_form");
        connection.Open();
        foreach (int c in opc_hallazgos)
        {
            SqlCommand command = new SqlCommand("select opciones.idc_opcion,opciones.descripcion,opciones.web_form from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" + c.ToString() + " and opciones_usuarios.idc_usuario=" + userID.ToString() + " and opciones_usuarios.activo=1 and opciones.activo = 1", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);
                    row = dt.NewRow();
                    row[0] = 0;
                    row[0] = Convert.ToInt32(reader[0]);
                    row[1] = Convert.ToString(reader[1]);
                    row[2] = Convert.ToString(reader[2]);
                    dt.Rows.Add(row);
                }
            }

            //        row["descripcion"] = reader["descripcion"];
            //        row["web_form"] = reader["web_form"];
            //row = reader[0];

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return dt;
    }

    public DataTable getinventarioSubmenuByUser(int userID)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("idc_opcion");
        dt.Columns.Add("descripcion");
        dt.Columns.Add("web_form");
        connection.Open();
        foreach (int c in InventarioOptions)
        {
            SqlCommand command = new SqlCommand("select opciones.idc_opcion,opciones.descripcion,opciones.web_form from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" + c.ToString() + " and opciones_usuarios.idc_usuario=" + userID.ToString() + " and opciones_usuarios.activo=1 and opciones.activo = 1", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);
                    row = dt.NewRow();
                    row[0] = 0;
                    row[0] = Convert.ToInt32(reader[0]);
                    row[1] = Convert.ToString(reader[1]);
                    row[2] = Convert.ToString(reader[2]);
                    dt.Rows.Add(row);
                }
            }

            //        row["descripcion"] = reader["descripcion"];
            //        row["web_form"] = reader["web_form"];
            //row = reader[0];

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return dt;
    }

    public int[] getRevisionSucSubmenuByUser(int userID)
    {
        int[] opciones = new int[RevisionOptions.Length];
        int i = 0;
        connection.Open();

        foreach (int c in RevisionOptions)
        {
            SqlCommand command = new SqlCommand("select * from opciones_usuarios where idc_opcion=" +
                c.ToString() + " and idc_usuario=" + userID.ToString() + " and activo=1", connection);

            command.CommandType = System.Data.CommandType.Text;

            command.CommandTimeout = 10;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                    opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);

            reader.Close();

            reader.Dispose();
        }

        connection.Close();

        return opciones;
    }

    public DataTable getMenuOptions(int userID)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("idc_opcion");
        dt.Columns.Add("descripcion");
        dt.Columns.Add("web_form");
        int[] opciones = new int[salesOptions.Length];
        connection.Open();

        foreach (int c in mainMenuOptions)
        {
            SqlCommand command = new SqlCommand("select opciones.idc_opcion,opciones.descripcion,opciones.web_form from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" + c.ToString() + " and opciones_usuarios.idc_usuario=" + userID.ToString() + " and opciones_usuarios.activo=1 and opciones.activo = 1", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);
                    row = dt.NewRow();
                    row[0] = 0;
                    row[0] = Convert.ToInt32(reader[0]);
                    row[1] = Convert.ToString(reader[1]);
                    row[2] = Convert.ToString(reader[2]);
                    dt.Rows.Add(row);
                }
            }

            //        row["descripcion"] = reader["descripcion"];
            //        row["web_form"] = reader["web_form"];
            //row = reader[0];

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return dt;
    }

    #endregion "Funciones Menu Principal"

    #region "Funciones de Hallazgos"

    public DataSet getSucursalesDisponibles()
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_sucursales_combo_disponibles", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getSucursalesDisponibles_ip(string direcip, int id_usuario)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_sucursales_combo_disponibles_ip", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.AddWithValue("@pdirecip", direcip);
        command.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet valid_options(string cadena, int id_usuario)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("Sp_MenuOpciones_cad", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.AddWithValue("@PCADENA", cadena);
        command.Parameters.AddWithValue("@idc_usuario", id_usuario);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet getpre_embarques_pendientes(int id_usuario)

    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_pre_embarques_pendientes_modi_usuario_web", connection);
        //SqlCommand command = new SqlCommand("sp_pre_embarques_pendientes_modi_usuario", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 6000;

        command.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet altaHallazgo(Int16 idsucursal, string hallazgo, Int16 idusuario, string ipusuario, string nombrepc, int idc_vehiculo, int idc_usuario_sol)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_ahallazgos", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@pidc_sucursal", idsucursal);
        cmd.Parameters.AddWithValue("@phallazgo", hallazgo);
        cmd.Parameters.AddWithValue("@pidc_usuario", idusuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pidc_vehiculo", idc_vehiculo);
        cmd.Parameters.AddWithValue("@pidc_usuario_sol", idc_usuario_sol);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revtaller_elemento(Int16 idc_revtallercheck, Int16 idc_elerevtaller, Int16 idusuario, string ipusuario, string nombrepc)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_arevtaller_checkd_ele_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_revtallercheck", idc_revtallercheck);
        cmd.Parameters.AddWithValue("@pidc_elerevtaller", idc_elerevtaller);
        cmd.Parameters.AddWithValue("@pidc_usuario", idusuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision(Int16 id_requerirveh, Int16 id_empleado, Int16 id_vehiculo, Int16 idusuario,
                                 string cadg, Int16 numg, string cadh, Int16 numh,
                                 string cado, Int16 numo, string ipusuario, string nombrepc)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_aasignacion_veh_requerir_revision_rev_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@pidc_requerirveh", id_requerirveh);
        cmd.Parameters.AddWithValue("@pidc_empleado", id_empleado);
        cmd.Parameters.AddWithValue("@pidc_vehiculo", id_vehiculo);
        cmd.Parameters.AddWithValue("@pidc_usuario", idusuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pcadenag", cadg);
        cmd.Parameters.AddWithValue("@pnumg", numg);
        cmd.Parameters.AddWithValue("@pcadenah", cadh);
        cmd.Parameters.AddWithValue("@pnumh", numh);
        cmd.Parameters.AddWithValue("@pcadena_obs", cado);
        cmd.Parameters.AddWithValue("@pnum_obs", numo);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_herra(Int16 id_empleado, Int16 id_vehiculo, Int16 idusuario, Int16 id_sucursal,
                             string cadena, Int16 num, string cadena2, Int16 num_hall, bool falta, decimal total, decimal descontar,
                              string ipusuario, string nombrepc, bool basica, int idc_revbasherr)
    {
        connection.Open();

        SqlCommand cmd = new SqlCommand("sp_arevision_herramientas_todo_hall_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_empleado", id_empleado);
        cmd.Parameters.AddWithValue("@pidc_vehiculo", id_vehiculo);
        cmd.Parameters.AddWithValue("@pidc_usuario", idusuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pcadena", cadena);
        cmd.Parameters.AddWithValue("@pnum", num);
        cmd.Parameters.AddWithValue("@pfalta", falta);
        cmd.Parameters.AddWithValue("@pidc_sucursal", id_sucursal);
        cmd.Parameters.AddWithValue("@ptotal", total);
        cmd.Parameters.AddWithValue("@pdescontar", descontar);
        cmd.Parameters.AddWithValue("@pcadena2", cadena2);
        cmd.Parameters.AddWithValue("@pnum_hall", num_hall);
        cmd.Parameters.AddWithValue("@pbasica", basica);
        cmd.Parameters.AddWithValue("@pidc_revbasherr", idc_revbasherr);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_suc(Int16 id_usuario, Int16 id_sucursal,
   string ipusuario, string nombrepc, string vobservaciones, bool aplica_solucion,
    bool bien, Int32 vidc_elerev)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_arevsuc_checkd_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_sucursal", id_sucursal);
        cmd.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@ppobservaciones", vobservaciones);
        cmd.Parameters.AddWithValue("@ppaplica_solucion", aplica_solucion);
        cmd.Parameters.AddWithValue("@pidc_elerev", vidc_elerev);
        cmd.Parameters.AddWithValue("@ppbien", bien);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_taller_mod(Int16 id_usuario, Int16 id_vehiculo, string ipusuario, string nombrepc, string vobservaciones, bool pendiente,
            bool bien, Int32 vidc_elerevtaller, int vidc_revtallercheckd)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_arevtaller_checkd", connection);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_vehiculo", id_vehiculo);
        cmd.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pobservaciones", vobservaciones);
        cmd.Parameters.AddWithValue("@ppendiente", pendiente);
        cmd.Parameters.AddWithValue("@pidc_elerevtaller", vidc_elerevtaller);

        cmd.Parameters.AddWithValue("@pidc_revtallercheckd", vidc_revtallercheckd);

        cmd.Parameters.AddWithValue("@pbien", bien);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_taller(Int16 id_usuario, Int16 id_vehiculo, string ipusuario, string nombrepc, string vobservaciones, bool pendiente,
    bool bien, Int32 vidc_elerevtaller, int vidc_revtallercheckd)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_arevtaller_checkd_web", connection);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_vehiculo", id_vehiculo);
        cmd.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pobservaciones", vobservaciones);
        cmd.Parameters.AddWithValue("@ppendiente", pendiente);
        cmd.Parameters.AddWithValue("@pidc_elerevtaller", vidc_elerevtaller);

        cmd.Parameters.AddWithValue("@pidc_revtallercheckd", vidc_revtallercheckd);

        cmd.Parameters.AddWithValue("@pbien", bien);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_suc_unidades(Int16 id_usuario, Int16 id_sucursal,
    string ipusuario, string nombrepc, string vobservaciones, bool aplica_solucion,
    bool bien, Int32 vidc_elerev, Int16 vidc_vehiculo)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_arevsuc_checkd_veh2_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_sucursal", id_sucursal);
        cmd.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pobservaciones", vobservaciones);
        cmd.Parameters.AddWithValue("@paplica_solucion", aplica_solucion);
        cmd.Parameters.AddWithValue("@pidc_elerev", vidc_elerev);
        cmd.Parameters.AddWithValue("@pbien", bien);
        cmd.Parameters.AddWithValue("@pidc_vehiculo", vidc_vehiculo);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_suc_unidades_herramientas(Int16 id_usuario, Int16 id_sucursal,
    string ipusuario, string nombrepc, string vobservaciones, bool aplica_solucion,
    bool bien, Int32 vidc_elerev, Int16 vidc_vehiculo, int vidc_empleado, int vnum, bool vfalta,
    string vcadena, Int16 vidc_sucursalv, float vtotal, float descontar)
    {
        connection.Open();

        SqlCommand cmd = new SqlCommand("sp_arevsuc_checkd_veh2_vale_web", connection);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_sucursal", id_sucursal);
        cmd.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pobservaciones", vobservaciones);
        cmd.Parameters.AddWithValue("@paplica_solucion", aplica_solucion);
        cmd.Parameters.AddWithValue("@pidc_elerev", vidc_elerev);
        cmd.Parameters.AddWithValue("@pbien", bien);
        cmd.Parameters.AddWithValue("@pidc_vehiculo", vidc_vehiculo);
        cmd.Parameters.AddWithValue("@pidc_empleado", vidc_empleado);
        cmd.Parameters.AddWithValue("@pnum", vnum);
        cmd.Parameters.AddWithValue("@pfalta", vfalta);
        cmd.Parameters.AddWithValue("@pcadena", vcadena);
        cmd.Parameters.AddWithValue("@PIDC_SUCURSALv", vidc_sucursalv);
        cmd.Parameters.AddWithValue("@ptotal", vtotal);
        cmd.Parameters.AddWithValue("@Pdescontar", descontar);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_revision_suc_patio(Int16 id_usuario, Int16 id_sucursal,
    string ipusuario, string nombrepc, string vobservaciones, bool aplica_solucion,
    bool bien, Int32 vidc_elerev, Int16 vidc_modulo, int vidc_empleado)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_arevsuc_checkd_mod2_web", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pidc_sucursal", id_sucursal);
        cmd.Parameters.AddWithValue("@pidc_usuario", id_usuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pobservaciones", vobservaciones);
        cmd.Parameters.AddWithValue("@paplica_solucion", aplica_solucion);
        cmd.Parameters.AddWithValue("@pidc_elerev", vidc_elerev);
        cmd.Parameters.AddWithValue("@pbien", bien);
        cmd.Parameters.AddWithValue("@pidc_modulo", vidc_modulo);
        cmd.Parameters.AddWithValue("@pidc_empleado", vidc_empleado);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet alta_pedido(Int16 ventrar, int idusuario, int id_cliente, decimal monto, int id_sucursal,
        bool vdesiva, Int16 vidc_iva, Int16 vidc_almacen, string vdsinexi, string VDCOSBAJO, string VDETALLES,
        bool vbitlla, int SIPASA2, decimal VFLETE, bool vbitcro, string VRECOGE, Int16 vidc_occli,
        string vdarti, string vdarti2, Int16 vtota, string vocc, Int32 vfoliocp,
        string vfechaent, bool vpro, Int16 vidpro, string vobs, int sipasa, Int16 vidc_sucursal_recoge,
        int vidcc, string vcalle, string vnumero, int vcp, Int16 ventdir, bool VBITOCC,
        Int32 vplazo, Int16 vtipopago, string votro, string vcanminima, string vcontacto, string vtelefono,
        string vmail, Int16 VTIPOP, int vidc_banco, decimal vmonto_pago, string vobservaciones_pago,
        bool vconfirmar_pago, string vfecha_deposito, string ipusuario, string nombrepc, string vtipog, string vsp)
    {
        connection.Open();

        SqlCommand cmd = new SqlCommand(vsp, connection);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@pidc_cliente", id_cliente);
        cmd.Parameters.AddWithValue("@pmonto", monto);
        cmd.Parameters.AddWithValue("@pidc_sucursal", id_sucursal);
        cmd.Parameters.AddWithValue("@pidc_usuario", idusuario);
        cmd.Parameters.AddWithValue("@pdirecip", ipusuario);
        cmd.Parameters.AddWithValue("@pnombrepc", nombrepc);
        cmd.Parameters.AddWithValue("@pdesiva", vdesiva);
        cmd.Parameters.AddWithValue("@pidc_iva", vidc_iva);
        cmd.Parameters.AddWithValue("@pidc_almacen", vidc_almacen);
        cmd.Parameters.AddWithValue("@pdsinexi", vdsinexi);
        cmd.Parameters.AddWithValue("@pdcosbajo", VDCOSBAJO);
        cmd.Parameters.AddWithValue("@PMENSAJE", VDETALLES);
        cmd.Parameters.AddWithValue("@pbitlla", vbitlla);
        cmd.Parameters.AddWithValue("@psipasa2", SIPASA2);
        cmd.Parameters.AddWithValue("@pflete", VFLETE);
        cmd.Parameters.AddWithValue("@ptipom ", "A");
        cmd.Parameters.AddWithValue("@pcambios", "");
        cmd.Parameters.AddWithValue("@pbitcro", vbitcro);
        cmd.Parameters.AddWithValue("@precoge", VRECOGE);
        cmd.Parameters.AddWithValue("@pidc_occli ", vidc_occli);
        cmd.Parameters.AddWithValue("@ptotart ", vtota);
        cmd.Parameters.AddWithValue("@pfolio", 0);
        cmd.Parameters.AddWithValue("@pocc", vocc);
        cmd.Parameters.AddWithValue("@pidc_folioprecp", vfoliocp);
        cmd.Parameters.AddWithValue("@pfecha_ent", vfechaent);
        cmd.Parameters.AddWithValue("@pproye", vpro);
        cmd.Parameters.AddWithValue("@pidpro ", vidpro);
        cmd.Parameters.AddWithValue("@pobs", vobs);
        cmd.Parameters.AddWithValue("@psipasa ", sipasa);
        cmd.Parameters.AddWithValue("@pidc_sucursal_recoge ", vidc_sucursal_recoge);
        cmd.Parameters.AddWithValue("@pidc_colonia ", vidcc);
        cmd.Parameters.AddWithValue("@pcalle ", vcalle);
        cmd.Parameters.AddWithValue("@pnumero ", vnumero);
        cmd.Parameters.AddWithValue("@pcod_postal ", vcp);
        cmd.Parameters.AddWithValue("@pentdir ", ventdir);
        cmd.Parameters.AddWithValue("@pbitocc ", VBITOCC);
        cmd.Parameters.AddWithValue("@pusuariopc ", "USUARIO");

        if (!ventrar.Equals(3))
        {
            cmd.Parameters.AddWithValue("@ptipo ", vtipog);
        }

        if (ventrar.Equals(4))
        {
            cmd.Parameters.AddWithValue("@pplazo ", vplazo);
            cmd.Parameters.AddWithValue("@ptipopago ", vtipopago);
            cmd.Parameters.AddWithValue("@potro ", votro);
            cmd.Parameters.AddWithValue("@pcanminima ", vcanminima);
            cmd.Parameters.AddWithValue("@pcontacto ", vcontacto);
            cmd.Parameters.AddWithValue("@ptelefono ", vtelefono);
            cmd.Parameters.AddWithValue("@pmail ", vmail);
        }

        if (vconfirmar_pago == true)
        {
            cmd.Parameters.AddWithValue("@vtipop ", VTIPOP);
            cmd.Parameters.AddWithValue("@pidc_banco ", vidc_banco);
            cmd.Parameters.AddWithValue("@pmonto_pago ", vmonto_pago);
            cmd.Parameters.AddWithValue("@pobservaciones_pago ", vobservaciones_pago);
            cmd.Parameters.AddWithValue("@pconfirmar_pago ", vconfirmar_pago);
            cmd.Parameters.AddWithValue("@pfecha_deposito ", vfecha_deposito);
        }

        if ((ventrar.Equals(2)) || (ventrar.Equals(3)))
        {
            cmd.Parameters.AddWithValue("@pdarti", vdarti2);
        }
        else
        {
            cmd.Parameters.AddWithValue("@pdarti", vdarti);
        }

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public string getFilePathByCode(string pcod_archivo)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_uni_archi", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@pcod_archivo", pcod_archivo);

        SqlDataReader reader = cmd.ExecuteReader();

        string path = "";

        if ((reader.HasRows))
        {
            while ((reader.Read()))
            {
                path = reader["unidad"].ToString();
            }
        }
        else
        {
            path = "error";
        }

        reader.Close();
        connection.Close();

        return path;
    }

    //public string getempleado_vehiculo(int vidc_vehiculo)
    //{
    //    connection.Open();

    //    SqlCommand cmd = new SqlCommand("sp_empleado_ayu", connection);
    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    //    cmd.Parameters.AddWithValue("@pidc_vehiculo", vidc_vehiculo);

    //    SqlDataReader reader = cmd.ExecuteReader();

    //    string path = "";

    //    if ((reader.HasRows))
    //    {
    //        while ((reader.Read()))
    //        {
    //            path = reader["chofer"].ToString();
    //        }
    //    }
    //    else
    //    {
    //        path = "error";
    //    }

    //    reader.Close();
    //    connection.Close();

    //    return path;
    //}

    #endregion "Funciones de Hallazgos"

    #region "Funciones Cliente x Agente"

    public SqlDataReader getClientDetails(string clientID)
    {
        SqlCommand command = new SqlCommand("sp_ver_fichacliente", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = Convert.ToInt32(clientID);

        return command.ExecuteReader();
    }

    public bool getLockedStatus(int clientID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_desbloqueado_hoy", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;
        SqlDataReader rdr = command.ExecuteReader();
        bool locked = false;
        if (rdr.HasRows)
        {
            while (rdr.Read())
                locked = Convert.ToBoolean(rdr["SI_NO"]);
        }
        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return locked;
    }

    public bool checkPaymentConfirmation(int clientID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_cliente_confirmacion_pago", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;
        SqlDataReader rdr = command.ExecuteReader();
        bool confirmation = false;
        if (rdr.HasRows)
        {
            while (rdr.Read())
                confirmation = Convert.ToBoolean(rdr["confirmacion"]);
        }
        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return confirmation;
    }

    public int calibrar_llantas(string vidc_vehiculo)
    {
        connection.Open();

        string query = "select gm.dbo.fn_calibrar_llantas(" + vidc_vehiculo + ") as calibrar";

        SqlCommand commandm = new SqlCommand(query, connection);
        commandm.CommandType = CommandType.Text;

        int calibrar = 0;

        SqlDataReader rdr3 = commandm.ExecuteReader();
        if (rdr3.HasRows)
            while (rdr3.Read())
                calibrar = Convert.ToInt32(rdr3["calibrar"]);

        rdr3.Close();
        rdr3.Dispose();
        connection.Close();

        return calibrar;
    }

    public decimal checkAuthorizedMoney(int clientID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_monto_autorizado", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;
        SqlDataReader rdr = command.ExecuteReader();
        decimal monto = 0;
        if (rdr.HasRows)
        {
            while (rdr.Read())
                if (rdr["AUTORIZADO"] != DBNull.Value)
                    monto = Convert.ToDecimal(rdr["AUTORIZADO"]);
        }
        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return monto;
    }

    public SqlDataReader getClientSaldos(string clientID)
    {
        SqlCommand command = new SqlCommand("select * from dbo.fn_saldos_ficha_cliente(" + clientID + " )", connection);
        command.CommandType = CommandType.Text;
        command.CommandTimeout = 10;
        return command.ExecuteReader();
    }

    public DataSet getClientContacts(string clientID)
    {
        connection.Open();

        string query = "SELECT TELEFONO,CONTACTO,NOMBRE,EMAIL,FECHA_CUMPLE,HOBIES,CELULAR,EQUIPO " +
            "FROM CLIENTES_TEL WHERE ACTIVO = 1 AND IDC_CLIENTE = " + clientID + " AND ISNULL(CONFIDENCIAL,0)=0 " +
            "ORDER BY CONTACTO";

        SqlCommand command = new SqlCommand(query, connection);
        command.CommandType = System.Data.CommandType.Text;
        command.CommandTimeout = 10;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);

        connection.Close();
        return ds;
    }

    public DataSet getClientDebt(string clientID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_ver_cuentasxcobrar", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_cliente", clientID);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);

        connection.Close();
        return ds;
    }

    public DataSet getClientInvoices(string clientID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_venxfac_arti_ult6", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_cliente", clientID);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);

        connection.Close();
        return ds;
    }

    #endregion "Funciones Cliente x Agente"

    #region "Funciones Pedidos"

    public decimal checkIfCountable(int itemID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_bgastos_chqseg", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_articulo", SqlDbType.Int)).Value = itemID;

        decimal porcentajes = 0;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
                porcentajes = Convert.ToDecimal(rdr["porcentaje"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return porcentajes;
    }

    public decimal obtenercosto(string itemID, string clientID, string branchID)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_precio_cliente_cedis", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_articulo", SqlDbType.Int)).Value = itemID;
        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = branchID;

        decimal costo = 0;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
                costo = Convert.ToDecimal(rdr["costo"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return costo;
    }

    public int CountableFieldCount()
    {
        connection.Open();

        SqlCommand command = new SqlCommand("select count(idc_articulo) as TotalPorcentajes from gastos_chqseg ", connection);
        command.CommandType = CommandType.Text;
        command.CommandTimeout = 10;

        int fieldCount = 0;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
                fieldCount = Convert.ToInt32(rdr["TotalPorcentajes"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return fieldCount;
    }

    public SqlDataReader searchForItemPrice(string itemID, string clientID, string branchID)
    {
        SqlCommand command = new SqlCommand("sp_precio_cliente_cedis", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_articulo", itemID);
        command.Parameters.AddWithValue("@pidc_cliente", clientID);
        command.Parameters.AddWithValue("@pidc_sucursal", branchID);

        return command.ExecuteReader();
    }

    public SqlDataReader searchForItem(string searchValue, int sucursalID, int userID)
    {
        SqlCommand command = new SqlCommand("sp_buscar_articulo_ventas_existencias", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 50;

        if (searchValue.EndsWith(":"))
            searchValue = searchValue.Replace(":", "");

        command.Parameters.Add(new SqlParameter("@pvalor", SqlDbType.VarChar)).Value = searchValue;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.SmallInt)).Value = sucursalID;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.SmallInt)).Value = userID;

        //Iterate through the possible types, returning (aborting) when something comes up

        char[] types = { 'A', 'D', 'C', 'B' };

        SqlDataReader rdr = null;
        SqlParameter sqlTipo = new SqlParameter("@ptipo", SqlDbType.Char);

        foreach (char c in types)
        {
            sqlTipo.Value = c;
            command.Parameters.Add(sqlTipo);
            rdr = command.ExecuteReader();
            if (rdr.HasRows)
                return rdr;
            else
            {
                rdr.Close();
                rdr.Dispose();
                command.Parameters.Remove(sqlTipo);
            }
        }

        //value not found
        return null;
    }

    public SqlDataReader searchforvehiculo(string searchValue)
    {
        SqlCommand command = new SqlCommand("selecciona_vehiculo", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 50;

        command.Parameters.Add(new SqlParameter("@pvalor", SqlDbType.VarChar)).Value = searchValue;

        SqlDataReader rdr = null;

        rdr = command.ExecuteReader();
        if (rdr.HasRows)
            return rdr;
        else
        {
            rdr.Close();
            rdr.Dispose();
            ;
        }

        //value not found
        return null;
    }

    //public DataSet getArticleDetails(int articleID, int storageID)
    //{
    //    connection.Open();
    //    SqlCommand command = new SqlCommand("sp_datos_articulo_web", connection);
    //    command.CommandType = CommandType.StoredProcedure;
    //    command.CommandTimeout = 10;

    //    command.Parameters.Add(new SqlParameter("@pidc_articulo", articleID));
    //    command.Parameters.Add(new SqlParameter("@pidc_almacen", storageID));

    //    SqlDataAdapter da = new SqlDataAdapter();
    //    da.SelectCommand = command;
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);

    //    connection.Close();
    //    return ds;
    //}

    public DataSet searchforvehiculo2(string searchValue)
    {
        SqlCommand command = new SqlCommand("selecciona_vehiculo", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 50;

        command.Parameters.Add(new SqlParameter("@pvalor", SqlDbType.VarChar)).Value = searchValue;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public SqlDataReader searchforvehiculo_id(int vidc_vehiculo)
    {
        SqlCommand command = new SqlCommand("sp_datos_vehiculo", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 50;

        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.VarChar)).Value = vidc_vehiculo;

        SqlDataReader rdr = null;

        rdr = command.ExecuteReader();
        if (rdr.HasRows)
            return rdr;
        else
        {
            rdr.Close();
            rdr.Dispose();
            ;
        }

        //value not found
        return null;
    }

    public bool checkForOCC(int clientID, string vocc)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_existe_occ", connection);
        command.CommandTimeout = 10;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@pidc_cliente", clientID));
        command.Parameters.Add(new SqlParameter("@pno_occ", vocc));

        SqlDataReader rdr = command.ExecuteReader();

        bool existe = false;
        if ((rdr.HasRows))
        {
            while ((rdr.Read()))
            {
                existe = Convert.ToBoolean(rdr["existe"]);
            }
        }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return existe;
    }

    public bool saveOrder(int clientID, int sucursalID, decimal monto, bool desIVA, int idIVA,
        int userID, int almacenID, string VC, int totalItems, string vocc, ref int vfolio)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_apedidos_web", connection);
        command.CommandTimeout = 10;
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.SmallInt)).Value = sucursalID;
        command.Parameters.Add(new SqlParameter("@pmonto", SqlDbType.Money)).Value = monto;
        command.Parameters.Add(new SqlParameter("@pdesiva", SqlDbType.Bit)).Value = desIVA;
        command.Parameters.Add(new SqlParameter("@pidc_iva", SqlDbType.TinyInt)).Value = idIVA;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.SmallInt)).Value = userID;
        command.Parameters.Add(new SqlParameter("@pidc_almacen", SqlDbType.SmallInt)).Value = almacenID;
        command.Parameters.Add(new SqlParameter("@pdarti", SqlDbType.VarChar)).Value = VC;
        command.Parameters.Add(new SqlParameter("@ptotart", SqlDbType.Int)).Value = totalItems;
        command.Parameters.Add(new SqlParameter("@pocc", SqlDbType.VarChar)).Value = vocc;

        SqlDataReader rdr = command.ExecuteReader();

        bool bien = false;
        if ((rdr.HasRows))
        {
            while ((rdr.Read()))
            {
                vfolio = Convert.ToInt32(rdr["folio"]);
                bien = (bool)rdr["bien"];
            }
        }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return bien;
    }

    public void saveOrderImage(string filePath, byte[] image, int fileLength, int folio)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("INSERT INTO Imagenes (nombre, length, imagen,id) " +
            "VALUES (@name, @length, @imagen,@vfolio)", connection);
        command.CommandTimeout = 10;
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@name", filePath);
        command.Parameters.AddWithValue("@length", fileLength);
        command.Parameters.AddWithValue("@vfolio", folio);
        command.Parameters.Add(new SqlParameter("@imagen", System.Data.SqlDbType.Image)).Value = image;

        command.ExecuteNonQuery();

        connection.Close();
    }

    public DataSet getArticleDetails(int articleID, int storageID)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_datos_articulo_web", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_articulo", articleID));
        command.Parameters.Add(new SqlParameter("@pidc_almacen", storageID));

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);

        connection.Close();
        return ds;
    }

    public bool checkItemPackage(int itemID, decimal count, ref decimal pckCount)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_arti_conv_int", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_articulo", SqlDbType.Int)).Value = itemID;
        command.Parameters.Add(new SqlParameter("@pcantidad", SqlDbType.Int)).Value = count;

        bool bien = false;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
            {
                if (!(bien = Convert.ToBoolean(rdr["pconv"])))
                    pckCount = Convert.ToDecimal(rdr["RCONVERSION"]);
            }
        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return bien;
    }

    public void getStockValues(int almacenID, int itemID, ref decimal stock, ref decimal shipping)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_bexistencia", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_articulo", SqlDbType.Int)).Value = itemID;
        command.Parameters.Add(new SqlParameter("@pidc_almacen", SqlDbType.Int)).Value = almacenID;
        command.Parameters.Add(new SqlParameter("@PEXIF", SqlDbType.Int)).Value = 0;

        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
            {
                stock = Convert.ToDecimal(rdr["EXISTENCIA"]);
                shipping = Convert.ToDecimal(rdr["EMBARQUE"]);
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();
    }

    public bool checkForNote(int clientID, int itemID, ref decimal precio_real)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("SP_nc_auto_CLIENTE_articulo", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;
        command.Parameters.Add(new SqlParameter("@pidc_articulo", SqlDbType.Int)).Value = itemID;

        bool note = false;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
            {
                precio_real = Convert.ToDecimal(rdr["precio_real"]);
                note = true;
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return note;
    }

    public bool checkrevelemento_unidad(int vidc_elerev, int vidc_usuario, int vidc_sucursal, int vidc_vehiculo)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_elemento_revsuc_unidad", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_elerev", SqlDbType.Int)).Value = vidc_elerev;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;
        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        bool revisado = false;

        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
            {
                revisado = Convert.ToBoolean(rdr["revisado"]);
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return revisado;
    }

    public int revelemento_unidad_idc(int vidc_elerev, int vidc_usuario, int vidc_sucursal, int vidc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_elemento_revsuc_unidad", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_elerev", SqlDbType.Int)).Value = vidc_elerev;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;
        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        int idc_revsuccheckdv = 0;

        SqlDataReader rdr = command.ExecuteReader();

        if (rdr.HasRows)
            while (rdr.Read())
            {
                idc_revsuccheckdv = Convert.ToInt32(rdr["idc_revsuccheckdv"]);
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return idc_revsuccheckdv;
    }

    public SqlDataReader getClientHistory(int clientID)
    {
        SqlCommand command = new SqlCommand("sp_selecciona_consignado_cliente", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;

        return command.ExecuteReader();
    }

    public string[] getClientDF(int clientID)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_consignado_df", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;

        SqlDataReader rdr = command.ExecuteReader();
        string[] dasParams = new string[8];
        if (rdr.HasRows)
            while (rdr.Read())
            {
                dasParams[0] = rdr["idc_colonia"].ToString().Trim();
                dasParams[1] = rdr["calle"].ToString().Trim();
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return dasParams;
    }

    public string[] getelemento_rev(int vidc_elerev, int vidc_usuario, int vidc_sucursal)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_elemento_revsuc", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pidc_elerev", SqlDbType.Int)).Value = vidc_elerev;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;

        SqlDataReader rdr7 = command.ExecuteReader();
        string[] dasParams7 = new string[8];
        if (rdr7.HasRows)
            while (rdr7.Read())
            {
                dasParams7[0] = rdr7["elemento"].ToString();
                dasParams7[1] = rdr7["grupo"].ToString();
                dasParams7[2] = rdr7["revisado"].ToString();
                dasParams7[3] = rdr7["bien"].ToString();
                dasParams7[4] = rdr7["observaciones"].ToString();
                dasParams7[5] = rdr7["aplica_solucion"].ToString();
                dasParams7[6] = rdr7["idc_revsuccheckd"].ToString();
            }

        rdr7.Close();
        rdr7.Dispose();
        connection.Close();

        return dasParams7;
    }

    public string[] getelemento_rev_unidades(int vidc_elerev, int vidc_usuario, int vidc_sucursal, int vidc_vehiculo)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_elemento_revsuc_unidad", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pidc_elerev", SqlDbType.Int)).Value = vidc_elerev;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;
        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        SqlDataReader rdr7 = command.ExecuteReader();
        string[] dasParams7 = new string[8];
        if (rdr7.HasRows)
            while (rdr7.Read())
            {
                dasParams7[0] = rdr7["elemento"].ToString();
                dasParams7[1] = rdr7["grupo"].ToString();
                dasParams7[2] = rdr7["revisado"].ToString();
                dasParams7[3] = rdr7["bien"].ToString();
                dasParams7[4] = rdr7["observaciones"].ToString();
                dasParams7[5] = rdr7["aplica_solucion"].ToString();
                dasParams7[6] = rdr7["idc_revsuccheckdv"].ToString();
                dasParams7[7] = rdr7["tipo"].ToString();
            }

        rdr7.Close();
        rdr7.Dispose();
        connection.Close();

        return dasParams7;
    }

    public string[] getelemento_rev_patio(int vidc_elerev, int vidc_usuario, int vidc_sucursal, int vidc_modulo, int vidc_empleado)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_elemento_revsuc_patio", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;
        command.Parameters.Add(new SqlParameter("@pidc_elerev", SqlDbType.Int)).Value = vidc_elerev;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;
        command.Parameters.Add(new SqlParameter("@pidc_modulo", SqlDbType.Int)).Value = vidc_modulo;
        command.Parameters.Add(new SqlParameter("@pidc_empleado", SqlDbType.Int)).Value = vidc_empleado;

        SqlDataReader rdr7 = command.ExecuteReader();
        string[] dasParams7 = new string[8];
        if (rdr7.HasRows)
            while (rdr7.Read())
            {
                dasParams7[0] = rdr7["elemento"].ToString();
                dasParams7[1] = rdr7["grupo"].ToString();
                dasParams7[2] = rdr7["revisado"].ToString();
                dasParams7[3] = rdr7["bien"].ToString();
                dasParams7[4] = rdr7["observaciones"].ToString();
                dasParams7[5] = rdr7["aplica_solucion"].ToString();
                dasParams7[6] = rdr7["idc_revsuccheckdm"].ToString();
            }

        rdr7.Close();
        rdr7.Dispose();
        connection.Close();

        return dasParams7;
    }

    public string[] getelemento_rev_taller(int vidc_elerevtaller, int vidc_usuario, int vidc_vehiculo)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_elemento_rev_taller", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;
        command.Parameters.Add(new SqlParameter("@pidc_elerevtaller", SqlDbType.Int)).Value = vidc_elerevtaller;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        SqlDataReader rdr7 = command.ExecuteReader();
        string[] dasParams7 = new string[8];
        if (rdr7.HasRows)
            while (rdr7.Read())
            {
                dasParams7[0] = rdr7["elemento"].ToString();
                dasParams7[1] = rdr7["grupo"].ToString();
                dasParams7[2] = rdr7["revisado"].ToString();
                dasParams7[3] = rdr7["bien"].ToString();
                dasParams7[4] = rdr7["observaciones"].ToString();
                dasParams7[5] = rdr7["pendiente"].ToString();
                dasParams7[6] = rdr7["idc_revtallercheckd"].ToString();
            }

        rdr7.Close();
        rdr7.Dispose();
        connection.Close();

        return dasParams7;
    }

    public string[] getelemento_rev_taller_id(int vidc_revtallercheckd, int vidc_usuario, int vidc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_elemento_rev_taller_id", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;
        command.Parameters.Add(new SqlParameter("@pidc_revtallercheckd", SqlDbType.Int)).Value = vidc_revtallercheckd;

        SqlDataReader rdr7 = command.ExecuteReader();
        string[] dasParams7 = new string[8];
        if (rdr7.HasRows)
            while (rdr7.Read())
            {
                dasParams7[0] = rdr7["elemento"].ToString();
                dasParams7[1] = rdr7["grupo"].ToString();
                dasParams7[2] = rdr7["revisado"].ToString();
                dasParams7[3] = rdr7["bien"].ToString();
                dasParams7[4] = rdr7["observaciones"].ToString();
                dasParams7[5] = rdr7["pendiente"].ToString();
                dasParams7[6] = rdr7["idc_revtallercheckd"].ToString();
            }

        rdr7.Close();
        rdr7.Dispose();
        connection.Close();
        return dasParams7;
    }

    public string getnextfolio_pedidos()
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_folio_preped_pedidos", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        string folio = "";
        SqlDataReader rdr = command.ExecuteReader();

        if (rdr.HasRows)
            while (rdr.Read())
            {
                folio = Convert.ToString(rdr["no_folio"]);
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();
        return folio;
    }

    public string tiempo_dias_corto(int xdife)
    {
        //string tiempo_texto="";
        ////double xdifed=xdife;

        int tdias = Convert.ToInt32(xdife / 86400);
        ///double r1d=Math.IEEERemainder(xdifed,86400);
        int r1 = xdife % 86400; ////Convert.ToInt32(r1d);

        int thora = Convert.ToInt32(r1 / 3600);
        ///double r2d = Math.IEEERemainder(xdifed,3600);
        int r2 = Convert.ToInt32(xdife % 3600);
        int tmin = Convert.ToInt32(r2 / 60);

        ////double r3d = Math.IEEERemainder(r2d, 60);
        int r3 = Convert.ToInt32(r2 % 60);

        int tseg = r3;

        string xtiempo = "";

        if (tdias > 0)
        {
            xtiempo = tdias.ToString().Trim() + "d ";
        }

        if (thora > 0)
        {
            xtiempo = xtiempo + thora.ToString().Trim() + "h ";
        }

        if (tmin > 0)
        {
            xtiempo = xtiempo + tmin.ToString().Trim() + "m ";
        }

        if (tseg > 0)
        {
            xtiempo = xtiempo + tseg.ToString().Trim() + "s ";
        }

        return xtiempo;
    }

    public decimal getdisponible(int clientID)
    {
        connection.Open();

        SqlCommand command11 = new SqlCommand("sp_credito_disponible", connection);
        command11.CommandType = CommandType.StoredProcedure;
        command11.CommandTimeout = 20;
        command11.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;

        decimal disponible = 0;

        SqlDataReader rdr1 = command11.ExecuteReader();

        if (rdr1.HasRows)
            while (rdr1.Read())
            {
                disponible = Convert.ToDecimal(rdr1["disponible"]);
            }

        rdr1.Close();
        rdr1.Dispose();
        connection.Close();

        return disponible;
    }

    public string[] getruta(string vbus)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_uni_archi", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pcod_archivo", SqlDbType.Char)).Value = vbus;

        SqlDataReader rdr = command.ExecuteReader();
        string[] dasParams2 = new string[1];
        if (rdr.HasRows)
            while (rdr.Read())
            {
                dasParams2[0] = rdr["unidad"].ToString().Trim();
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return dasParams2;
    }

    public string[] getvehrev(int vidc_requerirveh)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_datos_idc_requerirveh", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pidc_requerirveh", SqlDbType.Int)).Value = vidc_requerirveh;

        SqlDataReader rdr = command.ExecuteReader();
        string[] dasParams3 = new string[3];
        if (rdr.HasRows)
            while (rdr.Read())
            {
                dasParams3[0] = rdr["idc_vehiculo"].ToString().Trim();
                dasParams3[1] = rdr["idc_formatorev"].ToString().Trim();
                dasParams3[2] = rdr["idc_empleado"].ToString().Trim();
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return dasParams3;
    }

    public string[] getvehrevbas(int vidc_revbasherr)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_datos_idc_revbasherr", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pidc_revbasherr", SqlDbType.Int)).Value = vidc_revbasherr;

        SqlDataReader rdr = command.ExecuteReader();
        string[] dasParams3 = new string[3];
        if (rdr.HasRows)
            while (rdr.Read())
            {
                dasParams3[0] = rdr["idc_vehiculo"].ToString().Trim();
                dasParams3[1] = rdr["num_economico"].ToString().Trim();
                dasParams3[2] = rdr["idc_empleado"].ToString().Trim();
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return dasParams3;
    }

    public string[] getempleado_vehiculo(int vidc_vehiculo)
    {
        connection.Open();

        SqlCommand command = new SqlCommand("sp_empleado_ayu_revsuc", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 15;

        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        SqlDataReader rdr = command.ExecuteReader();

        string[] dasParams3 = new string[2];
        if (rdr.HasRows)
            while (rdr.Read())
            {
                dasParams3[0] = rdr["idc_empleado"].ToString().Trim();
                dasParams3[1] = rdr["chofer"].ToString().Trim();
            }

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return dasParams3;
    }

    public SqlDataReader getelementos_revision(int vidc_vehiculo)

    {
        SqlCommand command = new SqlCommand("sp_elementos_revision_vehiculo", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        return command.ExecuteReader();
    }

    public SqlDataReader getherramientas_revision(int vidc_vehiculo)
    {
        SqlCommand command = new SqlCommand("sp_revision_herramientas_clasif", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        return command.ExecuteReader();
    }

    public SqlDataReader getherramientas_revision_basica(int vidc_vehiculo)
    {
        SqlCommand command = new SqlCommand("sp_revision_herramientas_clasif", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;
        command.Parameters.Add(new SqlParameter("@prevision_basica", SqlDbType.Bit)).Value = true;

        return command.ExecuteReader();
    }

    public SqlDataReader getelementos_revision_suc(int vidc_sucursal, int vidc_usuario)
    {
        SqlCommand command = new SqlCommand("sp_predatos_revsuc_check", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;

        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;

        return command.ExecuteReader();
    }

    public SqlDataReader getelementos_revision_taller(int vidc_vehiculo, int vidc_usuario)
    {
        SqlCommand command = new SqlCommand("sp_datos_revtaller_check", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        return command.ExecuteReader();
    }

    public SqlDataReader getrevision_herramientas(int vidc_revsuccheckdv)
    {
        SqlCommand command = new SqlCommand("sp_datos_revsuc_checkd_veh_revh", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_revsuccheckdv", SqlDbType.Int)).Value = vidc_revsuccheckdv;

        return command.ExecuteReader();
    }

    public SqlDataReader getelementos_revision_suc_unidades(int vidc_sucursal, int vidc_usuario, int vidc_vehiculo)
    {
        SqlCommand command = new SqlCommand("sp_prerevsuc_checkd_veh", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_vehiculo", SqlDbType.Int)).Value = vidc_vehiculo;

        return command.ExecuteReader();
    }

    public SqlDataReader getelementos_revision_suc_patio(int vidc_sucursal, int vidc_usuario, int vidc_modulo, int vidc_empleado)
    {
        SqlCommand command = new SqlCommand("sp_prerevsuc_checkd_mod", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_sucursal", SqlDbType.Int)).Value = vidc_sucursal;
        command.Parameters.Add(new SqlParameter("@pidc_usuario", SqlDbType.Int)).Value = vidc_usuario;
        command.Parameters.Add(new SqlParameter("@pidc_modulo", SqlDbType.Int)).Value = vidc_modulo;
        command.Parameters.Add(new SqlParameter("@pidc_empleado", SqlDbType.Int)).Value = vidc_empleado;

        return command.ExecuteReader();
    }

    public SqlDataReader getColonias(string searchValue)
    {
        SqlCommand command = new SqlCommand("sp_bcolonias", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pvalor", SqlDbType.VarChar)).Value = searchValue;

        return command.ExecuteReader();
    }

    public string verifyCheckplus(int number, decimal amount, int clientID)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_comprobar_chekplus_PRE", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_folioprecp", SqlDbType.Int)).Value = number;
        command.Parameters.Add(new SqlParameter("@PMONTO", SqlDbType.Decimal)).Value = amount;
        command.Parameters.Add(new SqlParameter("@pidc_cliente", SqlDbType.Int)).Value = clientID;

        SqlDataReader rdr = command.ExecuteReader();
        string message = "";

        if (rdr.HasRows)
            while (rdr.Read())
                message = rdr["mensaje"].ToString();

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return message;
    }

    public SqlDataReader getBancos()
    {
        SqlCommand command = new SqlCommand("sp_combo_bancos", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        return command.ExecuteReader();
    }

    public decimal getFleteAmount(string cadena, string total, string truckID, string sucursalID, string coloniaID,
        string clientID, string desgloseIVA, string IVA, string cadena2)
    {
        connection.Open();

        string query = "select gm.dbo.fn_cadenas_fletes_preped(";
        query += "'" + cadena + "',";
        query += total + ",";
        query += truckID + ",";
        query += sucursalID + ",";
        query += coloniaID + ",";
        query += clientID + ",";
        query += "'" + desgloseIVA + "',";
        query += IVA + ",";
        query += "'" + cadena2 + "'";
        query += ") as flete";

        SqlCommand command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;
        decimal flete = 0;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
                flete = Convert.ToDecimal(rdr["flete"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return flete;
    }

    public bool checar_cambio_precios(string VDARTI, string VTOTA, string PIDC_CLIENTE, string PIDC_SUCURSAL)
    {
        connection.Open();

        string query = "select gm.dbo.FN_CAMBIO_PRECIOS(";
        query += "'" + VDARTI + "',";
        query += VTOTA + ",";
        query += PIDC_CLIENTE + ",";
        query += PIDC_SUCURSAL;
        query += ") as cambio";

        SqlCommand command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;

        bool cambio = false;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
                cambio = Convert.ToBoolean(rdr["cambio"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return cambio;
    }

    public bool cliente_bloqueado(string PIDC_CLIENTE)
    {
        connection.Open();

        string query = "select gm.dbo.fn_cliente_bloqueado(" + PIDC_CLIENTE + ") as bloqueado";

        SqlCommand command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;

        bool bloqueado = false;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())
                bloqueado = Convert.ToBoolean(rdr["bloqueado"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return bloqueado;
    }

    public bool tiene_revtaller(string pidc_vehiculo, string pidc_usuario)
    {
        connection.Open();

        string query = "select gm.dbo.fn_tiene_revtaller(" + pidc_vehiculo + "," + pidc_usuario + ") as tiene";

        SqlCommand command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;

        bool tiene = false;
        SqlDataReader rdr = command.ExecuteReader();
        if (rdr.HasRows)
            while (rdr.Read())

                tiene = Convert.ToBoolean(rdr["tiene"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return tiene;
    }

    public int id_revtaller(string pidc_vehiculo, string pidc_usuario)
    {
        connection.Open();
        string query = "select gm.dbo.fn_revtaller_id(" + pidc_vehiculo + "," + pidc_usuario + ") as idc_revtallercheck";

        SqlCommand command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;

        int idc_revtallercheck = 0;
        SqlDataReader rdr = command.ExecuteReader();

        if (rdr.HasRows)
            while (rdr.Read())
                idc_revtallercheck = Convert.ToInt32(rdr["idc_revtallercheck"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return idc_revtallercheck;
    }

    public bool rev_taller_continuar(string vidc_vehiculo, string vidc_usuario)
    {
        connection.Open();

        string query = "select gm.dbo.fn_vrevtaller_check(" + vidc_usuario + "," + vidc_vehiculo + ") as continuar";

        SqlCommand command = new SqlCommand(query, connection);

        command.CommandType = CommandType.Text;

        bool continuar = false;

        SqlDataReader rdr = command.ExecuteReader();

        if (rdr.HasRows)
            while (rdr.Read())
                continuar = Convert.ToBoolean(rdr["continuar"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return continuar;
    }

    public string usuario_empleado(string vidc_usuario)
    {
        connection.Open();

        string query = "select gm.dbo.FN_USUARIO_EMPLEADO(" + vidc_usuario + ") as empleado";

        SqlCommand command = new SqlCommand(query, connection);

        command.CommandType = CommandType.Text;

        string empleado = "";

        SqlDataReader rdr = command.ExecuteReader();

        if (rdr.HasRows)
            while (rdr.Read())
                empleado = Convert.ToString(rdr["empleado"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return empleado;
    }

    public string GetIPAddress(string HttpVia, string HttpXForwardedFor, string RemoteAddr)
    {
        // Use a default address if all else fails.
        string result = "127.0.0.2";

        // Web user - if using proxy

        string tempIP = string.Empty;

        if (HttpVia != null)
            tempIP = HttpXForwardedFor;
        else // Web user - not using proxy or can't get the Client IP
            tempIP = RemoteAddr;

        // If we can't get a V4 IP from the above, try host address list for internal users.

        if (!IsIPV4(tempIP))
        {
            try
            {
                string hostName = System.Net.Dns.GetHostName();

                foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostAddresses(hostName))
                {
                    if (IsIPV4(ip))
                    {
                        result = ip.ToString();
                        break;
                    }
                }
            }
            catch { }
        }
        return result;
    }

    public bool IsIPV4(string input)
    {
        bool result = false;

        System.Net.IPAddress address = null;

        if (System.Net.IPAddress.TryParse(input, out address))

            result = IsIPV4(address);

        return result;
    }

    public bool IsIPV4(System.Net.IPAddress address)
    {
        bool result = false;

        switch (address.AddressFamily)
        {
            case System.Net.Sockets.AddressFamily.InterNetwork:   // we have IPv4

                result = true;

                break;

            case System.Net.Sockets.AddressFamily.InterNetworkV6: // we have IPv6

                break;

            default:

                break;
        }

        return result;
    }

    public bool pasa_limite(string PIDC_CLIENTE, string total)
    {
        connection.Open();

        string query = "select gm.dbo.fn_saldo_total_cliente(" + PIDC_CLIENTE + "," + total + ") as pasa_limite";

        SqlCommand commandm = new SqlCommand(query, connection);
        commandm.CommandType = CommandType.Text;

        bool pasa_limite = true;

        SqlDataReader rdr3 = commandm.ExecuteReader();
        if (rdr3.HasRows)
            while (rdr3.Read())
                pasa_limite = Convert.ToBoolean(rdr3["pasa_limite"]) == false ? true : false;

        rdr3.Close();
        rdr3.Dispose();
        connection.Close();

        return pasa_limite;
    }

    public SqlDataReader verifyFolio(int folio, int AuthType)
    {
        SqlCommand command = new SqlCommand("sp_checar_folio_autorizacion", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_tipo_aut", AuthType));
        command.Parameters.Add(new SqlParameter("@pFOLIO", folio));

        return command.ExecuteReader();
    }

    public bool getCargoCheque()
    {
        connection.Open();
        SqlCommand command = new SqlCommand("select cargo_cheque from cargo_chekplus", connection);
        command.CommandType = CommandType.Text;
        SqlDataReader rdr = command.ExecuteReader();

        bool cargo = false;

        if (rdr.HasRows)
            while (rdr.Read())
                cargo = Convert.ToBoolean(rdr["cargo_cheque"]);

        rdr.Close();
        rdr.Dispose();
        connection.Close();

        return cargo;
    }

    public SqlDataReader checarCambioIVA(int sucursalID, int coloniaID)
    {
        SqlCommand command = new SqlCommand("sp_cambiar_iva_frontera", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;

        command.Parameters.Add(new SqlParameter("@pidc_sucursal", sucursalID));
        command.Parameters.Add(new SqlParameter("@pidc_colonia", coloniaID));

        SqlDataReader rdr = command.ExecuteReader();
        return rdr;
    }

    #endregion "Funciones Pedidos"

    #region "Misc Functions"

    public string getConnectionString()
    {
        str = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["strDeConexion"]);
        return str;
    }

    #endregion "Misc Functions"

    public DataSet Cargar_Combustible_Folio(int idc_tabla)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_busca_folio", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@ptabla", idc_tabla);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Buscar_Vehiculos(int idc_vehiculo, Boolean pweb)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_selecciona_vehiculos_todos", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@idc_vehiculo", idc_vehiculo);
        command.Parameters.AddWithValue("@pweb", pweb);
        //command.Parameters.AddWithValue("", idc_tabla);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        //System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Buscar_Chofer(int idc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_empleado_ayu", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_vehiculo", idc_vehiculo);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Cargar_Combo_combustible()
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_tipo_combustible", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        // command.Parameters.AddWithValue("@pidc_vehiculo", idc_vehiculo);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Vehiculo_tipo_combustible(int idc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandTimeout = 10;
        command.CommandText = "select dbo.fn_vehiculo_tcomustible(" + idc_vehiculo + ")";
        SqlDataAdapter da = new SqlDataAdapter();
        //da.SelectCommand = command;
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataTable Datos(string consulta)
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            command.CommandText = consulta;
            SqlDataReader da;
            da = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(da);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            connection.Close();
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet Buscar_Chofer_Vehiculo(string valor)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_selecciona_empleado", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pvalor", valor);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Kilometraje_anterior(int idc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_ver_km_anterior", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_vehiculo", idc_vehiculo);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Datos_Tanque_Vehiculo(int idc_vehiculo)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_vehiculos_tanque_datos", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_vehiculo", idc_vehiculo);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet Tanque_Sucursal(int idc_usuario)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_combo_tanques_combustible_usu", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_usuario", idc_usuario);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataSet DescripcionAutorizacion(int TipoAutorizacion)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_descripcion_AUTORIZACION", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_tipo_aut", TipoAutorizacion);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        connection.Close();
        return ds;
    }

    public DataRow Validar_Folio_Aut(int tipoFolio, int Folio)
    {
        connection.Open();
        SqlCommand command = new SqlCommand("sp_checar_folio_AUTORIZACION", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.CommandTimeout = 10;
        command.Parameters.AddWithValue("@pidc_tipo_aut", tipoFolio);
        command.Parameters.AddWithValue("@pFOLIO", Folio);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataRow row = null;
        if (ds.Tables.Count > 0)
        {
            row = ds.Tables[0].Rows[0];
        }
        connection.Close();
        return row;
    }

    public DataSet Guardar_Carga_Combustible(string[] parametros, object[] valores)
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("sp_acarga_combustible3", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 10;
            if (parametros.Length > 0)
            {
                for (int i = 0; i <= (parametros.Length - 1); i++)
                {
                    command.Parameters.AddWithValue(parametros[i], valores[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    public DataSet Leer_avisos_nuevos(int idc_usuario)
    {
        try
        {
            connection.Open();
            SqlCommand comando = new SqlCommand("sp_avisos_nuevo", connection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandTimeout = 10;
            comando.Parameters.AddWithValue("@pidc_usuario", idc_usuario);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comando;
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet Verificar_Avisos_Nuevos(int idc_usuario)
    {
        try
        {
            connection.Open();
            SqlCommand comando = new SqlCommand("sp_checa_avisos_nuevos", connection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandTimeout = 10;
            comando.Parameters.AddWithValue("@pidc_usuario", idc_usuario);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comando;
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet Ejecuta_SP(string sp, string[] parametros, object[] valores)
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 60000;
            if (parametros != null)
            {
                for (int i = 0; i <= (parametros.Length - 1); i++)
                {
                    command.Parameters.AddWithValue(parametros[i], valores[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (Exception ex)
        {
            connection.Close();
            throw ex;
        }
    }

    public void Enviar_Correo(MailMessage correo, int idc_usuario, int tipo)
    {
        DataSet ds = new DataSet();
        string[] parametros = { "@pidc_usuario", "@PTIPO" };
        object[] valores = { idc_usuario, tipo };
        string nombre_mostrar = "";
        string cuenta = "";
        string contraseña = "";
        int puerto = 0;
        try
        {
            ds = Ejecuta_SP("sp_correo_contraseña", parametros, valores);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nombre_mostrar = Convert.ToString(ds.Tables[0].Rows[0][2]);
                cuenta = Convert.ToString(ds.Tables[0].Rows[0][0]);
                contraseña = desencripta(Convert.ToString(ds.Tables[0].Rows[0][1]));
            }
            else
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    nombre_mostrar = Convert.ToString(ds.Tables[1].Rows[0][2]);
                    cuenta = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    contraseña = desencripta(Convert.ToString(ds.Tables[1].Rows[0][1]));
                }
            }

            if (cuenta != "")
            {
                puerto = correo_puerto();
                correo.From = new MailAddress(cuenta, nombre_mostrar, System.Text.Encoding.UTF8);
                correo.Bcc.Add("sistemas@gamamateriales.com.mx," + cuenta);
                correo.IsBodyHtml = true;
                NetworkCredential BasicAuthenticationInfo = new NetworkCredential(cuenta, contraseña);
                SmtpClient smtp = new SmtpClient("smtp.gamamateriales.com.mx");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = BasicAuthenticationInfo;
                smtp.Port = puerto;
                smtp.EnableSsl = false;
                smtp.Timeout = 500000;
                smtp.Send(correo);
                correo.Attachments.Dispose();
                correo.Dispose();
                correo = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void enviar_correo_local(MailMessage correo, int idc_sucursal)
    {
        string mail_suc = correo_suc(idc_sucursal);
        try
        {
            correo.To.Add(new MailAddress(mail_suc)); // destino
            correo.To.Add(new MailAddress("insumos@gamamateriales.mx"));
            correo.From = new MailAddress("administrador@gamamateriales.mx", "administrador", System.Text.Encoding.UTF8); // origen
            correo.Bcc.Add("gersistemas@gamamateriales.mx");
            SmtpClient smtp = new SmtpClient("192.168.0.2");
            smtp.Send(correo);
            correo.Attachments.Dispose();
            correo.Dispose();
            correo = null;
        }
        catch (Exception ex) { throw ex; }
    }

    public void enviar_correo_local_usuario(MailMessage correo, int idc_usuario)
    {
        string mail_int = correo_usu(idc_usuario);

        if (mail_int == "")
        {
            throw new Exception("El Usuario no Tiene Cuenta de Correo.");
        }

        try
        {
            correo.To.Add(new MailAddress(mail_int));
            correo.From = new MailAddress(mail_int, mail_int, System.Text.Encoding.UTF8);
            correo.Bcc.Add("gersistemas@gamamateriales.mx");
            SmtpClient smtp = new SmtpClient("192.168.0.2");
            smtp.Send(correo);
            correo.Attachments.Dispose();
            correo.Dispose();
            correo = null;
        }
        catch (Exception ex) { throw ex; }
    }

    public string correo_usu(int idc_usuario)
    {
        DataTable dt = new DataTable();
        //string mail_int = "";
        try
        {
            dt = Datos("Select mail_int from usuarios where idc_usuario =" + Convert.ToString(idc_usuario));
            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0][0]);
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string correo_suc(int idc_sucursal)
    {
        string[] parametros = { "@pidc_sucursal" };
        object[] valores = { idc_sucursal };
        DataSet ds = new DataSet();
        string mail = "";
        try
        {
            ds = Ejecuta_SP("sp_correo_vale_tablet", parametros, valores);
            mail = Convert.ToString(ds.Tables[0].Rows[0][0]);
            return mail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int correo_puerto()
    {
        DataTable dt = new DataTable();
        int puerto = 0;
        try
        {
            dt = Datos("SELECT TOP 1 puerto FROM correo_puerto WITH (nolock)");
            if (dt.Rows.Count > 0)
            {
                puerto = Convert.ToInt32(dt.Rows[0][0]);
            }

            return puerto;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string desencripta(string contra)
    {
        DataSet ds = new DataSet();
        string vclave2 = "";
        string vclave1 = "";
        string vcar = null;
        int vbus = 0;
        string vncar = "";
        try
        {
            ds = des_encripta();

            if (ds.Tables[0].Rows.Count > 0)
            {
                vclave2 = Convert.ToString(ds.Tables[0].Rows[0][0]);
                vclave1 = Convert.ToString(ds.Tables[0].Rows[0][1]);
            }
            for (int i = 0; i <= (contra.Length) - 1; i++)
            {
                vcar = contra.Substring(i, 1);
                vbus = vclave1.IndexOf(vcar);
                vncar = vncar + vclave2.Substring(vbus, 1);
            }
            return vncar.Trim();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet des_encripta()
    {
        try
        {
            return Ejecuta_SP("sp_encripta", null, null);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable menu_submenu(int userID, int[] opciones_menu)
    {
        DataTable dt = new DataTable();
        DataRow row = dt.NewRow();
        dt.Columns.Add("idc_opcion");
        dt.Columns.Add("descripcion");
        dt.Columns.Add("web_form");
        connection.Open();
        foreach (int c in opciones_menu)
        {
            SqlCommand command = new SqlCommand("select opciones.idc_opcion,opciones.descripcion,opciones.web_form from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" + c.ToString() + " and opciones_usuarios.idc_usuario=" + userID.ToString() + " and opciones_usuarios.activo=1 and opciones.activo = 1", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //opciones[i++] = Convert.ToInt32(reader["idc_opcion"]);
                    row = dt.NewRow();
                    row[0] = 0;
                    row[0] = Convert.ToInt32(reader[0]);
                    row[1] = Convert.ToString(reader[1]);
                    row[2] = Convert.ToString(reader[2]);
                    dt.Rows.Add(row);
                }
            }

            //        row["descripcion"] = reader["descripcion"];
            //        row["web_form"] = reader["web_form"];
            //row = reader[0];

            reader.Close();
            reader.Dispose();
        }
        connection.Close();
        return dt;
    }

    public String time_long(int minutos)
    {
        try
        {
            string horas = "";
            string dias = "";
            string[] hrs_min;
            string[] hrs_dias;

            horas = Convert.ToString(minutos / 60);
            //hrs_min = horas.Split(new char[] { "." });
            hrs_min = horas.Split(new char[] { '.' });
            if (hrs_min.Length > 0)
            {
                horas = Convert.ToString(hrs_min[0]);
            }
            minutos = minutos % 60;
            if (Convert.ToInt16(horas) >= 24)
            {
                dias = Convert.ToString(Convert.ToInt32(horas) / 24);
                hrs_dias = dias.Split(new char[] { '.' });
                if (hrs_dias.Length > 0)
                {
                    dias = hrs_dias[0];
                    horas = Convert.ToString(Convert.ToInt32(horas) % 24);
                }
            }
            if (dias == "")
            {
                dias = "0";
            }
            if (horas == "")
            {
                horas = "0";
            }
            string tiempo = "";
            tiempo = Convert.ToString(Convert.ToInt32(dias) > 0 ? (Convert.ToInt32(dias) == 1 ? dias + " Dia " : dias + " Dias ") : "");
            tiempo = tiempo + Convert.ToString(Convert.ToInt32(horas) > 0 ? (Convert.ToInt32(horas) < 10 ? "0" + horas + " Hrs. " : horas + " Hrs. ") : "");
            tiempo = tiempo + Convert.ToString(Convert.ToInt32(minutos) > 0 ? (Convert.ToInt32(minutos) < 10 ? "0" + minutos + " Min. " : minutos + " Min. ") : "");
            return tiempo;
        }
        catch
        {
            Exception ex = new Exception("Error al Calcular Tiempo Tiempo.");
            throw (ex);
        }
    }

    public void Ejecuta_SP(string p)
    {
        throw new NotImplementedException();
    }
}