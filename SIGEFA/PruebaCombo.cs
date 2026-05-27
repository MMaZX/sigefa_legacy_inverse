using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Formularios;

namespace SIGEFA;

public class PruebaCombo
{
	public DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	public DataGridViewComboBoxColumn CreateInventoryComboBox()
	{
		combo.DataSource = AdmPro.cargaetiquetas(frmLogin.iCodAlmacen);
		combo.DataPropertyName = "Id_etiqueta";
		combo.DisplayMember = "descripcion";
		combo.ValueMember = "Id_etiqueta";
		combo.Name = "Id_etiqueta";
		combo.HeaderText = "Code";
		combo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
		return combo;
	}
}
