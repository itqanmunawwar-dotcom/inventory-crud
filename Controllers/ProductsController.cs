using Inventory.Web.Data;
using Inventory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Web.Controllers;

public class ProductsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        var query = context.Products.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(product => product.Name.Contains(search) || (product.Description != null && product.Description.Contains(search)));

        ViewData["Search"] = search;
        return View(await query.OrderBy(product => product.Name).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return product is null ? NotFound() : View(product);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid) return View(product);
        product.UpdatedAt = DateTime.UtcNow;
        context.Add(product);
        await context.SaveChangesAsync();
        TempData["Message"] = "Product created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var product = await context.Products.FindAsync(id);
        return product is null ? NotFound() : View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();
        if (!ModelState.IsValid) return View(product);

        product.UpdatedAt = DateTime.UtcNow;
        context.Update(product);
        await context.SaveChangesAsync();
        TempData["Message"] = "Product updated.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return product is null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            TempData["Message"] = "Product deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
