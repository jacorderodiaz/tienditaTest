using System;
using System.Linq;
using Tiendita.Models;
using Tiendita.utils;

namespace Tiendita
{
    class Program
    {
        static void Main(string[] args)
        {
            Inicio();
        }

        public static void Inicio()
        {
            Console.WriteLine("---Menu----");
            Console.WriteLine("1) Crear usuario");
            Console.WriteLine("2) Entar");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();

            if (opcion == "1" || opcion == "2" || opcion == "0")
            {
                switch (opcion)
                {
                    case "1":
                        CrearUsuario();
                        break;
                    case "2":
                        Login();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ingresa un numero valido");
                Inicio();
            }


        }
        public static void InicioUsuarioExistente()
        {
            Console.WriteLine("Ya existe este usuario.");
            Console.WriteLine("1) Crear usuario");
            Console.WriteLine("2) Entar");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();

            if (opcion == "1" || opcion == "2" || opcion == "0")
            {
                switch (opcion)
                {
                    case "1":
                        CrearUsuario();
                        break;
                    case "2":
                        Login();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ingresa un numero valido");
                Inicio();
            }
        }
        public static void datosInvalidosMenu()
        {
            Console.WriteLine("Datos invalidos ¿Quiere intentar de nuevo?");
            Console.WriteLine("1) Intentar de nuevo");
            Console.WriteLine("2) Ir al menu");


            string opcion = Console.ReadLine();

            if (opcion == "1" || opcion == "2" )
            {
                switch (opcion)
                {
                    case "1":
                        Console.Clear();

                        Login();
                        break;
                    case "2":
                        Console.Clear();

                        Inicio();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ingresa un numero valido");
                Inicio();
            }
        }

            public static void Login()
        {
            Console.WriteLine("Escriba el correo");
            string correo = Console.ReadLine();
            if (correo == "")
            {
                Console.Clear();

                datosInvalidosMenu();
            }
            Console.WriteLine("Escriba la cotraseña");
            string contrasena = Console.ReadLine();
            if (contrasena=="")
            {
                Console.Clear();
                datosInvalidosMenu();
            }

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Usuario> usuarios = context.Usuarios.Where(p => p.Correo.Contains(correo));
                foreach (Usuario usuario in usuarios)
                {
                    usuario.Contrasena = Seguridad.DesEncriptar(usuario.Contrasena);
                    if (usuario.Correo == correo && usuario.Contrasena == contrasena)
                    {
                        Console.Clear();
                        Menu();
                    }else
                    {
                        Console.Clear();
                        Console.WriteLine("Intenta de nuevo");
                        Login();
                    }
                }
            }
        }
        public static void CrearUsuario()
        {
            Console.WriteLine("Crear usuario");
            Usuario usuario = new Usuario();
            usuario = LlenarUsuario(usuario);
            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Usuario> usuarios = context.Usuarios.Where(p=>p.Correo.Contains(usuario.Correo));
                foreach (Usuario usu in usuarios)
                {
                    if (usuario.Correo == usu.Correo)
                    {
                        Console.Clear();
                        InicioUsuarioExistente();
                    }
                }

                context.Add(usuario);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine("Usuario creado");
                Menu();
            }
        }
        public static void LlenarUsuarioInvalido()
        {
            Console.WriteLine("Datos invalidos ¿Quiere intentar de nuevo?");
            Console.WriteLine("1) Intentar de nuevo");
            Console.WriteLine("2) Ir al menu");


            string opcion = Console.ReadLine();

            if (opcion == "1" || opcion == "2")
            {
                switch (opcion)
                {
                    case "1":
                        Console.Clear();

                        CrearUsuario();
                        break;
                    case "2":
                        Console.Clear();

                        Inicio();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ingresa un numero valido");
                Inicio();
            }
        }

            public static Usuario LlenarUsuario (Usuario usuario)
        {
            Console.Write("Correo: ");
            string cor = usuario.Correo = Console.ReadLine();
            if (EmailUtils.IsValidEmail(cor))
            {
                Console.WriteLine("Correo valido");
            }
            else
            {
                Console.Clear();
                LlenarUsuarioInvalido();
            }
            Console.WriteLine("Escriba una contraseña con al menos un numero y minimo 8 caracteres");
            Console.Write("Contraseña: ");
           string c = usuario.Contrasena = Console.ReadLine();
            if (ContraseñaUtils.IsValiPassword(c) == true)
            {
                Console.Clear();
                LlenarUsuarioInvalido();
            }
            usuario.Contrasena = Seguridad.Encriptar(usuario.Contrasena);
            return usuario;

        }

        public static void Menu()
        {

            Console.WriteLine("Menu de productos");
            Console.WriteLine("1) Buscar producto");
            Console.WriteLine("2) Crear producto");
            Console.WriteLine("3) Actualizar producto");
            Console.WriteLine("4) Eliminar producto");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();
            if (opcion == "1" || opcion == "2" || opcion == "3" || opcion == "4" || opcion == "0")
            {
                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        BuscarProductos();
                        break;
                    case "2":
                        Console.Clear();
                        CrearProducto();
                        break;
                    case "3":
                        Console.Clear();
                        ActualizarProducto();
                        break;
                    case "4":
                        Console.Clear();
                        EliminarProducto();
                        break;
                    case "0":
                        Console.Clear();
                        Inicio();
                        break;
                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ingresa un numero valido");
                Menu();
            }

        }

        public static void BuscarProductos()
        {
            Console.WriteLine("Buscar prodcutos");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));
                foreach (Producto producto in productos)
                {
                    Console.WriteLine(producto);
                    Menu();   
                }
            }
        }

