import { Component, OnInit, Input, ChangeDetectorRef, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { BakimDurum, BakimModel, BakimPeriod, BakimTip, EnumCategory, Select } from 'app/models';
import { BakimService } from 'app/bakim/bakim.service';
import { compareEnumKeys, deepCopy, markAsTouched } from 'app/common/generic-functions';
import { takeUntil, filter, tap, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-bakim-form',
  templateUrl: './bakim-form.component.html',
  styleUrls: ['./bakim-form.component.css']
})
export class BakimFormComponent implements OnInit, AfterViewInit {

  data: BakimModel;
  defaultData: BakimModel;
  formInit = false;
  form: FormGroup;

  // enums
  EnumCategory = EnumCategory;
  BakimDurums = BakimDurum;
  BakimTips = BakimTip;
  BakimPeroids = BakimPeriod;
  compareEnumKeys = compareEnumKeys;

  private _unsubscribeAll = new Subject();
  private _bakimIdWaiter: Subject<number> = new Subject<number>();


  @Output() result: EventEmitter<number> = new EventEmitter<number>();

  private _bakimId = 0;
  @Input() set bakimId(value: number) {
    this._bakimId = value;
    if (this._bakimId > 0) {
      this._bakimIdWaiter.next(value);
    }
  }
  get bakimId(): number {
    return this._bakimId;
  }

  constructor(private formBuilder: FormBuilder,
    private _cd: ChangeDetectorRef,
    private _dialog: MatDialog,
    private _bakimService: BakimService) {
    this._bakimIdWaiter.pipe(
      takeUntil(this._unsubscribeAll),
      filter((id) => id > 0),
      mergeMap((id) => this._bakimService.getirBakim(id))
    ).subscribe((bakim) => {
      if (bakim) {
        this.data = bakim;
        this.defaultData = deepCopy(this.data);
        this.createForm();
      }
    });
  }


  ngOnInit(): void {
    // eğer düzenleme varsa form veriyle dolmalı
    if (!this.bakimId) {
      this.data = {} as BakimModel;
      this.defaultData = deepCopy(this.data);
      this.createForm();
    }
  }
  ngAfterViewInit() { }
  public validateForm(): boolean {
    markAsTouched(this.form);
    return this.form.valid;
  }
  public isClean(): boolean {
    return this.form.pristine;
  }
  public cancel(): void {
    if (!this.isClean()) {
      this.form.reset({
        Ad: this.defaultData.Ad,
        Aciklama: this.data.Aciklama,
        Tarihi: this.data.Tarihi,
        kullanici1: this.data.kullanici1,
        kullanici2: this.data.kullanici2,
        Gerceklestiren1: this.data.Gerceklestiren1,
        Gerceklestiren2: this.data.Gerceklestiren2,
        Gerceklestiren3: this.data.Gerceklestiren3,
        Gerceklestiren4: this.data.Gerceklestiren4,
        Durum: this.data.Durum,
        Tip: this.data.Tip,
        Period: this.data.Period,
      });
      // veri out edilerek parentine verilir
      this.result.emit(this.form.getRawValue());
    }
  }
  createForm(): void {
    this.form = this.formBuilder.group({
      Ad: [this.data.Ad, [Validators.required, Validators.maxLength(50)]],
      Aciklama: [this.data.Aciklama, [Validators.maxLength(100)]],
      Tarihi: [this.data.Tarihi, [Validators.required]],
      Durum: [this.data.Durum, [Validators.required, Validators.min(1)]],
      Tip: [this.data.Tip, [Validators.required, Validators.min(1)]],
      Period: [this.data.Period, [Validators.required, Validators.min(1)]]
    });

    // Kullanıcıları Yükle
    this.formInit = true;
    this._cd.detectChanges();
  }
  save(): void {

    if (this.validateForm()) {

      const bakim = {
        Id: this.bakimId,
        Gerceklestiren1: this.defaultData.Gerceklestiren1,
        Gerceklestiren2: this.defaultData.Gerceklestiren2,
        Gerceklestiren3: this.defaultData.Gerceklestiren3,
        Gerceklestiren4: this.defaultData.Gerceklestiren4,
        kullanici1: this.defaultData.kullanici1,
        kullanici2: this.defaultData.kullanici2,
        Durum: parseInt(this.form.get('Durum').value),
        Tip: parseInt(this.form.get('Tip').value),
        Period: parseInt(this.form.get('Period').value),
        Aciklama: this.form.get('Aciklama').value,
        Tarihi: this.form.get('Tarihi').value,
      } as BakimModel;
      console.log(bakim);
      this._bakimService.kaydetBakim(bakim).pipe(
        takeUntil(this._unsubscribeAll),
        filter((res) => res.success),
        tap((res) => {
          console.log(res);
        }),
        tap((res) => this.bakimId = res.key),
        tap((res) => this.result.emit(res.key))
      ).subscribe();
    }
  }
  SelectSorumluBir(sorumlu: Select): void {
    if (sorumlu) {
      this.defaultData.kullanici1 = sorumlu.Key;
    }
  }
  SelectSorumluIki(sorumlu: Select): void {
    if (sorumlu) {
      this.defaultData.kullanici2 = sorumlu.Key;
    }
  }
  GerceklestirenBir(sorumlu: Select): void {
    if (sorumlu) {
      this.defaultData.Gerceklestiren1 = sorumlu.Key;
    }
  }
  GerceklestirenIki(sorumlu: Select): void {
    if (sorumlu) {
      this.defaultData.Gerceklestiren2 = sorumlu.Key;
    }
  }
  GerceklestirenUc(sorumlu: Select): void {
    if (sorumlu) {
      this.defaultData.Gerceklestiren3 = sorumlu.Key;
    }
  }
  GerceklestirenDort(sorumlu: Select): void {
    if (sorumlu) {
      this.defaultData.Gerceklestiren4 = sorumlu.Key;
    }
  }
  trackByPeriodID(index: number, item: any): string {
    return item.ID;
  }
}

/*
Bakımın türü : Planlı, Talep, Arıza, Periyodik
Bakım önceliğine gerek yok
Bakımın durumu : Planlandı, tamamlandı, iptal, devam ediyor

Plan durumuna göre, filtreli sonuç gmsterilebilmeli
Period: 1 gün, 1 hafta, 2 hafta,3 hafta,1 ay,2 ay, 3 ay,4 ay,6 ay, 1 sene
Kullanıcı başlangıç ve bitiş tarıhını seçecek, periyoda göre diğer bakımlar hesaplanacak

Ana sayfada ise solda o günki elle girilen bakımlar(ekiplerin gittiği işler: talep arıza işleri)
ve o günki planlı bakımlar gözükecek.
ayrıyeten sağda akan bir ekranda yaklaşan (15 günlük planlı ve periyodik bakımlar akacak)
akarkenki bilgiler , bakım adı,tarihi,sorumlu kişileri

*/
