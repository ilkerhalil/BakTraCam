import { Injectable } from '@angular/core';
import { BakimModel, BakimModelBasic, PostResult } from 'app/models';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs/internal/observable/of';
import { Observable } from 'rxjs/Observable';
import { BaseService } from 'app/shared/base.service';

@Injectable()
export class BakimService extends BaseService {

    getirBakim(bakimId: number): Observable<BakimModel> {
        return this.getOnly<BakimModel>('/v1/Bakim/GetirBakim?id=' + bakimId);
    }
    silBakim(bakimId: number): Observable<PostResult> {
        return this.deleteValue('/v1/Bakim/SilBakim?id=' + bakimId).pipe(
            map((response: PostResult) => {

                return response;
            }),
            catchError((error) => {
                return of({ success: false } as PostResult);
            }),
        );
    }
    getirBakimListesi(): Observable<BakimModelBasic[]> {
        return this.getOnly<BakimModelBasic[]>('/v1/Bakim/BakimListesiniGetir/');
    }
    getirOnBesGunYaklasanBakimListesi(): Observable<BakimModelBasic[]> {
        return this.getOnly<BakimModelBasic[]>('/v1/Bakim/OnBesGunYaklasanBakimlariGetir/');
    }
    getirBakimListesiTipFiltreli(tip: number): Observable<BakimModelBasic[]> {
        return this.getOnly<BakimModelBasic[]>('/v1/Bakim/getirBakimListesiTipFiltreli?tip=' + tip);
    }
    getirBakimListesiDurumFiltreli(durum: number): Observable<BakimModelBasic[]> {
        console.log(durum);
        return this.getOnly<BakimModelBasic[]>('/v1/Bakim/getirBakimListesiDurumFiltreli?durum=' + durum);
    }
    kaydetBakim(bakimParam: BakimModel): Observable<PostResult> {
        return this.postValue('/v1/Bakim/KaydetBakim', bakimParam).
            pipe(
                map(response => {
                    return response;
                }),
                catchError(() => {
                    return of({ success: false } as PostResult);
                }),
            );
    }
}
