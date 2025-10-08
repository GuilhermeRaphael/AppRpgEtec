using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class LocalizacaoViewModel
    {
        private Map meuMapa;

        public Map MeuMapa
        {
            get => meuMapa;
            set
            {
                if (value != meuMapa)
                {
                    meuMapa = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
