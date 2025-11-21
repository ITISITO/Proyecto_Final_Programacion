namespace Sistema_de_pedidos_restaurante_PF
{
    partial class PedidoCard
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCambiarEstado = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblPlatos = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblMesa = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Orange;
            this.btnEditar.ForeColor = System.Drawing.Color.Linen;
            this.btnEditar.Location = new System.Drawing.Point(195, 261);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 32);
            this.btnEditar.TabIndex = 6;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Orange;
            this.btnEliminar.Location = new System.Drawing.Point(90, 260);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(97, 33);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click_1);
            // 
            // btnCambiarEstado
            // 
            this.btnCambiarEstado.BackColor = System.Drawing.Color.Orange;
            this.btnCambiarEstado.Location = new System.Drawing.Point(90, 298);
            this.btnCambiarEstado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCambiarEstado.Name = "btnCambiarEstado";
            this.btnCambiarEstado.Size = new System.Drawing.Size(205, 31);
            this.btnCambiarEstado.TabIndex = 9;
            this.btnCambiarEstado.Text = "Cambiar estado";
            this.btnCambiarEstado.UseVisualStyleBackColor = false;
            this.btnCambiarEstado.Click += new System.EventHandler(this.btnCambiarEstado_Click_1);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.lblEstado.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstado.Location = new System.Drawing.Point(171, 206);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 16);
            this.lblEstado.TabIndex = 15;
            this.lblEstado.Text = "Estado";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.lblTotal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotal.Location = new System.Drawing.Point(171, 170);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 16);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "Total";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecha.Location = new System.Drawing.Point(171, 236);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(50, 16);
            this.lblFecha.TabIndex = 13;
            this.lblFecha.Text = "Fecha";
            // 
            // lblPlatos
            // 
            this.lblPlatos.AutoSize = true;
            this.lblPlatos.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.lblPlatos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPlatos.Location = new System.Drawing.Point(171, 138);
            this.lblPlatos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlatos.Name = "lblPlatos";
            this.lblPlatos.Size = new System.Drawing.Size(51, 16);
            this.lblPlatos.TabIndex = 12;
            this.lblPlatos.Text = "Platos";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.lblCliente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCliente.Location = new System.Drawing.Point(171, 110);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(55, 16);
            this.lblCliente.TabIndex = 11;
            this.lblCliente.Text = "Cliente";
            // 
            // lblMesa
            // 
            this.lblMesa.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblMesa.AutoSize = true;
            this.lblMesa.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.lblMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMesa.Location = new System.Drawing.Point(171, 82);
            this.lblMesa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(45, 16);
            this.lblMesa.TabIndex = 10;
            this.lblMesa.Text = "Mesa";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Sistema_de_pedidos_restaurante_PF.Properties.Resources.unnamed__1_;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(399, 397);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // PedidoCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblPlatos);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblMesa);
            this.Controls.Add(this.btnCambiarEstado);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PedidoCard";
            this.Size = new System.Drawing.Size(399, 397);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCambiarEstado;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblPlatos;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
