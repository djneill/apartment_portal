using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Insights;
using apartment_portal_api.Services.AIService;
using AutoMapper;
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

    [HttpGet]
    public async Task<ActionResult<ICollection<InsightResponse>>> GetInsights()
    {
        var pastInsights = await _unitOfWork.InsightRepository.GetPastInsights();
        var pastInsightsResponse = _mapper.Map<ICollection<InsightResponse>>(pastInsights);

        var issues = await _unitOfWork.IssueRepository.GetCommonIssues();

        if (!issues.Any())
        {
            AllInsightsResponse earlyResponse = new([], pastInsightsResponse);
            return Ok(earlyResponse);
        }

        var aIPostReq = _mapper.Map<ICollection<IssueAIPostRequest>>(issues);
        var insightResponse = await _aiService.GenerateInsights(aIPostReq);

        var newInsights = _mapper.Map<ICollection<Insight>>(insightResponse);

        foreach (var newInsight in newInsights)
        {
            await _unitOfWork.InsightRepository.AddAsync(newInsight);
        }

        await _unitOfWork.SaveAsync();

        var currentInsightsResponse = _mapper.Map<ICollection<InsightResponse>>(newInsights);

        AllInsightsResponse res = new(currentInsightsResponse, pastInsightsResponse);

        return Ok(res);
    }
}