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
    public  partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
            ConfigurarFormularioBase();
        }


        private void frmBase_Load(object sender, EventArgs e)
        {
            SetUppercaseOnAllTextBoxes(this.Controls);
            SetUppercaseOnAllComboBoxes(this.Controls); 
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetUppercaseOnAllTextBoxes(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.CharacterCasing = CharacterCasing.Upper;
                }
                if (control.HasChildren)
                {
                    SetUppercaseOnAllTextBoxes(control.Controls);
                }
            }
        }

        private void SetUppercaseOnAllComboBoxes(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is ComboBox comboBox && comboBox.DropDownStyle != ComboBoxStyle.DropDownList)
                {
                    comboBox.KeyPress += ComboBox_KeyPress_ForceUppercase;
                }

                if (control.HasChildren)
                {
                    SetUppercaseOnAllComboBoxes(control.Controls);
                }
            }
        }

        private void ComboBox_KeyPress_ForceUppercase(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }


        protected void TextBox_KeyPress_ApenasNumerosEVirgula(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            TextBox textBox = sender as TextBox;
            if (e.KeyChar == ',' && textBox.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        protected void TextBox_KeyPress_ApenasNumerosInteiros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        protected void VerificarMaxLength(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt && !txt.ReadOnly && txt.MaxLength == 32767)
                {
                    txt.BackColor = Color.FromArgb(255, 255, 200); 

                    System.Diagnostics.Debug.WriteLine($"ALERTA: O campo '{txt.Name}' no formulário '{this.Name}' está sem limite (MaxLength).");
                }

                if (c.HasChildren)
                {
                    VerificarMaxLength(c);
                }
            }
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