        public static void FiltrarProductos()
        {
            Console.WriteLine("Buscar prodcutos");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));
                foreach (Producto producto in productos)
                {
                    Console.WriteLine(producto);

                }
            }
        }

        public static void CrearProducto()
        {
            Console.WriteLine("Crear producto");
            Producto producto = new Producto();
            producto = LlenarProducto(producto);

            using (TienditaContext context = new TienditaContext())
            {
                context.Add(producto);
                context.SaveChanges();
                Console.WriteLine("Producto creado");
                Menu();

            }
        }

        public static Producto LlenarProducto(Producto producto)
        {
            Console.Write("Nombre: ");
            producto.Nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            producto.Descripcion = Console.ReadLine();
            Console.Write("Precio: ");
            producto.Precio = decimal.Parse(Console.ReadLine());
            Console.Write("Costo: ");
            producto.Costo = decimal.Parse(Console.ReadLine());
            Console.Write("Cantidad: ");
            producto.Cantidad = decimal.Parse(Console.ReadLine());
            Console.Write("Tamaño: ");
            producto.Tamano = Console.ReadLine();
            Console.Clear();
            return producto;
        }

        public static Producto SelecionarProducto()
        {
            FiltrarProductos();
            Console.Write("Seleciona el código de producto: ");
            uint id = uint.Parse(Console.ReadLine());
            using (TienditaContext context = new TienditaContext())
            {
                Producto producto = context.Productos.Find(id);
                if (producto == null)
                {
                    SelecionarProducto();
                }
                return producto;
            }
        }

        public static void ActualizarProducto()
        {
            Console.WriteLine("Actualizar producto");
            Producto producto = SelecionarProducto();
            producto = LlenarProducto(producto);
            using (TienditaContext context = new TienditaContext())
            {
                context.Update(producto);
                context.SaveChanges();
                Console.WriteLine("Producto actualizado");
                Menu();

            }
        }

        public static void EliminarProducto()
        {
            Console.WriteLine("Eliminar producto");
            Producto producto = SelecionarProducto();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(producto);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine("Producto eliminado");
                Menu();
            }
        }
    }

}
