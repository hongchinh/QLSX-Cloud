using AutoMapper;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.Data.Responses.NhapXuat;
using QLSX.Shared.DTOs;
using QLSX.Shared.Entities;
using QLSX.Shared.Models;

namespace QLSX.Shared.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<NhapXuatModel, NhapXuatNavigatorResponse>();
        CreateMap<NhapXuatNavigatorResponse, NhapXuatModel>();

        CreateMap<InPhieuNhapXuatResponse, NhapXuatModel>();

        CreateMap<ThuChiModel, ThuChiNavigatorResponse>();
        CreateMap<ThuChiNavigatorResponse, ThuChiModel>();

        CreateMap<NhapXuatTonCuonModel, NhapXuatTonCuonNavigatorResponse>();
        CreateMap<NhapXuatTonCuonNavigatorResponse, NhapXuatTonCuonModel>();

        CreateMap<DieuChuyen, DieuChuyenNavigatorResponse>();
        CreateMap<DieuChuyenNavigatorResponse, DieuChuyen>();

        CreateMap<CoQuanResponse, CoQuan>();
        CreateMap<CoQuan, CoQuanResponse>();


        CreateMap<NhapXuatModel, DonDatHangModel>();
        CreateMap<DonDatHangModel, NhapXuatModel>();
        CreateMap<NoiDungDonDatHang, Entities.NoiDungNhapXuat>();
        CreateMap<Entities.NoiDungNhapXuat, NoiDungDonDatHang>();

        CreateMap<NhapXuatModel, DonDatHangNavigatorResponse>();
        CreateMap<DonDatHangNavigatorResponse, NhapXuatModel>();
        CreateMap<Entities.DonDatHang, DonDatHangNavigatorResponse>();
        CreateMap<DonDatHangNavigatorResponse, Entities.DonDatHang>();

        
    }
}
