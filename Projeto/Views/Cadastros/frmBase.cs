using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
            ConfigurarFormularioBase();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public virtual void ConhecaObj(object obj, object ctrl)
        {
        }

        public virtual void LimparTxt()
        {
        }
        public virtual void CarregaTxt()
        {
        }
        public virtual void BloquearTxt()
        {
        }
        public virtual void DesbloquearTxt()
        {
        }
    }
}
