using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ErosketaOrgatxoa
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<KeyValuePair<string, int>> fruitsInCart { get; set; } = new ObservableCollection<KeyValuePair<string, int>>();

        public MainPage()
        {
            InitializeComponent();
            fruitsListView.ItemsSource = fruitsInCart; 
        }

        void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnDragStarting invocado");

       
            if (sender is DragGestureRecognizer dragGestureRecognizer)
            {
             
                var image = dragGestureRecognizer.Parent as Image;

                if (image != null)
                {
                   
                    //Lortu frutaren izena
                    string fruitName = image.Source.ToString();

                   
                    if (fruitName.Contains("sandia"))
                        fruitName = "Sandia";

                    else if (fruitName.Contains("manzana"))
                        fruitName = "Sagarra";

                    else if (fruitName.Contains("pera"))
                        fruitName = "Udarea";

                    else if (fruitName.Contains("naranja"))
                        fruitName = "Naranja";

                    else if (fruitName.Contains("pinia"))
                        fruitName = "Piña";

                    else if (fruitName.Contains("melon"))
                        fruitName = "Meloia";

                 
                    if (!string.IsNullOrEmpty(fruitName))
                    {
                        e.Data.Properties.Add("FruitName", fruitName);
                        System.Diagnostics.Debug.WriteLine($"Mugitzen ari zaren fruta: {fruitName}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Ezin da frutaren izena aurkitu.");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Errorea  DragGestureRecognizer.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Objetua ez da irudi bat: {sender.GetType()}");
            }
        }

        async void OnDrop(object sender, DropEventArgs e)
        {
            
            if (e.Data.Properties.ContainsKey("FruitName"))
            {
             
                var fruitName = (string)e.Data.Properties["FruitName"];

                System.Diagnostics.Debug.WriteLine($"Fruta recibida: {fruitName}");

              //IRUDIA EZ ALDATZEKO BEHARREZKOA
                e.Handled = true;

              
                if (!string.IsNullOrEmpty(fruitName))
                {
                    
                    for (int i = 0; i < fruitsInCart.Count; i++)
                    {
                        if (fruitsInCart[i].Key == fruitName)
                        {
                          
                            if (fruitsInCart[i].Value < 20)
                            {
                                int newCount = fruitsInCart[i].Value + 1;
                                fruitsInCart[i] = new KeyValuePair<string, int>(fruitName, newCount);
                                System.Diagnostics.Debug.WriteLine($"Kantitatea eguneratu da: {fruitName} x{newCount}");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"Ezin dira  {fruitName}. Gehiago sartu. Kantitate maximoa burutu da.");
                              
                                await DisplayAlert("Limitea lortu duzu", $"Ezin dira 20  {fruitName}. Baino gehiago sartu", "OK");
                            }
                            return;
                        }
                    }

                
                    fruitsInCart.Add(new KeyValuePair<string, int>(fruitName, 1));
                    System.Diagnostics.Debug.WriteLine($"Karritora gehituta: {fruitName} x1");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ez da 'FruitName' aurkitu.");
            }
        }
    }
}
