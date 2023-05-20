using DemoQRCode.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace DemoQRCode.Controllers
{
    public class ProductController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ProductController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Products.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NoContent();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.QrCodeData = $"{product.Id}," + $"{product.Name}," + $"{product.Price}," + $"{product.Description}";
                    await _dbContext.Products.AddAsync(product);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(product);
        }

        public IActionResult GetQrCode(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(product.QrCodeData, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeBitMap = qrCode.GetGraphic(30);
            using (var stream = new MemoryStream())
            {
                qrCodeBitMap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return File(stream.ToArray(), "image/png");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.QrCodeData = $"{product.Id}," + $"{product.Name}," + $"{product.Price}," + $"{product.Description}";
                    _dbContext.Entry(product).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var pro = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            if (pro != null)
            {
                _dbContext.Products.Remove(pro);
                _dbContext.SaveChanges();
            }
            return View();
        }
    }
}