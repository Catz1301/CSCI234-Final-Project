using RollingStockDB;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CSCI234_Final_Project {
	public partial class Form1 : Form {
		RollingStockDatabase rollingStockDb;
		ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(Form1));
		DataGridViewCellStyle deletedCellStyle;
		DataGridViewCellStyle defaultCellStyle;
		public Form1() {
			InitializeComponent();
			rollingStockDb = new RollingStockDatabase();
			stockTypeDataGridViewTextBoxColumn.ValueType = typeof(StockType);
			stockTypeDataGridViewTextBoxColumn.DataSource = Enum.GetValues(typeof(StockType));
			deletedCellStyle = new DataGridViewCellStyle();
			defaultCellStyle = new DataGridViewCellStyle();
			deletedCellStyle.ForeColor = Color.Gray;
			defaultCellStyle.DataSourceNullValue = ((new DataGridViewCellStyle()).ForeColor = Color.Red);
		}

		private void RefreshRollingStock() {
			rollingStockDb.RefreshDbContext();
			rollingStockDb.GetRollingStocksSet()
				.OrderBy(entry => entry.Deleted)
				.Load();
		}

		private void SearchRollingStock(string searchText) {
			Validate();
			rollingStockBindingSource.EndEdit();
			rollingStockDb.RefreshDbContext();
			//rollingStockDb.GetRollingStocksSet().SqlQuery()
		}

		private void SaveTable() {
			Validate();
			rollingStockBindingSource.EndEdit();
			try {
				rollingStockDb.Save();
			} catch (DbEntityValidationException) {
				MessageBox.Show("Columns cannot be empty",
			   "Entity Validation Exception");
			}
			RefreshRollingStock();
			rollingStockBindingSource.DataSource = rollingStockDb.GetRollingStocksSet().Local;
			rollingStockGridView.Update();
			rollingStockGridView.Refresh();
		}

		private void MarkRowDeletion(bool deleted) {
			DataGridViewSelectedCellCollection cells = rollingStockGridView.SelectedCells;
			for (int i = 0; i < cells.Count; i++) {
				int rowIndex = cells[i].RowIndex;
				rollingStockGridView.Rows[rowIndex].Cells[7].Value = deleted;
				for (int cellIndex = 0;
					cellIndex < rollingStockGridView.Rows[rowIndex].Cells.Count;
					cellIndex++) {
					rollingStockGridView.Rows[rowIndex].Cells[cellIndex].Style = (deleted == true) ? deletedCellStyle : rollingStockGridView.Rows[rowIndex].DefaultCellStyle;
				}
				rollingStockGridView.Rows[rowIndex].ReadOnly = deleted;
			}
		}

		private bool Confirm(string msg, string confirmTitle = "Confirm", bool yesNoBtns = true) {
			DialogResult confirmation = MessageBox.Show(msg, confirmTitle, (yesNoBtns == true) ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			return (confirmation == DialogResult.OK || confirmation == DialogResult.Yes);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void cmdExit_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void Form1_Load(object sender, EventArgs e) {
			// TODO: This line of code loads data into the 'rollingStockDBDataSet.RollingStock' table. You can move, or remove it, as needed.
			rollingStockDb.LoadRollingStock();
			rollingStockBindingSource1.DataSource = rollingStockDb.GetRollingStocksSet().Local;
			for (int i = 0; i < rollingStockGridView.RowCount; i++) {
				if (rollingStockGridView.Rows[i].Cells[7].Value != null && (bool) rollingStockGridView.Rows[i].Cells[7].Value == true) {
					for (int cellIndex = 0;
						cellIndex < rollingStockGridView.Rows[i].Cells.Count;
						cellIndex++) {
						rollingStockGridView.Rows[i].Cells[cellIndex].Style = deletedCellStyle;
					}
					rollingStockGridView.Rows[i].ReadOnly = true;
				}
			}
		}


		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			rollingStockDb.Close();
		}

		private void bindingNavigatorSaveItem_Click(object sender, EventArgs e) {
			SaveTable();
		}

		private void saveTableBtn_Click(object sender, EventArgs e) {
			SaveTable();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			MessageBox.Show(resourceManager.GetString("About"), "About");
		}

		private void sUPRISEnotRlyToolStripMenuItem_Click(object sender, EventArgs e) {
			int i = 0;
			while (true) {
				i++;
				MessageBox.Show($"Congratulations! You have earned ({i}) virus for discovering the state I'm in: Boredom.", $"Woof {i}x");
			}
		}

		private void purgeToolStripMenuItem_Click(object sender, EventArgs e) {
			if (Confirm("Are you sure you want to permanently delete the items marked for deletion?", "Confirm the purge...")) {
				SaveTable(); // Save any pending changes
				rollingStockDb.Purge(); // remove items flagged as deleted
				SaveTable(); // save the table again, so that this can't be undone. That's what undelete is for.
			}
		}
		
		private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e) {
			MarkRowDeletion(true);
			SaveTable();
			rollingStockGridView.ClearSelection();
		}

		private void bindingNavigatorUndeleteItem_Click(object sender, EventArgs e) {
			MarkRowDeletion(false);
			SaveTable();
			rollingStockGridView.ClearSelection();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
			MarkRowDeletion(true);
			SaveTable();
			rollingStockGridView.ClearSelection();
		}

		private void undeleteToolStripMenuItem_Click(object sender, EventArgs e) {
			MarkRowDeletion(false);
			SaveTable();
			rollingStockGridView.ClearSelection();
		}

		private void addEntryBtn_Click(object sender, EventArgs e) {
			rollingStockBindingSource.AddNew();
			rollingStockBindingSource.MoveLast();
		}

		private void saveToolStripMenuItem1_Click(object sender, EventArgs e) {
			SaveTable();
		}

		private void bindingNavigatorSearchButton_Click(object sender, EventArgs e) {
			string searchText = bindingNavigatorSearchBox.Text;
			rollingStockDb.Search(searchText);
		}
	}
}
