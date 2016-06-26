

namespace controller {

	class mainController {

		public tableRoomController infoController {get; set;}
		public billController billController {get; set;}
		public bool isTable {get; set}

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

		// change current table to other free table 
		public void changeTable( int currentTableIndex, int changeToTableIndex ) {

			// set customer number and state
			tableList[changeToTableIndex-1].customerNum 
				= tableList[currentTableIndex-1].customerNum;
			tableList[changeToTableIndex-1].state 
				= tableList[currentTableIndex-1].stateOptions.InUse;

			// set bill
			tableList[changeToTableIndex-1].billIndex = this.billIndexGenerate(isTable, index);
			this.billDate = new Date();
			this.money = 0;
			this.itemNum = 0;
			this.billItemList = new List<BillItem>();
			this.isTable = isTable;
			this.tableOrRoomIndex = index;
			foreach ( table in tableList ) {
				if ( table.index == tableIndex ) {

				}
			}
		}
	}	

	class billController {

		public paymentController paymentController {get; set;}
		public bill currentBill {get; set;}

		public billController(mainController mainController) {
			this.currentBill = new bill(mainController.isTable, );
		}

		// go the the order controller
		public void addItem() {

		}

		// bill selected item deleted
		public void deleteItem( billItem billItem ) {
			this.bill.billItemList.remove( billItem );
		}

		// bill item selected split
		public List<billItem> split( List<billItem> itemList, int splitNum ) {

			List<billItem> newItemList = new List<billItem>();

			foreach( billItem in itemList ) {				
				for ( int i = 0; i < num; i++ ) {
					newItemList.add( new billItem(
										billItem.name, 
										billItem.quantity, 
										splitNum, 
										billItem.unitPrice, 
										billItem.totalPrice / num,
										"splitted by" + splitNum ) );
				}
			}

			return newItemList;
		}

		// bill items splitted number added by 1
		public int addSplitNum( int num ) {
			return (num += 1);
		}

		// bill items splitted number deducted by 1
		public int minusSplitNum( int num ) {
			return (num -= 1);
		}

		// same named bill items conbined
		public billItem combine( List<billItem> itemList ) {

			return ( new billItem (
						itemList[0].name, 
						itemList[0].quantity, 
						itemList[0].num, 
						itemList[0].unitPrice, 
						itemList[0].quantity * itemList[0].unitPrice,
						"conbined by" + itemList.count() ) );
		}

		// bill items discounted by amount
		public List<billItem> setDiscountByAmount( List<billItem> itemList, float discountAmount ) {

			List<billItem> newItemList = new List<billItem>();

			foreach ( billItem in itemList ) {
				newItemList.add ( new billItem (
										billItem.name, 
										billItem.quantity, 
										billItem.splitNum, 
										billItem.unitPrice, 
										billItem.totalPrice - discountAmount,
										"discounted by" + discountAmount ) );
			}

			return newItemList;
		}

		// bill items discounted by percentage
		public List<billItem> setDiscountByPercentage( List<billItem> itemList, float discountPercentage ) {
			
			List<billItem> newItemList = new List<billItem>();

			foreach ( billItem in itemList ) {

				newItemList.add ( new billItem (
										billItem.name, 
										billItem.quantity, 
										billItem.splitNum, 
										billItem.unitPrice, 
										billItem.totalPrice * (1 - discountPercentage),
										"discounted by" + discountPercentage ) );
			}

			return newItemList;
		}

		// get total price for a bill
		public float getTotalPrice( List<billItem> itemList ) {

			float totalPrice = 0.0;

			foreach ( billItem in itemList ) {
				totalPrice += billItem.totalPrice;
			}

			return totalPrice;
		}

		// get the strings of bill items
		public String billItemToString( List<billItem> itemList ) {

			String billItemString = "";
			float totalMoney = 0.0;
			// change all bill items into the strings
			foreach ( billItem in itemList ) {

				String quantityString = "";

				if ( billItem.splitNum == 1 ) {
					quantityString += billItem.quantity;
				}
				else {
					quantityString += billItem.quantity + "/" + billItem.splitNum;
				}

				billItemString += billItem.name + 
								  billItem.quantityString +
								  billItem.totalPrice +
								  "\n";
			}

			// add total money to the string
			billItemString += "Payment: " + totalMoney;

			// return the string
			return billItemString;
		}

		
	}

	class paymentController {

	}
}
