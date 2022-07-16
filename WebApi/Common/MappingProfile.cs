using AutoMapper;
using WebApi.Entities;
using WebApi.Application.AlbumOperations.Queries.GetAlbums;
using WebApi.Application.AlbumOperations.Queries.GetAlbumById;
using WebApi.Application.AlbumOperations.Commands.CreateAlbum;
using WebApi.Application.AlbumOperations.Commands.UpdateAlbum;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.Application.SingerOperations.Queries.GetSingers;
using WebApi.Application.SingerOperations.Queries.GetSingerById;
using WebApi.Application.SingerOperations.Commands.CreateSinger;
using WebApi.Application.SingerOperations.Commands.UpdateSinger;
using WebApi.Application.ProducerOperations.Queries.GetProducers;
using WebApi.Application.ProducerOperations.Queries.GetProducerById;
using WebApi.Application.ProducerOperations.Commands.CreateProducer;
using WebApi.Application.ProducerOperations.Commands.UpdateProducer;
using WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;
using System.Globalization;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // AlbumOperations
        CreateMap<Album, GetAlbumsViewModel>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>src.Year.Year));
        CreateMap<Album, GetAlbumByIdViewModel>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>src.Year.Year));
        CreateMap<CreateAlbumModel, Album>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>new DateTime(src.Year,01,01)));
        CreateMap<UpdateAlbumModel, Album>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Title, opt=> opt.Condition(src=>src.Title is not null))
            .ForMember(dest=> dest.Year, opt=> opt.Condition(src=>src.Year is not null))
            .ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>new DateTime(src.Year.Value,01,01)))
            .ForMember(dest=> dest.Price, opt=> opt.Condition(src=>src.Price is not null))
            .ForMember(dest=> dest.GenreId, opt=> opt.Condition(src=>src.GenreId is not null))
            .ForMember(dest=> dest.ProducerId, opt=> opt.Condition(src=>src.ProducerId is not null))
            .ForMember(dest=> dest.IsActive, opt=> opt.Condition(src=>src.IsActive is not null));
        
        // CustomerOperations
        CreateMap<Customer, GetCustomersViewModel>();
        CreateMap<Customer, GetCustomerByIdViewModel>();
        CreateMap<CreateCustomerModel, Customer>();
         CreateMap<UpdateCustomerModel, Customer>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore());
        // CustomerGenres
        CreateMap<Genre, GetCustomerGenresViewModel>();
        // CustomerAlbums
        CreateMap<CustomerAlbum, GetCustomerAlbumsByIdViewModel>()
            .ForMember(dest=> dest.OrderDate, opt=> opt.MapFrom(src=> src.OrderDate.ToString("yyyy-MM-dd hh:mm:ss")));

        // SingerOperations
        CreateMap<Singer, GetSingersViewModel>();
        CreateMap<Singer, GetSingerByIdViewModel>();
        CreateMap<CreateSingerModel, Singer>();
         CreateMap<UpdateSingerModel, Singer>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Name, opt=> opt.Condition(src=> src.Name is not null))
            .ForMember(dest=> dest.Surname, opt=> opt.Condition(src=> src.Surname is not null));

        // ProducerOperations
        CreateMap<Producer, GetProducersViewModel>();
        CreateMap<Producer, GetProducerByIdViewModel>();
        CreateMap<CreateProducerModel, Producer>();
        CreateMap<UpdateProducerModel, Producer>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Name, opt=> opt.Condition(src=> src.Name is not null))
            .ForMember(dest=> dest.Surname, opt=> opt.Condition(src=> src.Surname is not null));
        
        
    }
}