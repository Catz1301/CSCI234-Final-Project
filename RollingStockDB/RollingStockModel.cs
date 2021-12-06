using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollingStockDB {
	public class RollingStockModel : DbContext {
		// Your context has been configured to use a 'RollingStockModel' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'RollingStockDB.RollingStockModel' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'RollingStockModel' 
		// connection string in the application configuration file.
		public RollingStockModel()
			:  base("RollingStockModel") {
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<RollingStock> RollingStocks { get; set; }
		//public virtual DbSet<DeletedRollingStock> DeletedRollingStocks { get; set; }
	}

	public enum StockType {
		AUTORACK,
		BOXCAR,
		CABOOSE,
		CENTER_IBEAM,
		ENGINE,
		FLATCAR,
		GONDOLA,
		HOPPER,
		INSPECTION,
		TANK,
		WAYCAR
	};

	public class RollingStock {
		[Index(IsUnique = true)][Key]
		public int Id { get; set; }
		public bool Is_Engine { get; set; }
		public bool Has_Load { get; set; }
		public string Owning_Company { get; set; }
		public StockType Stock_Type { get; set; }
		public string Reporting_Marks { get; set; }
		public int Fleet_Id { get; set; }
		public bool Deleted { get; set; }
	}

	/*public class DeletedRollingStock {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }
		public bool Is_Engine { get; set; }
		public bool Has_Load { get; set; }
		public string Owning_Company { get; set; }
		public StockType Stock_Type { get; set; }
		public string Reporting_Marks { get; set; }
		public int Fleet_Id { get; set; }
	}*/
}