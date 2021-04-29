using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Common.Helper.Enums;
using BakTraCam.Core.DataAccess.Repositories.Maintenance;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Core.Entity;
using BakTraCam.Service.DataContract;
using BakTraCam.Util.Mapping.Adapter;

namespace BakTraCam.Core.Business.Domain.Maintenance
{
    public class MaintenanceDomain : DomainBase<MaintenanceDomain>, IMaintenanceDomain
    {
        private readonly IMaintenanceRepository _bakimRep;
 
        public MaintenanceDomain(ICustomMapper mapper, IDatabaseUnitOfWork uow,IMaintenanceRepository bakimRep) 
            : base(mapper, uow)
        {
            _bakimRep = bakimRep;
        }
        

        public async Task<IEnumerable<MaintenanceModelBasic>> OnBesGunYaklasanBakimlariGetirAsync()
        {
            var query = $"SELECT  t.Id, t.Durum, t.Period, t.Tip, t.Ad, t.Aciklama, t.BaslangicTarihi,t.BitisTarihi,t.Tarihi,"
                  + " ks1.Ad as Sorumlu1, ks2.Ad as Sorumlu2, ks1.Ad as Gerceklestiren1, kg2.Ad as Gerceklestiren2,"
                  + " kg3.Ad as Gerceklestiren3, kg4.Ad as Gerceklestiren4 FROM [tBakim] as t"
                  + " left  join tKullanici ks1 on t.Sorumlu1 = ks1.Id"
                  + " left  join tKullanici ks2 on t.Sorumlu2 = ks2.Id"
                  + " left  join tKullanici kg1 on t.Gerceklestiren1 = kg1.Id"
                  + " left  join tKullanici kg2 on t.Gerceklestiren3 = kg2.Id"
                  + " left  join tKullanici kg3 on t.Gerceklestiren3 = kg3.Id"
                  + " left  join tKullanici kg4 on t.Gerceklestiren4 = kg4.Id"
                  + " where t.Durum="+ (int)Enums.BakimDurum.Beklemede;

            var bakimlar = await Uow.RawQueryAsync<MaintenanceModelBasic>(query, string.Empty);

            return bakimlar;
        }
        public async Task<IEnumerable<MaintenanceModelBasic>> BakimlariGetirAsync()
        {
            var sqlScript = $"SELECT  t.Id, t.Durum, t.Period, t.Tip, t.Ad, t.Aciklama, t.BaslangicTarihi,t.BitisTarihi,t.Tarihi,"
                            + " ks1.Ad as Sorumlu1, ks2.Ad as Sorumlu2, ks1.Ad as Gerceklestiren1, kg2.Ad as Gerceklestiren2,"
                            + " kg3.Ad as Gerceklestiren3, kg4.Ad as Gerceklestiren4 FROM [tBakim] as t"
                            + " left  join tKullanici ks1 on t.Sorumlu1 = ks1.Id"
                            + " left  join tKullanici ks2 on t.Sorumlu2 = ks2.Id"
                            + " left  join tKullanici kg1 on t.Gerceklestiren1 = kg1.Id"
                            + " left  join tKullanici kg2 on t.Gerceklestiren3 = kg2.Id"
                            + " left  join tKullanici kg3 on t.Gerceklestiren3 = kg3.Id"
                            + " left  join tKullanici kg4 on t.Gerceklestiren4 = kg4.Id where t.Durum<>" + (int)Enums.BakimDurum.Tamamlandi;
            
            var bakimlar = await Uow.RawQueryAsync<MaintenanceModelBasic>(sqlScript, string.Empty);

            return bakimlar;
        }

        public async Task<MaintenanceModel> GetirBakimAsync(int id)
        {
            var bakim = new MaintenanceEntity();
            bakim = await _bakimRep.FirstOrDefaultAsync(a => a.Id == id);
            return Mapper.Map<MaintenanceEntity, MaintenanceModel>(bakim);
        }

