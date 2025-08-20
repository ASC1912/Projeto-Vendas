using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroCompras2 : Form
    {
        public frmCadastroCompras2()
        {
            InitializeComponent();
            ConfigurarFormularioBase();
        }

        public virtual void ConfigurarFormularioBase()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.ShowIcon = false;
            this.AutoScroll = true;
            this.CancelButton = this.btnSair;
            this.Size = new Size(1360, 728);
            this.MinimumSize = new Size(1360, 728);
            this.MaximumSize = new Size(1360, 728);
            this.AutoSize = false;
        }
    }
}
