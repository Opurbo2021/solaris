using Application.Common.Models;
using Application.DTOs.Address;

namespace Application.Interfaces.Services;

public interface IAddressService
{
    Task<Result<IEnumerable<AddressResponse>>> GetListAsync();
    Task<Result<AddressResponse>> GetByIdAsync(int id);
    Task<Result<IEnumerable<AddressResponse>>> SearchAsync(string city, string? state = null);
    Task<Result<AddressResponse>> CreateAsync(CreateAddressRequest request);
    Task<Result> UpdateAsync(int id, UpdateAddressRequest request);
    Task<Result> DeleteAsync(int id);
}