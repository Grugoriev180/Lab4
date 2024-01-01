using AutoMapper;
using Lab4.IRepositoryBase;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controlers;

public class QueueController: Controller
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public QueueController(IOrderItemsRepository orderItemsRepository, IProductsRepository productsRepository, IUserRepository userRepository,
        IQueueRepository queueRepository, IMapper mapper)
    {
        _orderItemsRepository = orderItemsRepository;
        _productsRepository = productsRepository;
        _userRepository = userRepository;
        _queueRepository = queueRepository;
        _mapper = mapper;
    }
    public IActionResult Index()
    {
        var queues = _queueRepository.GetAllQueues();
        return Json(queues);
    }
}