namespace Sistema_de_pedidos_restaurante_PF
{
    partial class FormRegistro
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCorreoElec = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConfirmarPassword = new System.Windows.Forms.Label();
            this.txtCorreoElec = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmarPass = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(43, 11);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(56, 16);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(16, 44);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(132, 22);
            this.txtNombre.TabIndex = 1;
            // 
            // lblCorreoElec
            // 
            this.lblCorreoElec.AutoSize = true;
            this.lblCorreoElec.Location = new System.Drawing.Point(24, 108);
            this.lblCorreoElec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorreoElec.Name = "lblCorreoElec";
            this.lblCorreoElec.Size = new System.Drawing.Size(118, 16);
            this.lblCorreoElec.TabIndex = 2;
            this.lblCorreoElec.Text = "Correo Electronico";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(43, 201);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(76, 16);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Contraseña";
            // 
            // lblConfirmarPassword
            // 
            this.lblConfirmarPassword.AutoSize = true;
            this.lblConfirmarPassword.Location = new System.Drawing.Point(12, 282);
            this.lblConfirmarPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirmarPassword.Name = "lblConfirmarPassword";
            this.lblConfirmarPassword.Size = new System.Drawing.Size(134, 16);
            this.lblConfirmarPassword.TabIndex = 4;
            this.lblConfirmarPassword.Text = "Confirmar contraseña";
            // 
            // txtCorreoElec
            // 
            this.txtCorreoElec.Location = new System.Drawing.Point(16, 145);
            this.txtCorreoElec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCorreoElec.Name = "txtCorreoElec";
            this.txtCorreoElec.Size = new System.Drawing.Size(132, 22);
            this.txtCorreoElec.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(16, 233);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(132, 22);
            this.txtPassword.TabIndex = 6;
            // 
            // txtConfirmarPass
            // 
            this.txtConfirmarPass.Location = new System.Drawing.Point(16, 315);
            this.txtConfirmarPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConfirmarPass.Name = "txtConfirmarPass";
            this.txtConfirmarPass.Size = new System.Drawing.Size(132, 22);
            this.txtConfirmarPass.TabIndex = 7;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(16, 394);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(100, 28);
            this.btnEnviar.TabIndex = 8;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(183, 394);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtConfirmarPass);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtCorreoElec);
            this.Controls.Add(this.lblConfirmarPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblCorreoElec);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormRegistro";
            this.Text = "FormRegistro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCorreoElec;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirmarPassword;
        private System.Windows.Forms.TextBox txtCorreoElec;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmarPass;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

