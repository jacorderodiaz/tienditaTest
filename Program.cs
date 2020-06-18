using System;
using System.Linq;
using Tiendita.Models;

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

        public static void Login()
        {
            Console.WriteLine("Escriba el correo");
            string correo = Console.ReadLine();
            Console.WriteLine("Escriba la cotraseña");
            string contrasena = Console.ReadLine();

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Usuario> usuarios = context.Usuarios.Where(p => p.Correo.Contains(correo));
                foreach (Usuario usuario in usuarios)
                {
                    usuario.Contrasena = Seguridad.DesEncriptar(usuario.Contrasena);
                    Console.WriteLine(usuario.Contrasena);
                    if (usuario.Correo == correo && usuario.Contrasena == contrasena)
                    {
                        Menu();
                    }else
                    {
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
                        Console.WriteLine("Ya existe este usuario.");
                        CrearUsuario();
                    }
                }
                Console.WriteLine(usuario.Contrasena);
                context.Add(usuario);
                context.SaveChanges();
                Console.WriteLine("Usuario creado");
                Menu();
            }
        }

        public static Usuario LlenarUsuario (Usuario usuario)
        {
            Console.Write("Correo: ");
            usuario.Correo = Console.ReadLine();
            Console.Write("Contraseña: ");
            usuario.Contrasena = Console.ReadLine();
            usuario.Contrasena = Seguridad.Encriptar(usuario.Contrasena);
            Console.WriteLine(usuario.Contrasena);
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

            return producto;
        }

        public static Producto SelecionarProducto()
        {
            BuscarProductos();
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
                Console.WriteLine("Producto eliminado");
                Console.Clear();

            }
        }
    }

    public static class Seguridad
    {
        public static string Encriptar(this string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string DesEncriptar(this string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
