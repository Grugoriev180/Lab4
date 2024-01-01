using AutoMapper;
using Lab4.IRepositoryBase;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lab4.Controlers;

public class OrderItemsConteroller : Controller
{
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQueueRepository _queueRepository;
        private readonly IMapper _mapper;

        public OrderItemsConteroller(IOrderItemsRepository orderItemsRepository, IOrderRepository orderRepository, IProductsRepository productsRepository,
            IUserRepository userRepository, IQueueRepository queueRepository, IMapper mapper)
        {
            _orderItemsRepository = orderItemsRepository;
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _userRepository = userRepository;
            _queueRepository = queueRepository;
            _mapper = mapper;
        }
        
        public IActionResult AdminIndex()
        {
            var orderItems = _orderItemsRepository.GetAllOrderItems();
            return Json(orderItems);
        }
        [HttpGet("OrderItems/Buy/{productid}")]
        public IActionResult Buy(int productId)
        {
            var product = _productsRepository.GetById(productId);

            if (product == null)
                return NotFound();

            return Json(product);
        }

        [HttpPost("OrderItems/Buy/{productid}")]
        public IActionResult Buy(int productId, int amount)
        {
            var product = _productsRepository.GetById(productId);

            if (product == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.GetUserById(userId);

            if (user == null)
                return NotFound();

            if (product.Amount >= amount)
            {
                var order = new Orders
                {
                    ApplicationUserID = userId,
                    Status = "Done" 
                };

                _orderRepository.Add(order);

                var orderItem = new OrderItems
                {
                    ProductID = productId,
                    Amount = amount,
                    Order = order
                };

                _orderItemsRepository.Add(orderItem);

                product.Amount -= amount;
                _productsRepository.Update(product);

                return RedirectToAction("Index", "Products");
            }
            else
            {
                var order = new Orders
                {
                    ApplicationUserID = userId,
                    Status = "Not Done"
                };
                _orderRepository.Add(order);
                var lastOrderId = _orderRepository.GetOrderId();

                var orderItem = new OrderItems
                {
                    OrderID = lastOrderId,
                    ProductID = productId,
                    Amount = amount,
                    Order = order
                };
                
                var deliveryRequest = new Queue
                {
                    ProductID = productId,
                    OrderID = lastOrderId,
                    AmountRequest = amount,
                };

                _queueRepository.Add(deliveryRequest);

                _orderItemsRepository.Add(orderItem);

                return RedirectToAction("Index", "Products");
            }
        }

        [HttpGet("OrderItems/Cancel/{orderid}")]
        public IActionResult Cancel(int orderid)
        {
            var orderItem = _orderItemsRepository.GetByOrderID(orderid);
            var order = _orderRepository.GetById(orderid);
            if (orderItem != null && order.Status == "Not Done")
            {
                order.Status = "Cancelled";
                _orderRepository.Update(order);

                var deliveryRequest = _queueRepository.GetByOrderId(orderid);
                if (deliveryRequest != null)
                {
                    _queueRepository.Delete(deliveryRequest);
                }

                return NotFound();
            }

            return NotFound();
        }
}