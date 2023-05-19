using Cafeteria.Data;
using Cafeteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Cafeteria.Controllers
{
    public class BillsController : Controller
    {
        private readonly CafeteriaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public const string CARTKEY = "cart";
        public BillsController(CafeteriaContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // CART.
        // Add drink to cart
        [Route("addcart/{drinkId:Guid}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] Guid drinkId)
        {
            var drink = _context.Drinks.Where(p => p.DrinkId == drinkId).FirstOrDefault();
            if (drink == null)
            {
                return NotFound("Giỏ hàng trống!");
            }

            // Add to Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Drink.DrinkId == drinkId);
            if (cartitem != null)
            {
                // Existed, add 1.
                cartitem.Quantity++;
            }
            else
            {
                // Add.
                cart.Add(new CartItem() { Quantity = 1, Drink = drink });
            }

            // Save cart into Session.
            SaveCartSession(cart);
            // Move to Cart view.
            return RedirectToAction(nameof(Cart));
        }

        // Delete item in cart.
        [Route("/removecart/{drinkId:Guid}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] Guid drinkId)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Drink.DrinkId == drinkId);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // Update.
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] Guid drinkid, [FromForm] int quantity)
        {
            // Update Cart, change quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Drink.DrinkId == drinkid);
            if (cartitem != null)
            {
                // Existed, add 1.
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            return View("Cart", GetCartItems());
        }

        // Display cart.
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        // Get cart from Session (CartItem list).
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Delete cart from session.
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Save Cart (CartItem list) into session.
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        // GET: BillsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BillsController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: BillsController/Create
        public ActionResult Create()
        {
            ViewBag.CartItems = GetCartItems();
            return View();
        }

        // POST: BillsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                var orderId = Guid.NewGuid();
                var orders = new Order
                {
                    OrderId = orderId,
                    GuestName = order.GuestName,
                    GuestPhone = order.GuestPhone,
                    GuestAddress = order.GuestAddress,
                    Note = order.Note,
                };
                _context.Orders.Add(orders);
                _context.SaveChanges();
                var listCartItem = GetCartItems();
                foreach (var item in listCartItem)
                {
                    var orderItemAdd = new OrderItem
                    {
                        OrderItemId = Guid.NewGuid(),
                        Quantity = item.Quantity,
                        DrinkId = item.Drink.DrinkId,
                        OrderId = orderId
                    };
                    _context.OrderItems.Add(orderItemAdd);
                }
                _context.SaveChanges();
                return View("Details", _context.Orders.Include(x => x.OrderItems).ThenInclude(c => c.Drink).FirstOrDefault(x => x.OrderId == orderId));
            }
            catch
            {
                return View();
            }
        }
        
        // GET: BillsController/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: BillsController/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(Guid orderId)
        {
            try
            {
                var query = _context.Orders.Include(x => x.OrderItems).ThenInclude(c => c.Drink).FirstOrDefault(x => x.OrderId == orderId);
                if(query != null)
                {
                    return View("Details", query);
                }
                else
                {
                    return BadRequest("Không tìm thấy mã hóa đơn!");
                }
            }
            catch
            {
                return View();
            }
        }
        
        // GET: BillsController/Search
        public ActionResult Cancel(Guid? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var order = _context.Orders.FirstOrDefault(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: BillsController/Search
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirm(Guid orderId)
        {
            try
            {
                var query = _context.Orders.Include(x => x.OrderItems).ThenInclude(c => c.Drink).FirstOrDefault(x => x.OrderId == orderId);
                if(query != null)
                {
                    query.Status = Models.Enum.Status.Canceled;

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Details));
                }
                else
                {
                    return BadRequest("Không tìm thấy mã hóa đơn!");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
