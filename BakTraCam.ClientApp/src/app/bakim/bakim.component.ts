import { Component, OnInit, OnDestroy, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { BakimService } from './bakim.service';
import { BakimModel } from 'app/models';
import { Subject, fromEvent } from 'rxjs';
import { takeUntil, tap, map, pluck, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { BakimFormPopupComponent } from './popups/bakim-form-popup/bakim-form-popup.component';

@Component({
  selector: 'app-bakim',
  templateUrl: './bakim.component.html',
  styleUrls: ['./bakim.component.scss']
})
export class BakimComponent implements OnInit, OnDestroy {

  loading: boolean;
  bakimListe: BakimModel[];

  displayedColumns: string[] = ['id', 'ad', 'aciklama', 'tarihi',
    'kullanici1', 'kullanici2', 'gerceklestiren1',
    'gerceklestiren2', 'gerceklestiren3',
    'gerceklestiren4', 'actions'];
  dataSource: MatTableDataSource<BakimModel>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('filter', { static: true })
  filter: ElementRef;

  private _unsubscribeAll: Subject<any> = new Subject();

  constructor(private _bService: BakimService, private _dialog: MatDialog) {
    this.bakimListesiniGetir();
  }
  ngOnInit() {

  }

  ngOnDestroy() {
    this._unsubscribeAll.unsubscribe();
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  private bakimListesiniGetir() {
    this._bService.getirBakimListesi().pipe(
      takeUntil(this._unsubscribeAll),
      tap(() => this.loading = true),
      map((resListe) => {
        this.bakimListe = (resListe as BakimModel[]);
        console.log(this.bakimListe);
        this.dataSource = new MatTableDataSource(this.bakimListe);
        console.log(this.dataSource.data);
      }),
      tap(() => this.loading = false),
    ).subscribe()
  }
  createNewTask(): void {
    this.openBakimPopup({ id: 0 });
  }
  openBakimPopup(data: any): void {
    this._dialog.open(BakimFormPopupComponent, {
      disableClose: true,
      panelClass: 'form-dialog',
      data: data
    }).afterClosed().pipe(
      takeUntil(this._unsubscribeAll)
    ).subscribe((res) => {
      if (res) {
        this.bakimListesiniGetir();
      }
    });
  }
}
