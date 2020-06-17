using System;
using System.Linq;
using Tiendita.Models;

namespace Tiendita
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1) Buscar producto");
            Console.WriteLine("2) Crear producto");
            Console.WriteLine("3) Actualizar producto");
            Console.WriteLine("4) Eliminar producto");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarProductos();
                    break;
                case "2":
                    CrearProducto();
                    break;
                case "3":
                    ActualizarProducto();
                    break;
                case "4":
                    EliminarProducto();
                    break;
                case "0": return;
            }
            Menu();
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
            }
        }
    }
}
