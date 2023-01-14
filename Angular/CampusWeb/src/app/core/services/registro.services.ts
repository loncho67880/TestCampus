import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
    providedIn: 'root'
})
export class RegistroService {

    constructor(private http: HttpClient) { }

    Registrar(user: User): Observable<any> {
        const httpOptions = {
            headers: new HttpHeaders({'Content-Type': 'application/json'})
          };
        console.log(JSON.stringify(user));
        return this.http.post("https://localhost:7013/api/Registro", JSON.stringify(user), httpOptions);
    }
}
