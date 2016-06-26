
namespace model {

	abstract class customerContainer {
		/** the index for each container*/
		public int index {get; set;}
		
		/** the maximum customer number for each container */
		public int capacity {get; set;} 
		
		/** the number of customer */
		public int customerNum {get; set;} 

		/** the bill for the customer container */
		public bill bill {get; set;}

		/** the state of a container */
		public stateOptions state {get; set;}
		
		/** the state options for a container */
		public enum stateOptions {
			InUse, Cleaning, Free, Checking, reparing
		}

		abstract private void setCapacityByIndex(int index);
	}

	class table: customerContainer {

		public table(int index) {
			this.index = index;
			this.setCapacityByIndex(index);
			this.customerNum = 0;
			this.bill = new bill(true, index);
			this.state = stateOptions.Free;
		}

		private void setCapacityByIndex(int index) {
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
			this.bill = new bill(false, index);
			this.state = stateOptions.Free;
		}

		private void setCapacityByIndex(int index) {
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
		 * bill index form(21 digits): 
		 * yyyymmddhhmmss + 0/1(0 for table, 1 for ktv, 2 for table+ktv, 3 for ktv+table) + xx(table or ktv index) + preserve(00)
		 */
		private int billIndexGenerate( boolean isTable, int index ) {
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

		// used when change table or room 
		private int changeCustomerContainerBillIndexGernerate( 
						int originalBillIndex, int changeToTableIndex, bool changeToTable ) {
			// add date
			String billIndex = (originalBillIndex.toString()).subString(0,11);
			// add table or ktv room identifier
			if ( changeToTable ) {
				
			}
		}

		public void setItemNumByListCount() {
			this.itemNum = this.billItemList.count();
		}
	}

	/*
		this class defines the bill items for a bill
	*/
	class billItem {
		/** the name for a bill item */
		public String name {get; set;}

		/** the item index for a bill item */
		// public int itemIndex {get; set;}

		/** the quantity for a bill item */
		public int quantity {get; set;}

		/** the split number for a item */
		public int splitNum {get; set;}

		/** the unit price for a item */
		public float unitPrice {get; set;}

		/** the total price by quantity for this single item */
		public float totalPrice {get; set;}

		/** the notes for a bill item */
		public String notes {get; set;}

		/** 
			constructor for default 
		*/
		public billItem() {
			this.name = "";
			// this.itemIndex = 0;
			this.quantity = 0;
			this.splitNum = 1;
			this.unitPrice = 0;
			this.totalPrice = 0;
			this.notes = "";
		}

		/** 
			constructor for spliting, combining, discounting
		*/
		public billItem( String name, int quantity, int splitNum, float unitPrice, float totalPrice, String notes ) {
			this.name = name;
			// this.itemIndex = 0;
			this.quantity = quantity;
			this.splitNum = splitNum;
			this.unitPrice = unitPrice;
			this.totalPrice = totalPrice;
			this.notes += ", " + notes;
		}

		/*
			constructor with a bill item name input for a bill item build up
		*/
		public billItem(String name) {
			this.generateItemByItemName(name);	
		}

		public void generateItemByItemName(String name) {
			String line = "";
			String[] lineParts;
			StreamReader reader = new StreamReader("billItemFile");
			while ( (line = billItemFile.ReadLine()) != null ) {
				lineParts = line.split(new String[]{' '});
				if ( lineParts[0] == name ) {		
					this.name = lineParts[0];
					// this.itemIndex = lineParts[1];
					this.unitPrice = lineParts[1];	
					this.quantity = 0;		
					break;
				}
			}
			reader.close();
		}
	}

	class billItemList {

		public int itemNum {get; set;}
		public List<billItem> billItemList {get; set;}

		public billItemList() {
			this.billItemList = new List<billItem>();
			this.itemNum = 0;
		}

		public int getItemNumFromList() {
			return this.billItemList.count();
		}
	}

	class payment {

		public int paymentIndex {get; set;}
		public List<billItem> billItemList {get; set;}
		public float sumOfMoney {get; set;}
		public Date paymentDate {get; set;}
		public paymentOption paymentType;
		public enum paymentOption {
			Cash, Card
		}

		public payment() {

		}
	}
}