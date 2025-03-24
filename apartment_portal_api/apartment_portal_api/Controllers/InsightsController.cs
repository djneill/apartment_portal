using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Insights;
using apartment_portal_api.Services.AIService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[Authorize(Roles = "Admin")]
public class InsightsController : BaseApiController
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
    public async Task<ActionResult<AllInsightsResponse>> GetInsights()
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

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateInsight(int id, InsightPatchRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var insight = await _unitOfWork.InsightRepository.GetAsync(id);
        if (insight is null) return NotFound();

        var statusResponse = await _unitOfWork.InsightStatusRepository.GetAsync(s => s.Name == "Resolved");
        var status = statusResponse.FirstOrDefault();
        if (status is null) return BadRequest("Something went wrong");

        insight.ActionTaken = request.ActionTaken;
        insight.InsightStatusId = request.IsComplete ? status.Id : insight.InsightStatusId;

        _unitOfWork.InsightRepository.Update(insight);
        await _unitOfWork.SaveAsync();

        return Ok();
    }
}