        public async Task<IEnumerable<MaintenanceModelBasic>> getirBakimListesiTipFiltreliAsync(int tip)
        {
            var sqlScript = $"SELECT  t.Id, t.Durum, t.Period, t.Tip, t.Ad, t.Aciklama, t.BaslangicTarihi,t.BitisTarihi,t.Tarihi,"
                            + " ks1.Ad as Sorumlu1, ks2.Ad as Sorumlu2, ks1.Ad as Gerceklestiren1, kg2.Ad as Gerceklestiren2,"
                            + " kg3.Ad as Gerceklestiren3, kg4.Ad as Gerceklestiren4 FROM [tBakim] as t"
                            + " left  join tKullanici ks1 on t.Sorumlu1 = ks1.Id"
                            + " left  join tKullanici ks2 on t.Sorumlu2 = ks2.Id"
                            + " left  join tKullanici kg1 on t.Gerceklestiren1 = kg1.Id"
                            + " left  join tKullanici kg2 on t.Gerceklestiren3 = kg2.Id"
                            + " left  join tKullanici kg3 on t.Gerceklestiren3 = kg3.Id"
                            + " left  join tKullanici kg4 on t.Gerceklestiren4 = kg4.Id where t.Tip=" + tip+" and Durum<>"+(int)Enums.BakimDurum.Tamamlandi;

            return await Uow.RawQueryAsync<MaintenanceModelBasic>(sqlScript, string.Empty);
        }
        public async Task<IEnumerable<MaintenanceModelBasic>> getirBakimListesiDurumFiltreliAsync(int durum)
        {
            var sqlScript = $"SELECT  t.Id, t.Durum, t.Period, t.Tip, t.Ad, t.Aciklama, t.BaslangicTarihi,t.BitisTarihi,t.Tarihi,"
                            + " ks1.Ad as Sorumlu1, ks2.Ad as Sorumlu2, ks1.Ad as Gerceklestiren1, kg2.Ad as Gerceklestiren2,"
                            + " kg3.Ad as Gerceklestiren3, kg4.Ad as Gerceklestiren4 FROM [tBakim] as t"
                            + " left  join tKullanici ks1 on t.Sorumlu1 = ks1.Id"
                            + " left  join tKullanici ks2 on t.Sorumlu2 = ks2.Id"
                            + " left  join tKullanici kg1 on t.Gerceklestiren1 = kg1.Id"
                            + " left  join tKullanici kg2 on t.Gerceklestiren3 = kg2.Id"
                            + " left  join tKullanici kg3 on t.Gerceklestiren3 = kg3.Id"
                            + " left  join tKullanici kg4 on t.Gerceklestiren4 = kg4.Id where t.Durum=" + durum;

            return await Uow.RawQueryAsync<MaintenanceModelBasic>(sqlScript, string.Empty);
        }

        public async Task<MaintenanceModel> KaydetBakimAsync(MaintenanceModel model)
        {
            var bakim = model.Id > 0 ? await _bakimRep.FirstOrDefaultAsync(m => m.Id == model.Id) : null;
            if (null == bakim)
            {
                // yeni bakımlar beklemede oluşur
                model.Durum = (int)Enums.BakimDurum.Beklemede;
                model.Date = model.StartDate;
                if (model.Tip == (int)Enums.BakimTip.Planli || model.Tip == (int)Enums.BakimTip.Periyodik)
                {
                    // kaç adet bakım olacağı hesaplanlanmalı ve tarihi de not alınmalı
                    var ts = model.StartDate.Subtract(model.StartDate);
                    var dateDiff = int.Parse(ts.TotalDays.ToString());
                    var bakimSayisi = dateDiff / model.Period;
                    for (var i = 0; i < bakimSayisi; i++)
                    {
                        model.Date = model.Date.AddDays(model.Period);
                        bakim = Mapper.Map<MaintenanceModel, MaintenanceEntity>(model);

                        await _bakimRep.AddAsync(bakim);
                        await Uow.SaveChangesAsync();
                    }
                }
                else
                {
                    model.Date = model.StartDate;
                    bakim = Mapper.Map<MaintenanceModel, MaintenanceEntity>(model);

                    await _bakimRep.AddAsync(bakim);
                    await Uow.SaveChangesAsync();
                }

            }
            else
            {
                Mapper.Map(model, bakim);

                await _bakimRep.UpdateAsync(bakim);
                await Uow.SaveChangesAsync();
            }
            return Mapper.Map<MaintenanceEntity, MaintenanceModel>(bakim);

        }

        public async Task<int> SilBakimAsync(int id)
        {
            await _bakimRep.DeleteAsync(m => m.Id == id);
            await Uow.SaveChangesAsync();
            return id;
        }


        
    }
}
