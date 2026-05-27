using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

public class frmReportVentaCredito2 : Form
{
	private IContainer components = null;

	private DateTimePicker dateTimePicker1;

	private DateTimePicker dateTimePicker2;

	private Button button1;

	public frmReportVentaCredito2()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.dateTimePicker1.Location = new System.Drawing.Point(22, 22);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
		this.dateTimePicker1.TabIndex = 0;
		this.dateTimePicker2.Location = new System.Drawing.Point(301, 22);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
		this.dateTimePicker2.TabIndex = 1;
		this.button1.Location = new System.Drawing.Point(214, 72);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(609, 295);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dateTimePicker2);
		base.Controls.Add(this.dateTimePicker1);
		base.Name = "frmReportVentaCredito2";
		this.Text = "frmReportVentaCredito2";
		base.ResumeLayout(false);
	}
}
