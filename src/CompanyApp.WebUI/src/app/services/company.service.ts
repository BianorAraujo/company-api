import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Company } from '../models/company';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private apiUrl = `${environment.ApiUrl}/company`

  constructor(private http: HttpClient) { }

  GetAllCompanies() : Observable<Company[]>{
    return this.http.get<Company[]>(`${this.apiUrl}/getall`);
  }

  GetCompanyById(id:number) : Observable<Company>{
    return this.http.get<Company>(`${this.apiUrl}/getbyid/${id}`);
  }

  GetCompanyByIsin(isin:string) : Observable<Company>{
    return this.http.get<Company>(`${this.apiUrl}/getbyisin/${isin}`);
  }

  CreateCompany(company: Company) : Observable<number>{
    return this.http.post<number>(`${this.apiUrl}/create`, company);
  }

  UpdateCompany(company: Company) : Observable<Company>{
    return this.http.put<Company>(`${this.apiUrl}/update/${company.id}`, company);
  }

}
