using System.Security.Cryptography.X509Certificates;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Insights;
using apartment_portal_api.Services;
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
    private AIService _aiService;

    public InsightsController(IUnitOfWork unitOfWork, IMapper mapper, AIService aiService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _aiService = aiService;
    }

    [HttpGet("/currentInsights")]
    public async Task<ActionResult<ICollection<InsightResponse>>> GetCurrentInsights()
    {
        var issues = await _unitOfWork.InsightRepository.GenerateInsights();

        var aIPostReq = _mapper.Map<ICollection<IssueAIPostRequest>>(issues);
        var insightResponse = _aiService.GenerateInsights(aIPostReq);

        return Ok(insightResponse);
    }

    [HttpGet("/previousInsights")]
    public async Task<ActionResult<ICollection<InsightResponse>>> GetPreviousInsights()
    {
        var insights = await _unitOfWork.InsightRepository.GetAsync();

        var response = _mapper.Map<IEnumerable<InsightResponse>>(insights);


        return Ok(response);
    }
}