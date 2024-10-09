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

        /// <summary>
        ///? Metodo honi fruta bat arrastratzenan deitzen zaio
        /// </summary>
        /// <param name="sender">Aukerattuako irudia</param>
        /// <param name="e">Argumentoak</param>
        void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnDragStarting erabiltzen");


            if (sender is DragGestureRecognizer dragGestureRecognizer)
            {
                var image = dragGestureRecognizer.Parent as Image; //? Lortu guk aukeratutako irudia

                if (image != null)
                {
                    // ! Fruta izena eskuratzen du argazki izenaren arabera
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
                    System.Diagnostics.Debug.WriteLine("Errorea DragGestureRecognizer.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Objetua ez da irudi bat: {sender.GetType()}");
            }
        }

        /// <summary>
        /// ? Karritoan fruta bat askatzean deitzen den metodoa
        /// </summary>
        /// <param name="sender">Gure karritoa</param>
        /// <param name="e">Argumentoak</param>
        async void OnDrop(object sender, DropEventArgs e)
        {

            if (e.Data.Properties.ContainsKey("FruitName"))
            {
                var fruitName = (string)e.Data.Properties["FruitName"];
                System.Diagnostics.Debug.WriteLine($"Fruta eskuratuta: {fruitName}");

                // ! EZARRI BAI O BAI BESTELA IRUDIA ALDATZEN DA
                e.Handled = true;

                if (!string.IsNullOrEmpty(fruitName))
                {
                    //? Konprobatu fruta karritoan dagoen
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
                                // ? Abisua 20 fruta baino gehiago artzen baditugu
                                System.Diagnostics.Debug.WriteLine($"Ezin dira {fruitName}. Gehiago sartu. Kantitate maximoa burutu da.");
                                await DisplayAlert("Limitea lortu duzu", $"Ezin dira 20 {fruitName} Baino gehiago sartu", "OK");
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

        /// <summary>
        /// ! Desegin botoia sakatzerakoan exekutatzen den metodoa
        /// ! Azkenik gehitutako fruta ezabatzen du
        /// </summary>
        /// <param name="sender">Bidalitako objetua</param>
        /// <param name="e">Los argumentos del evento</param>
        void OnUndoButtonClicked(object sender, EventArgs e)
        {
            if (fruitsInCart.Count > 0)
            {
                var lastItem = fruitsInCart[fruitsInCart.Count - 1];
                if (lastItem.Value > 1)
                {

                    fruitsInCart[fruitsInCart.Count - 1] = new KeyValuePair<string, int>(lastItem.Key, lastItem.Value - 1);
                }
                else
                {

                    fruitsInCart.RemoveAt(fruitsInCart.Count - 1);
                }
                System.Diagnostics.Debug.WriteLine($"Deseginda: {lastItem.Key}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ez dago elementurik desegiteko.");
            }
        }

        /// <summary>
        /// Gure fruta zerrenda arbitzeko metodoa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClearButtonClicked(object sender, EventArgs e)
        {
            fruitsInCart.Clear();
            System.Diagnostics.Debug.WriteLine("Fruten zerrenda garbitu da.");
        }

        /// <summary>
        /// ! Aplikaziotik ireteteko metodoa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
            System.Diagnostics.Debug.WriteLine("Aplikazioa itxi da.");
        }
    }
}
