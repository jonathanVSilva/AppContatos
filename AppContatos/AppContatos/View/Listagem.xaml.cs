using AppContatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppContatos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {

        ObservableCollection<Contato> lista_contatos = new ObservableCollection<Contato>();

        public Listagem()
        {
            InitializeComponent();

            lst_contatos.ItemsSource = lista_contatos;  
        }

        private void ToolbarItem_Clicked_Adicionar(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoContato());

            } catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }
           
        }

        private void lst_contato_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            lista_contatos.Clear();

            Task.Run(async () =>
            {
                List<Contato> temp = await App.Database.GetAll();

                foreach (Contato c in temp)
                {
                    lista_contatos.Add(c);
                }
            });
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;

            Contato contato_selecionado = (Contato)disparador.BindingContext;

            string mensagem = "Remover " + contato_selecionado.Nome + " da compra? ";

            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Contato", "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.Delete(contato_selecionado.Id);
                lista_contatos.Remove(contato_selecionado);
            }
        }

        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;

            lista_contatos.Clear();

            Task.Run(async () =>
            {
                List<Contato> temp = await App.Database.Search(q);

                foreach (Contato c in temp)
                {
                    lista_contatos.Add(c);
                }
            });
        }

        private void lst_contatos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Contato contato_selecionado = (Contato)e.SelectedItem;

                ((ListView)sender).SelectedItem = null;

                Navigation.PushAsync(new NovoContato
                {
                    BindingContext = contato_selecionado
                });
            }
            
        }

        private void lst_contatos_Refreshing(object sender, EventArgs e)
        {
            lista_contatos.Clear();

            Task.Run(async () =>
            {
                List<Contato> temp = await App.Database.GetAll();

                foreach (Contato c in temp)
                {
                    lista_contatos.Add(c);
                }
            });

            ref_carregando.IsRefreshing = false;
        }
    }
}