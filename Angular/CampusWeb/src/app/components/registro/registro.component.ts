import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { User } from 'src/app/core/models/user';
import { ClienteService } from 'src/app/core/services/ciudades.services';
import * as _moment from 'moment';
import { Cities } from 'src/app/core/models/cities';
import { catchError, debounceTime, delay, filter, map, of, ReplaySubject, Subject, switchMap, takeUntil, tap } from 'rxjs';

const moment = _moment;

export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'DD-MMM-YYYY',
    monthYearLabel: 'DD-MMM-YYYY',
    dateA11yLabel: 'DD-MMM-YYYY',
    monthYearA11yLabel: 'DD-MMM-YYYY',
  },
};
@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.scss'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },

    {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
  ]
})
export class RegistroComponent implements OnInit, OnDestroy {
  
  user: User = new User();
  fecha = new FormControl(moment());

  myForm = new FormGroup({
    name: new FormControl(this.user.name, [
      Validators.required,
      Validators.minLength(4)
    ]),
    email: new FormControl(this.user.email, [
      Validators.required,
      Validators.minLength(4),
      Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')
    ]),
    phone: new FormControl(this.user.phone, [
      Validators.required,
      Validators.minLength(4)
    ])
  });

  citySelected = new FormControl<Cities>(new Cities());
  citySelectedFilter = new FormControl<string>('');
  cityInvalid: boolean = false;
  public listCitys: ReplaySubject<Cities[]> = new ReplaySubject<Cities[]>(1);
  public searching = false;
  minDate = new Date("December 31, 1999 23:15:00");

  protected cities: Cities[] = [];

  protected _onDestroy = new Subject<void>();

  constructor(private clienteService: ClienteService){

  }

  ngOnInit(): void {
     // listen for search field value changes
     this.citySelectedFilter.valueChanges
     .pipe(
       filter(search => !!search && search?.length >= 3),
       tap(() => this.searching = true),
       takeUntil(this._onDestroy),
       debounceTime(200),
       switchMap(search => {
         if (!this.cities && search?.length != null &&  search?.length < 3) {
           return [];
         }

         this.clienteService.get(search!).subscribe(data=> {
            this.cities = data;
         });

         return [];
       }),
       delay(500),
       takeUntil(this._onDestroy)
     )
     .subscribe(filteredCities => {
       this.searching = false;
       this.listCitys.next(filteredCities!);
     },
       error => {
         // no errors in our simulated example
         this.searching = false;
         // handle error...
       });
  }

  submitForm(){
    this.cityInvalid = !this.citySelected.valid;
    if(this.myForm.valid && this.fecha.valid && this.citySelected.valid && this.fecha.value!.toDate() > this.minDate){
      this.user.name = this.myForm.get('name')?.value!;
      this.user.email = this.myForm.get('email')?.value!;
      this.user.phone = this.myForm.get('phone')?.value!;
      this.user.date = this.fecha.value!.toDate();
      this.user.city = this.citySelected.value!.completed;
    }
  }

  showErrorName():boolean{
    return (this.myForm.get('name')?.invalid! && this.myForm.get('name')?.touched!) || this.myForm.get('name')?.dirty! && this.myForm.get('name')?.value == '';
  }

  showErrorEmail():boolean{
    return this.myForm.get('email')?.invalid! && this.myForm.get('email')?.touched!;
  }

  showErrorTelefono():boolean{
    return (this.myForm.get('phone')?.invalid! && this.myForm.get('phone')?.touched!) || this.myForm.get('phone')?.dirty! && this.myForm.get('phone')?.value == '';
  }

  showErrorDateMin():boolean{
    return this.fecha.value!.toDate() <= this.minDate;
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
