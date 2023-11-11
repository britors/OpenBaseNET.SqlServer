﻿using AutoMapper;
using MediatR;
using OpenBaseNET.Application.DTOs.Base.Response;
using OpenBaseNET.Application.DTOs.Cliente.Requests;
using OpenBaseNET.Application.DTOs.Cliente.Responses;
using OpenBaseNET.Application.Features.Clientes.AtualizarCliente;
using OpenBaseNET.Application.Features.Clientes.BuscarClientePorId;
using OpenBaseNET.Application.Features.Clientes.BuscarClientesPorNome;
using OpenBaseNET.Application.Features.Clientes.BuscarClientesPorNomeComDapper;
using OpenBaseNET.Application.Features.Clientes.BuscarTodosOsClientes;
using OpenBaseNET.Application.Features.Clientes.CadastrarCliente;
using OpenBaseNET.Application.Features.Clientes.DeletarCliente;
using OpenBaseNET.Application.Interfaces.Services;

namespace OpenBaseNET.Application.Services;

public sealed class ClienteApplicationService : IClienteApplicationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ClienteApplicationService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<AtualizarClienteResponse?>
        AtualizarAsync(AtualizarClienteRequest request)
    {
        var query = _mapper.Map<AtualizarClienteCommand>(request);
        return await _mediator.Send(query);
    }

    public async Task<IEnumerable<BuscaClienteResponse>>
        BuscarClientesPorNomeAsync(BuscaClientePorNomeRequest request)
    {
        var query = _mapper.Map<BuscarClientesPorNomeQuery>(request);
        return await _mediator.Send(query);
    }

    public async Task<IEnumerable<BuscaClienteResponse>>
        BuscarClientesPorNomeComDapperAsync(BuscaClientePorNomeRequest request)
    {
        var query = _mapper.Map<BuscarClientesPorNomeComDapperQuery>(request);
        return await _mediator.Send(query);
    }

    public async Task<CadastrarClienteResponse?>
        CadastrarAsync(CadastrarClienteRequest request)
    {
        var query = _mapper.Map<CadastrarClienteCommand>(request);
        return await _mediator.Send(query);
    }

    public async Task<DeletarClienteResponse?> DeletarAsync(DeletarClienteRequest request)
    {
        var query = _mapper.Map<DeletarClienteCommand>(request);
        return await _mediator.Send(query);
    }

    public async Task<BuscaClienteResponse> BuscarClienteAsync(BuscaClienteRequest request)
    {
        var query = _mapper.Map<BuscarClientePorIdQuery>(request);
        return await _mediator.Send(query);
    }

    public async Task<PaginatedResponse<BuscaClienteResponse>> TodosOsClientesAsync(TodosOsClientesRequest request)
    {
        var query = _mapper.Map<BuscarTodosOsClientesQuery>(request);
        return await _mediator.Send(query);
    }
}