import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.ApiUrl}/auth`
  
  constructor(private http: HttpClient) { }
  
    GetToken() : Observable<any>{
      return this.http.get<any>(`${this.apiUrl}/gettoken`);
    }
}
