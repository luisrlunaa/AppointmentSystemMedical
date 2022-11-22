﻿namespace AppointmentSystemMedical.CapaPresentacion.Gerente
{
    partial class frmCartillaMedica
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.rptViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ObtenerCartillaMedica_ResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ObtenerFechaActual_ResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ObtenerCartillaMedica_ResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObtenerFechaActual_ResultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitulo.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(25, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(151, 25);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "Cartilla Medica";
            // 
            // rptViewer
            // 
            reportDataSource1.Name = "CartillaMedica";
            reportDataSource1.Value = this.ObtenerCartillaMedica_ResultBindingSource;
            reportDataSource2.Name = "FechaActual";
            reportDataSource2.Value = this.ObtenerFechaActual_ResultBindingSource;
            this.rptViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.rptViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.rptViewer.LocalReport.ReportEmbeddedResource = "AppointmentSystemMedical.Reportes.rptCartillaMedica.rdlc";
            this.rptViewer.Location = new System.Drawing.Point(25, 75);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.ServerReport.BearerToken = null;
            this.rptViewer.Size = new System.Drawing.Size(550, 350);
            this.rptViewer.TabIndex = 7;
            this.rptViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // ObtenerCartillaMedica_ResultBindingSource
            // 
           // this.ObtenerCartillaMedica_ResultBindingSource.DataSource = typeof(AppointmentSystemMedical.CapaDatos.ObtenerCartillaMedica_Result);
            // 
            // ObtenerFechaActual_ResultBindingSource
            // 
            //this.ObtenerFechaActual_ResultBindingSource.DataSource = typeof(AppointmentSystemMedical.CapaDatos.ObtenerFechaActual_Result);
            // 
            // frmCartillaMedica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.rptViewer);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmCartillaMedica";
            this.Text = "frmMedicos";
            this.Load += new System.EventHandler(this.frmEstadisticas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ObtenerCartillaMedica_ResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObtenerFechaActual_ResultBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private Microsoft.Reporting.WinForms.ReportViewer rptViewer;
        private System.Windows.Forms.BindingSource ObtenerCartillaMedica_ResultBindingSource;
        private System.Windows.Forms.BindingSource ObtenerFechaActual_ResultBindingSource;
    }
}