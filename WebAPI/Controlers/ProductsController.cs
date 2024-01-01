using AutoMapper;
using Lab4.IRepositoryBase;
using Lab4.Models;
using Lab4.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controlers;

public class ProductsController : Controller

{
        private readonly IProductsRepository _productsRepository;
        private readonly IQueueRepository _deliveryQueueRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository, IQueueRepository deliveryQueueRepository, IOrderItemsRepository orderItemsRepository,
            IOrderRepository orderRepository, IMapper mapper) 
        {
            _productsRepository = productsRepository;
            _deliveryQueueRepository = deliveryQueueRepository;
            _orderItemsRepository = orderItemsRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var products = _productsRepository.GetAll();
            return Json(products);
        }
        
        public IActionResult Create() 
        {
            return Json("Created");
        }
        
        [HttpPost]
        public IActionResult Create(Products product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _productsRepository.Add(product);
            return Ok();
        }
        
        
        public IActionResult Delete(int id)
        {
            var productDetails = _productsRepository.GetById(id);
            if (productDetails == null)
                return NotFound();
            return Json(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteProduct(int id)
        {
            var productDetails = _productsRepository.GetById(id);
            if (productDetails == null)
                return NotFound();
            _productsRepository.Delete(productDetails);
            return Ok();
        }
        [HttpGet]
        public IActionResult Add(int id)
        {
            var product = _productsRepository.GetById(id);

            if (product == null)
            {
                return NotFound(); 
            }

            return Json(product);
        }

        [HttpPost]
        public IActionResult Add(int productId, int amount)
        {
            var product = _productsRepository.GetById(productId);


            product.Amount += amount;

            _productsRepository.Update(product);

            return Ok();
        }
    }