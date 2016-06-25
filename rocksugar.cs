

namespace controller {

	class mainController {

		public tableRoomController infoController {get; set;}
		public billController billController {get; set;}

		public tableList tableList {get; set;}
		public roomList roomList {get; set;}

		public mainController() {
			infoController = new infoController(this);
			billController = new billController(this);

			tableList = new tableList();

			roomList = new roomList();			
		}
	}

	class infoController {

	}	

	class billController {

		paymentController paymentController;
	}

	class paymentController {

	}
}


namespace model {

	abstract class customerContainer {
		/** the index for each container*/
		public int index {get; set;}
		
		/** the maximum customer number for each container */
		public int capacity {get; set;} 
		
		/** the number of customer */
		public int customerNum {get; set;} 

		/** the state of a container */
		public stateOptions state {get; set;}
		
		/** the state options for a container */
		public enum stateOptions {
			Dining, Cleaning, Free, Checking, reparing
		}

		abstract private setCapacityByIndex(int index);
	}

	class table: customerContainer {

		public table(int index) {
			this.index = index;
			this.setCapacityByIndex(index);
			this.customerNum = 0;
			this.state = stateOptions.Free;
		}

		private setCapacityByIndex(int index) {
			if ( index == 11 || index == 12) {
				this.capacity = 6;
			}
			else if ( index == 6 || index == 7 ) {
				this.capacity = 2;
			}
			else {
				this.capacity = 4;
			}
		}
	}

	class room: customerContainer {

		public room(int index) {
			this.index = index;
			this.setCapacityByIndex(index);
			this.customerNum = 0;
			this.state = stateOptions.Free;
		}

		private setCapacityByIndex(int index) {
			if ( index == 4 ) {
				this.capacity = 12;
			}
			else if ( index == 3 ) {
				this.capacity = 8;
			}
			else {
				this.capacity = 6;
			}
		}
	}

	class tableList {

		public List<table> tableList {get; set;}

		public tableList() {
			this.tableList = new List<table>();
			for ( int i = 0; i < 13; i++ ) {
				tableList.Add(new table(i+1));
			}
		}
	}

	public roomList {

		public List<room> roomList {get; set}

		public roomList() {
			this.roomList = new List<room>();
			for ( int i = 0; i < 5; i++ ) {
				roomList.Add(new room(i+1));
			}
		}
	}

	class bill {

		/** absolute bill index for each bill */
		public int billIndex {get; set;}

		/** bill generating time including date and time */
		public Date billDate {get; set;}

		/** bill total money */
		public float money {get; set;}

		/** bill item number */
		public int itemNum {get; set;}

		/** bill item list */
		public List<BillItem> billItemList {get; set;}

		/** is bill for a table */
		public boolean isTable {get; set;}

		/** table or romm index */
		public int tableOrRoomIndex {get; set;}

		public Bill( boolean isTable, int index ) {
			this.billIndex = this.billIndexGenerate(isTable, index);
			this.billDate = new Date();
			this.money = 0;
			this.itemNum = 0;
			this.billItemList = new List<BillItem>();
			this.isTable = isTable;
			this.tableOrRoomIndex = index;
		}
	
		/**
		 * bill index form(28 digits): 
		 * yyyymmddhhmmss + 0/1(0 for table, 1 for ktv) + xx(table or ktv index) + preserve(00)
		 * + absolute index for a day(xxx) 
		 */
		private int billIndexGenerate(boolean isTable, int index) {
			// add date
			String billIndex = (new SimpleDateFormat("yyMMddHHmmss")).format(getBillDate());
			// add table or ktv room identifier
			if ( isTable ) billIndex += "0";		
			else billIndex += "1";
			// add table or ktv room index
			billIndex += Integer.toString(index);
			// add preserve digits
			billIndex += Integer.toString(00);
			
			return Integer.parseInt(billIndex);
		}
	}
}



