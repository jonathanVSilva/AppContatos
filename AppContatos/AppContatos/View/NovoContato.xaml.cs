using AppContatos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppContatos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoContato : ContentPage
    {
        public NovoContato()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
             try
             {
                Contato contato_selecionado = new Contato();

                if (BindingContext != null)
                    contato_selecionado = BindingContext as Contato;

                Contato c = new Contato
                {
                    Id = contato_selecionado.Id,
                    Nome = txt_nome.Text,
                    Sobrenome = txt_sobrenome.Text,
                    Email = txt_email.Text,
                    Numero = Convert.ToDouble(txt_numero.Text),
                };


                if (c.Id == 0)
                {
                    await App.Database.Insert(c);
                    await DisplayAlert("Deu Certo!", "Contato Inserido", "OK");
                }
                else
                {
                    await App.Database.Update(c);
                    await DisplayAlert("Deu Certo!", "Contato atualizado", "OK");
                }

                await Navigation.PopAsync();
            } 
            catch(Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
    }
}