using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class AddressRepo(AppDbContext context) : GenericRepo<Address>(context), IAddressRepo
{
    
}