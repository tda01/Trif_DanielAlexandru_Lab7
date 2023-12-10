using Trif_DanielAlexandru_Lab7.Models;
namespace Trif_DanielAlexandru_Lab7;

public partial class ListPage : ContentPage
{
    List<Product> displayedProducts;

    public ListPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopList = (ShopList)BindingContext;

     
        if (displayedProducts == null)
        {
            displayedProducts = await App.Database.GetListProductsAsync(shopList.ID);
            listView.ItemsSource = displayedProducts;
        }
        else
        {
           
            listView.ItemsSource = displayedProducts;
        }
    }

    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            var selectedProduct = listView.SelectedItem as Product;

            displayedProducts.Remove(selectedProduct);

          
            listView.ItemsSource = null;
            listView.ItemsSource = displayedProducts;
        }
    }

}