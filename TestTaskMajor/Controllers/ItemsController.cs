using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskMajor.Data;
using TestTaskMajor.Models;
using TestTaskMajor.Models.Domain;

namespace TestTaskMajor.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemDbContext itemDbContext;
        public ItemsController(ItemDbContext itemDbContext) 
        {
            this.itemDbContext = itemDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await itemDbContext.Items.ToListAsync();
            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddItemViewModel addItemRequest) 
        {
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = addItemRequest.Name,
                Description = addItemRequest.Description,
                Price = addItemRequest.Price,
                Img = addItemRequest.Img
            };
            await itemDbContext.Items.AddAsync(item);
            await itemDbContext.SaveChangesAsync();

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var item = await itemDbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                var viewModel = new UpdateItemViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Img = item.Img
                };

                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateItemViewModel model)
        {
            var item = await itemDbContext.Items.FindAsync(model.Id);

            if (item != null)
            {
                item.Name = model.Name;
                item.Description = model.Description;
                item.Price = model.Price;
                item.Img = model.Img;

                await itemDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateItemViewModel model)
        {
            var item = await itemDbContext.Items.FindAsync(model.Id);

            if (item != null)
            {
                itemDbContext.Items.Remove(item);
                await itemDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
