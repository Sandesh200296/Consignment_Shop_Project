using ConsignmentShopLibrary;

namespace ConsignmentShopDemoUI
{
    public partial class Form1 : Form
    {
        private Store store = new Store();
        // this will be list what's in the shopping cart
        private List<Item> ShoppingCartData = new List<Item>();
        
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();

        private decimal storeProfit = 0;

        //private Item item = new Item();
        //private Vendor vendor = new Vendor();
        public Form1()
        {
            InitializeComponent();
            SetupData();

            // .Where(x => x.Sold == false).ToList();
            // The above line is a filter to make sure item in itemsList gets removed
            // when we hit Purchase btn.
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListBox.DataSource = itemsBinding;

            // Here Display is the new property , which is created in Item class
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            //To store dummy data into cartListBox
            cartBinding.DataSource = ShoppingCartData;
            shoppingCartListBox.DataSource= cartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";


            // To store dummy data into vendorsListBox
            vendorsBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorsBinding;

            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";
        }

        // This method is to setup the dummy data.
        private void SetupData()
        {
            /*
            // One way to add Vendor details
            // First create object of Vendor class
            Vendor demoVendor = new Vendor();
            // Entering the details
            demoVendor.FirstName = "Billi";
            demoVendor.LastName = "Smith";
            demoVendor.Commission = .5M;
            
            //Adding the above Vendor details in store class objects
            store.Vendors.Add(demoVendor);

            demoVendor = new Vendor();
            demoVendor.FirstName = "Sue";
            demoVendor.LastName = "Jonas";
            demoVendor.Commission = 0.3M;

            store.Vendors.Add(demoVendor);
            */

            // Second Way (Efficient Way) Adding Vendor data  
            //store.Vendors = new List<Vendor>();
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Smith" });// Commission is default
            store.Vendors.Add(new Vendor { FirstName = "Sue", LastName = "Jonas" });

            // Adding Item data
            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "Harry Potter Book1",
                Description = "A book about a boy",
                Price = 5.20M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 1.50M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Elevation",
                Description = @"When Scott Carey becomes ill, 
                                old friends and new neighbors come to the rescue.
                                You won’t find King’s usual gory storytelling here.
                                In fact, Elevation is quite a tender story about friendship 
                                and the ways a community tends to circle the wagons around its own
                                … with a supernatural twist, of course.",
                Price = 3.80M,
                Owner = store.Vendors[0]
            });

            store.Name = "Seconds are Better";
        }

        private void addToCartButton_Click(object sender, EventArgs e)
        {
            // check with dummy message box when click on Add To Cart btn
            //MessageBox.Show("I have been clicked.");

            // This btn should figure out what is selected from the items list
            // Copy that item to the shopping cart
            // Do we remove the item from the Items List? - no
            Item SelectedItem = (Item)itemsListBox.SelectedItem;
            //MessageBox.Show(SelectedItem.Title);

            ShoppingCartData.Add(SelectedItem);
            //This will add the listbox items into add to cart list box
            cartBinding.ResetBindings(false);
        }

        private void makePayment_Click(object sender, EventArgs e)
        {
            // mark each item in the cart as sold
            // clear the cart

            foreach (Item item in ShoppingCartData)
            { 
                item.Sold = true;
                item.Owner.PaymentDue += (item.Owner.Commission * item.Price);
                storeProfit += (1- item.Owner.Commission) * item.Price;
            }

            ShoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            storeProfitValue.Text = string.Format("${0}", storeProfit);


            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}