import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ClienteService {

    constructor(private http: HttpClient) { }

    get(search: string): Observable<any> {
        return this.http.get("https://localhost:7013/api/Cities?search="+search);
    }
}
