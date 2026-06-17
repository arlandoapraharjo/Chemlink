using System;
using System.Collections.Generic;
using CHEMLINK.Contexts;
using CHEMLINK.Models;
using CHEMLINK.Views;

namespace CHEMLINK.Controllers
{
    /// <summary>
    /// Handles product CRUD operations and category management.
    /// </summary>
    public class ProductController
    {
        private readonly MainForm _view;
        private readonly User _currentUser;

        // Contexts
        private readonly ProductContext _productContext;
        private readonly CategoryContext _categoryContext;

        // In-memory state
        private List<Product> _products;
        private List<Category> _categories;

        public ProductController(MainForm view, User user)
        {
            _view = view;
            _currentUser = user;

            _productContext = new ProductContext();
            _categoryContext = new CategoryContext();
            _products = _productContext.Read();
            _categories = _categoryContext.Read();

            // Wire product events
            _view.AddProductEvent += HandleAddProduct;
            _view.EditProductEvent += HandleEditProduct;
            _view.DeleteProductEvent += HandleDeleteProduct;
            _view.ManageCategoryEvent += HandleManageCategory;
        }

        public void ShowProductCatalog()
        {
            _products = _productContext.Read();
            _categories = _categoryContext.Read();
            _view.ShowProductCatalog(_products, _currentUser.Role == "Admin", _categories);
        }

        private void HandleAddProduct(object? sender, Product e)
        {
            int idKategori = 0;
            foreach (var cat in _categories)
            {
                if (cat.Name == e.Category)
                {
                    idKategori = cat.Id;
                    break;
                }
            }
            _productContext.Create(e.Name, idKategori, e.Stock, e.Price, e.Description, _currentUser.Id, e.SupplierId);
            _view.ShowMessage("Obat pertanian berhasil ditambahkan!");
            ShowProductCatalog();
        }

        private void HandleEditProduct(object? sender, Product e)
        {
            int idKategori = 0;
            foreach (var cat in _categories)
            {
                if (cat.Name == e.Category)
                {
                    idKategori = cat.Id;
                    break;
                }
            }
            _productContext.Update(e.Id, e.Name, idKategori, e.Stock, e.Price, e.Description, e.SupplierId);
            _view.ShowMessage("Data obat berhasil diupdate!");
            ShowProductCatalog();
        }

        private void HandleDeleteProduct(object? sender, int id)
        {
            _productContext.Delete(id);
            _view.ShowMessage("Obat berhasil dihapus.");
            ShowProductCatalog();
        }

        private void HandleManageCategory(object? sender, EventArgs e)
        {
            _categories = _categoryContext.Read();
            using (var form = new ManageCategoryForm(_categories))
            {
                form.AddCategoryEvent += (s, args) =>
                {
                    _categoryContext.Create(args.Name);
                    _categories = _categoryContext.Read();
                    form.LoadCategories(_categories);
                };
                form.UpdateCategoryEvent += (s, args) =>
                {
                    _categoryContext.Update(args.Id, args.Name);
                    _categories = _categoryContext.Read();
                    form.LoadCategories(_categories);
                };
                form.DeleteCategoryEvent += (s, id) =>
                {
                    _categoryContext.Delete(id);
                    _categories = _categoryContext.Read();
                    form.LoadCategories(_categories);
                };
                form.ShowDialog(_view);
            }
            ShowProductCatalog(); // Refresh categories in product catalog
        }
    }
}
