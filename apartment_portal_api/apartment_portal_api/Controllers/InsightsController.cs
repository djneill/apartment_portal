using System.Security.Cryptography.X509Certificates;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Insights;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[Route("[controller]")]
[ApiController]
public class InsightsController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public InsightsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("/currentInsights")]
    public async Task<ActionResult<ICollection<InsightResponse>>> GetCurrentInsights()
    {
        var insights = (await _unitOfWork.InsightRepository.GetAsync())
            .ToList()
            .OrderByDescending(insight => insight.CreatedOn)
            .Take(3);

        var response = _mapper.Map<IEnumerable<InsightResponse>>(insights);

        return Ok(response);
    }

    [HttpGet("/previousInsights")]
    public async Task<ActionResult<ICollection<InsightResponse>>> GetPreviousInsights()
    {
        var insights = await _unitOfWork.InsightRepository.GetAsync();

        var response = _mapper.Map<IEnumerable<InsightResponse>>(insights);


        return Ok(response);
    }
}