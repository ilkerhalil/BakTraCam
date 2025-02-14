﻿using BakTraCam.Core.DataAccess.Repositores;
using BakTraCam.Service.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Core.Entity;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Common.Helper.Enums;
using BakTraCam.Core.DataAccess.Repositores.Bakim;
using BakTraCam.Util.Mapping.Adapter;

namespace BakTraCam.Core.Business.Domain.Bakim
{
    public class BakimDomain : DomainBase<BakimDomain>, IBakimDomain
    {
        //private readonly IDatabaseUnitOfWork _uow;
        private readonly IBakimRepository _bakimRep;
        //private readonly IKullaniciRepository _kullaniciRep;
        //public BakimDomain(IServiceProvider serviceProvider) : base(serviceProvider)
        //{
        //    _bakimRep = serviceProvider.GetService<IBakimRepository>();
        //    _kullaniciRep = serviceProvider.GetService<IKullaniciRepository>();
        //    _uow = serviceProvider.GetService<IDatabaseUnitOfWork>();
        //}
        public BakimDomain(ICustomMapper mapper, IDatabaseUnitOfWork uow,IBakimRepository bakimRep) 
            : base(mapper, uow)
        {
            _bakimRep = bakimRep;
        }
        

        public async Task<IEnumerable<BakimModelBasic>> OnBesGunYaklasanBakimlariGetirAsync()
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

            var bakimlar = await Uow.RawQueryAsync<BakimModelBasic>(query, string.Empty);

            return bakimlar;
        }
        public async Task<IEnumerable<BakimModelBasic>> BakimlariGetirAsync()
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
            
            var bakimlar = await Uow.RawQueryAsync<BakimModelBasic>(sqlScript, string.Empty);

            return bakimlar;
        }

        public async Task<BakimModel> GetirBakimAsync(int id)
        {
            var bakim = new BakimEntity();
            bakim = await _bakimRep.FirstOrDefaultAsync(a => a.Id == id);
            return Mapper.Map<BakimEntity, BakimModel>(bakim);
        }

        public async Task<IEnumerable<BakimModelBasic>> getirBakimListesiTipFiltreliAsync(int tip)
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

            return await Uow.RawQueryAsync<BakimModelBasic>(sqlScript, string.Empty);
        }
        public async Task<IEnumerable<BakimModelBasic>> getirBakimListesiDurumFiltreliAsync(int durum)
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

            return await Uow.RawQueryAsync<BakimModelBasic>(sqlScript, string.Empty);
        }

        public async Task<BakimModel> KaydetBakimAsync(BakimModel model)
        {
            var bakim = model.Id > 0 ? await _bakimRep.FirstOrDefaultAsync(m => m.Id == model.Id) : null;
            if (null == bakim)
            {
                // yeni bakımlar beklemede oluşur
                model.Durum = (int)Enums.BakimDurum.Beklemede;
                model.Tarihi = model.BaslangicTarihi;
                if (model.Tip == (int)Enums.BakimTip.Planli || model.Tip == (int)Enums.BakimTip.Periyodik)
                {
                    // kaç adet bakım olacağı hesaplanlanmalı ve tarihi de not alınmalı
                    var ts = model.BitisTarihi.Subtract(model.BaslangicTarihi);
                    var dateDiff = int.Parse(ts.TotalDays.ToString());
                    var bakimSayisi = dateDiff / model.Period;
                    for (var i = 0; i < bakimSayisi; i++)
                    {
                        model.Tarihi = model.Tarihi.AddDays(model.Period);
                        bakim = Mapper.Map<BakimModel, BakimEntity>(model);

                        await _bakimRep.AddAsync(bakim);
                        await Uow.SaveChangesAsync();
                    }
                }
                else
                {
                    model.Tarihi = model.BaslangicTarihi;
                    bakim = Mapper.Map<BakimModel, BakimEntity>(model);

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
            return Mapper.Map<BakimEntity, BakimModel>(bakim);

        }

        public async Task<int> SilBakimAsync(int id)
        {
            await _bakimRep.DeleteAsync(m => m.Id == id);
            await Uow.SaveChangesAsync();
            return id;
        }


        
    }
}
