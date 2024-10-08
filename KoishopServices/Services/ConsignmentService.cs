﻿using AutoMapper;
using DTOs.Consignment;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class ConsignmentService : IConsignmentService
{
    private readonly IMapper _mapper;
    private readonly IConsignmentRepository _consignmentRepository;

    public ConsignmentService(IMapper mapper, IConsignmentRepository consignmentRepository)
    {
        this._mapper = mapper;
        this._consignmentRepository = consignmentRepository;
    }
    public async Task AddConsignment(ConsignmentCreationDto consignmentCreationDto)
    {
        //TODO: Add validation before create and mapping
        if (consignmentCreationDto.StartDate.Date < DateTime.Now.Date)
        {
            throw new ArgumentException("StartDate cannot be in the past.");
        }
        if (consignmentCreationDto.EndDate.HasValue && consignmentCreationDto.EndDate.Value.Date < consignmentCreationDto.StartDate.Date)
        {
            throw new ArgumentException("EndDate cannot be earlier than StartDate.");
        }
        if (string.IsNullOrEmpty(consignmentCreationDto.ConsignmentType))
        {
            throw new ArgumentException("ConsignmentType is required.");
        }
        if (consignmentCreationDto.Price <= 0)
        {
            throw new ArgumentException("Price must be greater than zero.");
        }
        if (string.IsNullOrEmpty(consignmentCreationDto.Status))
        {
            throw new ArgumentException("Status is required.");
        }
        var consignment = _mapper.Map<Consignment>(consignmentCreationDto);
        await _consignmentRepository.AddAsync(consignment);
    }

    public async Task<ConsignmentDto> GetConsignmentById(int id)
    {
        var consignment = await _consignmentRepository.GetByIdAsync(id);
        if (consignment == null)
            return null;
        return _mapper.Map<ConsignmentDto>(consignment);
    }

    public async Task<IEnumerable<ConsignmentDto>> GetListConsignment()
    {
        var consignments = await _consignmentRepository.GetAllAsync();
        var result = _mapper.Map<List<ConsignmentDto>>(consignments);
        return result;
    }

    public async Task<bool> RemoveConsignment(int id)
    {
        var exist = await _consignmentRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _consignmentRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateConsignment(int id, ConsignmentUpdateDto consignmentUpdateDto)
    {
        var existingConsignment = await _consignmentRepository.GetByIdAsync(id);
        if (existingConsignment == null)
            return false;

        //TODO: Add validation before Update and mapping
        if (consignmentUpdateDto.StartDate.Date < DateTime.Now.Date)
        {
            throw new ArgumentException("StartDate cannot be in the past.");
        }
        if (consignmentUpdateDto.EndDate.HasValue && consignmentUpdateDto.EndDate.Value.Date < consignmentUpdateDto.StartDate.Date)
        {
            throw new ArgumentException("EndDate cannot be earlier than StartDate.");
        }
        if (string.IsNullOrEmpty(consignmentUpdateDto.ConsignmentType))
        {
            throw new ArgumentException("ConsignmentType is required.");
        }
        if (consignmentUpdateDto.Price <= 0)
        {
            throw new ArgumentException("Price must be greater than zero.");
        }
        if (string.IsNullOrEmpty(consignmentUpdateDto.Status))
        {
            throw new ArgumentException("Status is required.");
        }
        _mapper.Map(consignmentUpdateDto, existingConsignment);
        await _consignmentRepository.UpdateAsync(existingConsignment);
        return true;
    }
}
