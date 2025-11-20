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
            this.LblNumeroMesa = new System.Windows.Forms.Label();
            this.LblCliente = new System.Windows.Forms.Label();
            this.LblPlatos = new System.Windows.Forms.Label();
            this.LblFecha = new System.Windows.Forms.Label();
            this.LblTotal = new System.Windows.Forms.Label();
            this.LblEstado = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblNumeroMesa
            // 
            this.LblNumeroMesa.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.LblNumeroMesa.AutoSize = true;
            this.LblNumeroMesa.Location = new System.Drawing.Point(123, 9);
            this.LblNumeroMesa.Name = "LblNumeroMesa";
            this.LblNumeroMesa.Size = new System.Drawing.Size(44, 16);
            this.LblNumeroMesa.TabIndex = 0;
            this.LblNumeroMesa.Text = "Mesa:";
            // 
            // LblCliente
            // 
            this.LblCliente.AutoSize = true;
            this.LblCliente.Location = new System.Drawing.Point(119, 48);
            this.LblCliente.Name = "LblCliente";
            this.LblCliente.Size = new System.Drawing.Size(48, 16);
            this.LblCliente.TabIndex = 1;
            this.LblCliente.Text = "Cliente";
            // 
            // LblPlatos
            // 
            this.LblPlatos.AutoSize = true;
            this.LblPlatos.Location = new System.Drawing.Point(119, 89);
            this.LblPlatos.Name = "LblPlatos";
            this.LblPlatos.Size = new System.Drawing.Size(45, 16);
            this.LblPlatos.TabIndex = 2;
            this.LblPlatos.Text = "Platos";
            // 
            // LblFecha
            // 
            this.LblFecha.AutoSize = true;
            this.LblFecha.Location = new System.Drawing.Point(119, 201);
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Size = new System.Drawing.Size(45, 16);
            this.LblFecha.TabIndex = 3;
            this.LblFecha.Text = "Fecha";
            // 
            // LblTotal
            // 
            this.LblTotal.AutoSize = true;
            this.LblTotal.Location = new System.Drawing.Point(123, 121);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(38, 16);
            this.LblTotal.TabIndex = 4;
            this.LblTotal.Text = "Total";
            // 
            // LblEstado
            // 
            this.LblEstado.AutoSize = true;
            this.LblEstado.Location = new System.Drawing.Point(119, 157);
            this.LblEstado.Name = "LblEstado";
            this.LblEstado.Size = new System.Drawing.Size(50, 16);
            this.LblEstado.TabIndex = 5;
            this.LblEstado.Text = "Estado";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Editar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(104, 234);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Eliminar";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(195, 234);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Est";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // PedidoCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LblEstado);
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.LblFecha);
            this.Controls.Add(this.LblPlatos);
            this.Controls.Add(this.LblCliente);
            this.Controls.Add(this.LblNumeroMesa);
            this.Name = "PedidoCard";
            this.Size = new System.Drawing.Size(285, 277);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblNumeroMesa;
        private System.Windows.Forms.Label LblCliente;
        private System.Windows.Forms.Label LblPlatos;
        private System.Windows.Forms.Label LblFecha;
        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.Label LblEstado;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}
