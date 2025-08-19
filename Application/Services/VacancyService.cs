using Application.DTOs.Vacancy;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.Vacancy;
using Application.UseCasesInterfaces.VacancyUseCase;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IAddVacancyUseCase _addVacancyUseCase;
        private readonly IGetVacancyUseCase _getVacancyUseCase;
        private readonly IUpdateVacancyUseCase _updateVacancyUseCase;
        private readonly IDeleteVacancyUseCase _deleteVacancyUseCase;
        private readonly IListVacancyUseCase _listVacancyUseCase;
        private readonly IPublishVacancyUseCase _publishVacancyUseCase;
        private readonly IUnpublishVacancyUseCase _unPublishVacancyUseCase;
        private readonly IListPublishedVacancyUseCase _listPublishedVacancyUseCase;

        public VacancyService(IAddVacancyUseCase addVacancyUseCase,
                               IGetVacancyUseCase getVacancyUseCase,
                               IUpdateVacancyUseCase updateVacancyUseCase,
                               IDeleteVacancyUseCase deleteVacancyUseCase,
                               IListVacancyUseCase listVacancyUseCase,
                               IPublishVacancyUseCase publishVacancyUseCase,
                               IUnpublishVacancyUseCase unPublishVacancyUseCase,
                               IListPublishedVacancyUseCase listPublishedVacancyUseCase)
        {
            _addVacancyUseCase = addVacancyUseCase;
            _getVacancyUseCase = getVacancyUseCase;
            _updateVacancyUseCase = updateVacancyUseCase;
            _deleteVacancyUseCase = deleteVacancyUseCase;
            _listVacancyUseCase = listVacancyUseCase;
            _publishVacancyUseCase = publishVacancyUseCase;
            _unPublishVacancyUseCase = unPublishVacancyUseCase;
            _listPublishedVacancyUseCase = listPublishedVacancyUseCase;
        }

        public async Task<IEnumerable<VacancyResponseDTO>> GetAllVacanciesAsync(string? role, int pageNumber, int pageSize)
        {
            return await _listVacancyUseCase.ExecuteAsync(role, pageNumber, pageSize);
        }
        public async Task<IEnumerable<VacancyResponseDTO>> GetAllPublishedVacanciesAsync(string? role, int pageNumber, int pageSize)
        {
            return await _listPublishedVacancyUseCase.ExecuteAsync(role, pageNumber, pageSize);
        }
        public async Task<VacancyResponseDTO> GetVacancyByIdAsync(Guid vacancyId)
        {
            return await _getVacancyUseCase.ExecuteAsync(vacancyId);
        }

        public async Task CreateVacancyAsync(AddVacancyDTO vacancy)
        {
            await _addVacancyUseCase.ExecuteAsync(vacancy);
        }

        public async Task UpdateVacancyAsync(UpdateVacancyDTO updateVacancyDTO, Guid vacancyId)
        {
            await _updateVacancyUseCase.ExecuteAsync(updateVacancyDTO, vacancyId);
        }

        public async Task DeleteVacancyAsync(Guid vacancyId)
        {
            await _deleteVacancyUseCase.ExecuteAsync(vacancyId);
        }

        public async Task PublishVacancyAsync(Guid vacancyId)
        {
            await _publishVacancyUseCase.ExecuteAsync(vacancyId);
        }

        public async Task UnPublishVacancyAsync(Guid vacancyId)
        {
            await _unPublishVacancyUseCase.ExecuteAsync(vacancyId);
        }
    }
}