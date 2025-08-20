using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {

        public UsuarioViewModel()
        {
            uService = new UsuarioService();
            InicializarCommand();
        }

        public void InicializarCommand()
        {
            AutenticarCommand = new Command(async() => await AutenticarUsuario());
        }

        private UsuarioService uService;
        public ICommand AutenticarCommand { get; set; }
        

        #region AtributosPropriedades
        private string login = string.Empty;
        private string senha = string.Empty;

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        public string Senha 
        {   get => senha;
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Metodos
        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);

                if(!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem Vindo(a) 🤞🤞🤞 {uAutenticado.Username}";

                    //Guardando dados para o futuro
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "OK");

                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados INCORRETOS 😎🤦‍", "OK");
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + ex.InnerException, "OK");
            }
        }
        #endregion
    }
}
