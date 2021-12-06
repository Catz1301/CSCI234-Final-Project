using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace RollingStockDB
{
	public class RollingStockDatabase
	{
		RollingStockModel db;
		public RollingStockDatabase() {
			db = new RollingStockModel();
		}

		public void AddEntry(RollingStock rollingStockEntity) {
			db.RollingStocks.Add(rollingStockEntity);
			db.SaveChanges();
		}

		public void RefreshDbContext() {
			if (db != null) {
				db.Dispose();
			}

			db = new RollingStockModel();
		}

		public int Size() {
			return db.RollingStocks.Local.Count;
		}

		public void LoadRollingStock() {
			db.RollingStocks.Load();
		}

		public string Search() {
			throw new NotImplementedException();
		}

		public void Close() {
			db.Database.Connection.Close();
		}

		public DbSet<RollingStock> GetRollingStocksSet() {
			return db.RollingStocks;
		}

		public int Save() {
			return db.SaveChanges();
		}

		public void Purge() {
			//DbSqlQuery<RollingStock> query = db.RollingStocks.SqlQuery("DELETE FROM dbo.RollingStocks WHERE Deleted = true");
			db.Database.ExecuteSqlCommand("DELETE FROM dbo.RollingStocks WHERE Deleted='true'");
		}

		public void Search(string searchText) {
			if (string.IsNullOrWhiteSpace(searchText)) {
				throw new ArgumentException($"'{nameof(searchText)}' cannot be null or whitespace.", nameof(searchText));
			}
			

			throw new NotImplementedException();
		}
	}
}
