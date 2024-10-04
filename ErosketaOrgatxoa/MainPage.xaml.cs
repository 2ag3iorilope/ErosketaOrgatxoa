using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace ErosketaOrgatxoa
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> fruitsInCart { get; set; } = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();
            fruitsListView.ItemsSource = fruitsInCart; // Asignar la fuente de datos al ListView
        }

        void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnDragStarting invocado");

            // Verificar que el sender sea un DragGestureRecognizer
            if (sender is DragGestureRecognizer dragGestureRecognizer)
            {
                // Obtener el elemento que contiene el gesto
                var image = dragGestureRecognizer.Parent as Image;

                if (image != null)
                {
                    // Obtener el nombre de la imagen
                    string fruitName = image.Source.ToString();

                    // Determinar el nombre de la fruta a partir del Source
                    if (fruitName.Contains("sandia"))
                        fruitName = "Sandía";
                    else if (fruitName.Contains("manzana"))
                        fruitName = "Manzana";
                    else if (fruitName.Contains("pera"))
                        fruitName = "Pera";
                    else if (fruitName.Contains("naranja"))
                        fruitName = "Naranja";
                    else if (fruitName.Contains("pinia"))
                        fruitName = "Piña";
                    else if (fruitName.Contains("melon"))
                        fruitName = "Melón";

                    // Almacenar el nombre de la fruta en los datos de arrastre
                    if (!string.IsNullOrEmpty(fruitName))
                    {
                        e.Data.Properties.Add("FruitName", fruitName);
                        System.Diagnostics.Debug.WriteLine($"Arrastrando fruta: {fruitName}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No se pudo determinar el nombre de la fruta.");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No se pudo obtener la imagen del DragGestureRecognizer.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"El objeto que se arrastra no es un DragGestureRecognizer: {sender.GetType()}");
            }
        }




        // Manejador para recibir la fruta en el carrito
        void OnDrop(object sender, DropEventArgs e)
        {
            // Verificar si se ha recibido un nombre de fruta
            if (e.Data.Properties.ContainsKey("FruitName"))
            {
                // Obtener el nombre de la fruta
                var fruitName = (string)e.Data.Properties["FruitName"];

                // Mensaje de depuración para verificar que se ha recibido el nombre de la fruta
                System.Diagnostics.Debug.WriteLine($"Fruta recibida: {fruitName}");

                // Añadir el nombre de la fruta a la lista cuando se suelta en el carrito
                if (!string.IsNullOrEmpty(fruitName))
                {
                    fruitsInCart.Add(fruitName); // Añadir a la lista observable
                    System.Diagnostics.Debug.WriteLine($"Añadido al carrito: {fruitName}"); // Confirmación de adición
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No se encontró 'FruitName' en las propiedades de datos.");
            }
        }
    }
}
