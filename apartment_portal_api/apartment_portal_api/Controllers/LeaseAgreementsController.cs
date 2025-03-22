using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.LeaseAgreements;
using apartment_portal_api.Models.LeaseStatuses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace apartment_portal_api.Controllers;

[Authorize]
public class LeaseAgreementsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LeaseAgreementsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create(LeaseAgreementPostRequest request)
    {
        LeaseAgreement newAgreement = _mapper.Map<LeaseAgreement>(request);
        newAgreement.LeaseStatusId = 3;

        var unitUserResponse = await _unitOfWork.UnitUserRepository.GetAsync(uu => uu.UserId == request.UserId);
        var unitUser = unitUserResponse.FirstOrDefault();

        if (unitUser == null)
        {
            return BadRequest($"Could not find unit for user {request.UserId}");
        }
        newAgreement.UnitUsersId = unitUser.Id;

        await _unitOfWork.LeaseAgreementRepository.AddAsync(newAgreement);
        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<ICollection<LeaseStatusResponse>>> FetchStatuses()
    {
        var statuses = await _unitOfWork.LeaseStatusRepository.GetAsync();

        var response = _mapper.Map<ICollection<LeaseStatusResponse>>(statuses);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<LeaseAgreementResponse>>> FetchAgreements(int userId = 0, int leaseStatusId = 0)
    {
        var isUserOrAdmin = IsUserOrAdmin(userId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        var leaseAgreementsResponse =
            await _unitOfWork.LeaseAgreementRepository.GetLeaseAgreementsAsync(userId, leaseStatusId);

        var response = _mapper.Map<ICollection<LeaseAgreementResponse>>(leaseAgreementsResponse);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeaseAgreementResponse>> FetchAgreementById(int id)
    {
        var agreementResponse = await _unitOfWork.LeaseAgreementRepository.GetAsync(lease => lease.Id == id, $"{nameof(LeaseAgreement.Status)},{nameof(LeaseAgreement.UnitUser)}");
        var agreement = agreementResponse.FirstOrDefault();

        if (agreement is null) return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(agreement.UnitUser.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        var response = _mapper.Map<LeaseAgreementResponse>(agreement);

        return Ok(response);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateLeaseAgreement(int id, LeaseAgreementPatchRequest request)
    {
        if (id != request.Id) return BadRequest();

        var leaseAgreementResponse = await _unitOfWork.LeaseAgreementRepository.GetAsync(lease => lease.Id == id, nameof(LeaseAgreement.UnitUser));
        var leaseAgreement = leaseAgreementResponse.FirstOrDefault();
        if (leaseAgreement is null) return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(leaseAgreement.UnitUser.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        leaseAgreement.StartDate = request.StartDate ?? leaseAgreement.StartDate;
        leaseAgreement.EndDate = request.EndDate ?? leaseAgreement.EndDate;
        leaseAgreement.SignedOn = request.SignedOn ?? leaseAgreement.SignedOn;
        leaseAgreement.Link = request.Link ?? leaseAgreement.Link;
        leaseAgreement.LeaseStatusId = request.StatusId ?? leaseAgreement.LeaseStatusId;

        _unitOfWork.LeaseAgreementRepository.Update(leaseAgreement);

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    private ActionResult? IsUserOrAdmin(int requestUserId)
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
        {
            return Unauthorized();
        }

        bool isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userClaim.Value != requestUserId.ToString())
        {
            return Forbid();
        }

        return null;
    }
}