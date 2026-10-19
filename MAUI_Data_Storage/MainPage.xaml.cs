using System.Collections.ObjectModel;
using MAUI_Data_Storage.DataAccess;
using MAUI_Data_Storage.Models;

namespace MAUI_Data_Storage
{
    public partial class MainPage : ContentPage
    {
        readonly ItemData itemData = new();
        public ObservableCollection<Item> Items { get; } = new();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadItems();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Every field is required; list the missing ones and do not save
            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(txtItemId.Text)) missing.Add("Item ID");
            if (string.IsNullOrWhiteSpace(txtItemName.Text)) missing.Add("Item Name");
            if (string.IsNullOrWhiteSpace(txtItemDescription.Text)) missing.Add("Item Description");

            if (missing.Count > 0)
            {
                ShowMessage($"Please enter: {string.Join(", ", missing)}. The item was not saved.", isError: true);
                return;
            }

            string itemId = txtItemId.Text.Trim();
            if (await itemData.GetItemByItemIdAsync(itemId) is not null)
            {
                ShowMessage($"An item with ID \"{itemId}\" already exists. The item was not saved.", isError: true);
                return;
            }

            var item = new Item
            {
                ItemId = itemId,
                ItemName = txtItemName.Text.Trim(),
                ItemDescription = txtItemDescription.Text.Trim(),
                DateAdded = DateTime.Now
            };
            await itemData.SaveItemAsync(item);

            ShowMessage($"Item \"{item.ItemName}\" was saved.", isError: false);
            txtItemId.Text = txtItemName.Text = txtItemDescription.Text = string.Empty;
            LoadItems();
        }

        private async void LoadItems()
        {
            var items = await itemData.GetItemsAsync();
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        }

        private void ShowMessage(string text, bool isError)
        {
            lblMessage.Text = text;
            lblMessage.TextColor = isError ? Colors.OrangeRed : Colors.LimeGreen;
            lblMessage.IsVisible = true;
        }
    }
}
